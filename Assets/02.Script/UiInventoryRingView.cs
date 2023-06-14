using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BackEnd;
using UnityEngine.UI.Extensions;


public class UiInventoryRingView : FancyCell<RingData_Fancy>
{
    RingData_Fancy itemData;
    

    [SerializeField]
    private WeaponView weaponView;

    [SerializeField]
    private GameObject equipText;

    [SerializeField]
    private GameObject hasMask;


    private WeaponData weaponData;
    private MagicBookData magicBookData;
    private NewGachaTableData newGachaData;

    [SerializeField]
    private GameObject upgradeButton;

    [SerializeField]
    private TextMeshProUGUI weaponAbilityDescription;


    [SerializeField]
    private Button equipButton;


    [SerializeField]
    private TextMeshProUGUI norigaeDescription;
    private CompositeDisposable disposables = new CompositeDisposable();

    private void SetEquipButton(bool onOff)
    {
        equipButton.gameObject.SetActive(onOff);

    }
    private void OnDestroy()
    {
        disposables.Dispose();
    }

    public void Initialize(WeaponData weaponData, MagicBookData magicBookData, NewGachaTableData newGachaTableData, Action<WeaponData, MagicBookData> onClickCallBack)
    {
        this.weaponData = weaponData;
        this.magicBookData = magicBookData;
        this.newGachaData = newGachaTableData;


        norigaeDescription.gameObject.SetActive(true);

        
        if (newGachaData != null)
        {
            norigaeDescription.SetText($"영혼의숲 능력치\n{newGachaData.Specialadd}배 상승\n<color=red>(공격 능력치)");
        }


        SubscribeWeapon();

        SetParent();
    }


    private void SubscribeWeapon()
    {
        disposables.Clear();

        if (newGachaData != null)
        {
            ServerData.equipmentTable.TableDatas[EquipmentTable.SoulRing].AsObservable().Subscribe(WhenEquipNewGachaChanged).AddTo(disposables);
            ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].hasItem.AsObservable().Subscribe(WhenHasStageChanged).AddTo(disposables);
            ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].amount.AsObservable().Subscribe(WhenAmountChanged).AddTo(disposables);

        }

        if (newGachaData != null)
        {
            ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].level.AsObservable().Subscribe(WhenItemLevelChanged).AddTo(disposables);
        }
    }



    private void WhenItemLevelChanged(int level)
    {
        SetCurrentWeapon();
        UpdateLevelUpUi();
    }

    private void UpdateLevelUpUi()
    {
        if (newGachaData == null) return;

        if (newGachaData != null && ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].level.Value >= newGachaData.Maxlevel)
        {
            return;
        }


        float price = 0f;
        float currentMagicStoneAmount = 0f;

       if (newGachaData != null)
        {
            price = ServerData.newGachaServerTable.GetNewGachaLevelUpPrice(newGachaData.Stringid);
            currentMagicStoneAmount = ServerData.goodsTable.GetCurrentGoods(GoodsTable.GrowthStone);
        }
        

    }

    private void WhenAmountChanged(int amount)
    {
        if (newGachaData != null)
        {
            upgradeButton.SetActive(amount >= newGachaData.Requireupgrade);
        }
    }

    private void WhenHasStageChanged(int state)
    {
        hasMask.SetActive(state == 0);

        SetEquipButton(state == 1);

    }


    private void WhenEquipNewGachaChanged(int idx)
    {
        equipText.SetActive(idx == this.newGachaData.Id);

        UpdateEquipButton();
    }


    public void OnClickUpgradeButton()
    {
        if (newGachaData != null)
        {
            int amount = ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].amount.Value;

            if (amount < newGachaData.Requireupgrade)
            {
                return;
            }

            if (TableManager.Instance.NewGachaData.TryGetValue(newGachaData.Id + 1, out var nextNewGacha))
            {
                int currentWeaponCount = ServerData.newGachaServerTable.GetCurrentNewGachaCount(newGachaData.Stringid);
                int nextWeaponCount = ServerData.newGachaServerTable.GetCurrentNewGachaCount(nextNewGacha.Stringid);

                int upgradeNum = currentWeaponCount / newGachaData.Requireupgrade;

                ServerData.newGachaServerTable.UpData(newGachaData, upgradeNum * newGachaData.Requireupgrade * -1);
                ServerData.newGachaServerTable.UpData(nextNewGacha, upgradeNum);

                ServerData.newGachaServerTable.SyncToServerAll(new List<int>() { newGachaData.Id, nextNewGacha.Id });
                itemData.ParentView.SortHasItem();
            }
            else
            {
                //맥스레벨 처리
                PopupManager.Instance.ShowAlarmMessage("더이상 승급이 불가능 합니다.");
            }
        }
    }

    public void OnClickAllUpgradeButton()
    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "이전 단계 장비들도 전부 승급 합니까?", () =>
        {

            if (newGachaData != null)
            {
                itemData.ParentView.AllUpgradeRing(newGachaData.Id);
                itemData.ParentView.SortHasItem();
            }

        }, null);
    }

    private void SetCurrentWeapon()
    {
        weaponView.Initialize(weaponData, magicBookData, null, newGachaData);


        SetWeaponAbilityDescription();
    }


    private void SetWeaponAbilityDescription()
    {
        WeaponEffectData effectData;
        string stringid;
        int weaponLevel = 0;

        if (newGachaData != null)
        {
            effectData = TableManager.Instance.WeaponEffectDatas[this.newGachaData.Effectid];
            weaponLevel = ServerData.newGachaServerTable.TableDatas[this.newGachaData.Stringid].level.Value;
            stringid = this.newGachaData.Stringid;
        }
        else
        {
            effectData = TableManager.Instance.WeaponEffectDatas[this.magicBookData.Magicbookeffectid];
            weaponLevel = ServerData.magicBookTable.TableDatas[this.magicBookData.Stringid].level.Value;
            stringid = this.magicBookData.Stringid;
        }
        string description = string.Empty;

        description += "<color=#ff00ffff>장착 효과</color>\n";

        float equipValue1 = 0f, equipValue1_max = 0f, equipValue2 = 0f, equipValue2_max = 0f;
        float hasValue1 = 0f, hasValue2 = 0f, hasValue3 = 0f, hasValue1_max = 0f, hasValue2_max = 0f, hasValue3_max = 0f;

        if (newGachaData != null)
        {
            equipValue1 = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Equipeffectbase1, effectData.Equipeffectvalue1);
            equipValue1_max = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Equipeffectbase1, effectData.Equipeffectvalue1, this.newGachaData.Maxlevel);
            equipValue2 = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Equipeffectbase2, effectData.Equipeffectvalue2);
            equipValue2_max = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Equipeffectbase2, effectData.Equipeffectvalue2, this.newGachaData.Maxlevel);

            hasValue1 = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase1, effectData.Haseffectvalue1);
            hasValue1_max = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase1, effectData.Haseffectvalue1, this.newGachaData.Maxlevel);
            hasValue2 = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase2, effectData.Haseffectvalue2);
            hasValue2_max = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase2, effectData.Haseffectvalue2, this.newGachaData.Maxlevel);
            hasValue3 = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase3, effectData.Haseffectvalue3);
            hasValue3_max = ServerData.newGachaServerTable.GetNewGachaEffectValue(this.newGachaData.Stringid, effectData.Haseffectbase3, effectData.Haseffectvalue3, this.newGachaData.Maxlevel);
        }

        if (effectData.Equipeffecttype1 != -1)
        {
            StatusType type = (StatusType)effectData.Equipeffecttype1;

            if (type.IsPercentStat())
            {
                float value = equipValue1 * 100f;
                float value_max = equipValue1_max * 100f;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }
            else
            {
                float value = equipValue1;
                float value_max = equipValue1_max;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }

        }

        if (effectData.Equipeffecttype2 != -1)
        {
            StatusType type = (StatusType)effectData.Equipeffecttype2;

            if (type.IsPercentStat())
            {
                float value = equipValue2 * 100f;
                float value_max = equipValue2_max * 100f;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }
            else
            {
                float value = equipValue2;
                float value_max = equipValue2_max;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }

        }

        description += "\n<color=#ffff00ff>보유 효과</color>\n";

        if (effectData.Haseffecttype1 != -1)
        {
            StatusType type = (StatusType)effectData.Haseffecttype1;

            if (type.IsPercentStat())
            {
                float value = hasValue1 * 100f;
                float value_max = hasValue1_max * 100f;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }
            else
            {
                float value = hasValue1;
                float value_max = hasValue1_max;

                if (newGachaData != null)
                {
                    description += $"{CommonString.GetStatusName(type)} {(value.ToString())}\n";
                }
                else
                {
                    description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
                }
            }
        }

        if (effectData.Haseffecttype2 != -1)
        {
            StatusType type = (StatusType)effectData.Haseffecttype2;

            if (type.IsPercentStat())
            {
                float value = hasValue2 * 100f;
                float value_max = hasValue2_max * 100f;


                description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
            }
            else
            {
                float value = hasValue2;
                float value_max = hasValue2_max;


                description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}\n";
            }

        }

        if (effectData.Haseffecttype3 != -1)
        {
            StatusType type = (StatusType)effectData.Haseffecttype3;

            if (type.IsPercentStat())
            {
                float value = hasValue3 * 100f;
                float value_max = hasValue3_max * 100f;


                description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}";
            }
            else
            {
                float value = hasValue3;
                float value_max = hasValue3_max;


                description += $"{CommonString.GetStatusName(type)} {Utils.ConvertBigNum(value)}";
            }

        }

        weaponAbilityDescription.SetText(description);
    }


    public void OnClickEquipButton()
    {
        
        if (newGachaData != null)
        {
            PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "정말로 반지를 변경 할까요?", () =>
            {
                ServerData.equipmentTable.ChangeEquip(EquipmentTable.SoulRing, newGachaData.Id);
            }, () => { });
        }

        UpdateEquipButton();
    }

    private void UpdateEquipButton()
    {
        int id = this.weaponData != null ? weaponData.Id : this.magicBookData != null ? magicBookData.Id : newGachaData.Id;

        int has = 0;


        if (newGachaData != null)
        {
            has = ServerData.newGachaServerTable.GetNewGachaData(newGachaData.Stringid).hasItem.Value;
        }

        SetEquipButton(has == 1);

        if (equipButton.gameObject.activeSelf)
        {
            string key = weaponData != null ? EquipmentTable.Weapon : magicBookData != null ? EquipmentTable.MagicBook : EquipmentTable.SoulRing;
            int equipIdx = ServerData.equipmentTable.TableDatas[key].Value;

            equipButton.interactable = equipIdx != id;
            //equipDescription.SetText(equipIdx == id ? "장착중" : "장착");
        }
        // ShowSubDetailView();
    }

    public void SetParent()
    {
        
        if (newGachaData != null)
        {
            if (ServerData.newGachaServerTable.TableDatas[newGachaData.Stringid].hasItem.Value == 1)
            {
                this.transform.SetAsFirstSibling();
            }
        }
    }

    public void UpdateUi(NewGachaTableData newGachaTableData)
    {
        this.newGachaData = newGachaTableData;
        Initialize(null, null, newGachaData, null);
    }

    public override void UpdateContent(RingData_Fancy itemData)
    {
        if (this.itemData != null && this.itemData.RingData.Id == itemData.RingData.Id)
        {
            return;
        }

        this.itemData = itemData;

//        Debug.LogError("DolpasS!");
        if (itemData.RingData != null)
        {
            UpdateUi(this.itemData.RingData);
        }
    }

    float currentPosition = 0;
    [SerializeField] Animator animator = default;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    public override void UpdatePosition(float position)
    {
        currentPosition = position;

        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }

        animator.speed = 0;
    }

    void OnEnable() => UpdatePosition(currentPosition);
}
