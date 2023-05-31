using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiInventoryAnimalView : MonoBehaviour
{
    [SerializeField]
    private UiAnimalView uiAnimalView;

    [SerializeField]
    private TextMeshProUGUI abilDescription;
    [SerializeField]
    private TextMeshProUGUI acquireDescription;

    [SerializeField]
    private TextMeshProUGUI upgradePrice;
    [SerializeField]
    private TextMeshProUGUI upgradePrice_Awake;
    [SerializeField]
    private TextMeshProUGUI equipButtonDesc;

    [SerializeField]
    private TextMeshProUGUI levelUpButtonDesc;

    [SerializeField]
    private TextMeshProUGUI levelUpButtonDesc_2;

    
    [SerializeField]
    private Image levelUpButtonImage;
    [SerializeField]
    private Image levelUpButtonAwakeImage;

    [SerializeField]
    private Image equipButtonImage;

    [SerializeField]
    private Sprite greenButtonSprite;

    [SerializeField]
    private Sprite purpleButtonSprite;

    [SerializeField]
    private ContinueOpenButton continueOpenButton_Continue;

    [SerializeField]
    private GameObject continueOpenButton_Btn;

    private SuhopetTableData suhopetTableData;

    private SuhoSuhoPetServerData suhoSuhoPetServerData;

    private Coroutine syncRoutine;

    private WaitForSeconds waitDelay = new WaitForSeconds(0.1f);

    [SerializeField]
    private GameObject lockObject;


    public void Initialize(SuhopetTableData suhopetTableData)
    {
        this.suhopetTableData = suhopetTableData;

        suhoSuhoPetServerData = ServerData.suhoAnimalServerTable.TableDatas[suhopetTableData.Stringid];
            
        acquireDescription.SetText(this.suhopetTableData.Acquiredescription);
        
        uiAnimalView.Initialize(suhopetTableData);

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.equipmentTable.TableDatas[EquipmentTable.SuhoAnimal].AsObservable().Subscribe(e =>
        {
            equipButtonImage.sprite = suhopetTableData.Id == e ? purpleButtonSprite : greenButtonSprite;

            equipButtonDesc.SetText(suhopetTableData.Id == e ? "장착중" : "장착");
        }).AddTo(this);


        suhoSuhoPetServerData.level.AsObservable().Subscribe(currentLevel =>
        {
            int maxLevel = suhopetTableData.Maxlevel;

            if (currentLevel >= maxLevel)
            {
                if (suhopetTableData.SUHOPETTYPE == SuhoPetType.Basic)
                {
                    upgradePrice.SetText("<color=red>각성완료</color>");
                    upgradePrice_Awake.SetText("<color=red>각성완료</color>");
                }
                else
                {
                    upgradePrice.SetText("<color=red>최대레벨</color>");
                    upgradePrice_Awake.SetText("<color=red>최대레벨</color>");
                }

                levelUpButtonImage.sprite = purpleButtonSprite;
                levelUpButtonAwakeImage.sprite = purpleButtonSprite;
            }
            else
            {
                upgradePrice.SetText(suhopetTableData.Requirevalue[currentLevel].ToString());
                upgradePrice_Awake.SetText(suhopetTableData.Requirevalue[currentLevel].ToString());
                levelUpButtonImage.sprite = greenButtonSprite;
                levelUpButtonAwakeImage.sprite = greenButtonSprite;
            }

            if (currentLevel == GameBalance.suhoAnimalAwakeLevel - 1)
            {
                continueOpenButton_Continue.gameObject.SetActive(false);
                continueOpenButton_Continue.StopAutoClickRoutine();
                continueOpenButton_Btn.SetActive(true);
                if (suhopetTableData.SUHOPETTYPE == SuhoPetType.Basic)
                {
                    levelUpButtonDesc.SetText("각성");
                    levelUpButtonDesc_2.SetText("각성");
                }
                else
                {
                    levelUpButtonDesc.SetText("최대레벨");
                    levelUpButtonDesc_2.SetText("최대레벨");
                }
            }
            else
            {
                continueOpenButton_Continue.gameObject.SetActive(true);
                continueOpenButton_Btn.SetActive(false);
                levelUpButtonDesc.SetText("레벨업");
                levelUpButtonDesc_2.SetText("레벨업");
            }

            abilDescription.SetText(
                $"보유효과\n{CommonString.GetStatusName(StatusType.SuperCritical11DamPer)} {suhopetTableData.Abilvalue[currentLevel] * 100}%");
        }).AddTo(this);

        suhoSuhoPetServerData.hasItem.AsObservable().Subscribe(e => { lockObject.SetActive(e == 0); }).AddTo(this);
    }

    
    public void OnClickLevelUpButton()
    {
        if (suhoSuhoPetServerData.level.Value != GameBalance.suhoAnimalAwakeLevel - 1)
        {
            LevelUpRoutine();
        }
        else
        {
            continueOpenButton_Continue.gameObject.SetActive(false);
            continueOpenButton_Continue.StopAutoClickRoutine();
            if (suhopetTableData.SUHOPETTYPE == SuhoPetType.Basic)
            {
                PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "수호동물을 각성 할까요?\n수호동물 각성시,수호동물이 기술을 사용 합니다.\n(각성된 수호동물중 가장 높은 단계 기술 사용)", () => { LevelUpRoutine(); }, null);
            }
            else
            {
                LevelUpRoutine();
            }
        }
    }

    private void LevelUpRoutine()
    {
        int currentLevel = suhoSuhoPetServerData.level.Value;

        if (currentLevel >= suhopetTableData.Maxlevel)
        {
            if (suhopetTableData.SUHOPETTYPE == SuhoPetType.Basic)
            {
                PopupManager.Instance.ShowAlarmMessage("이미 각성 했습니다.");
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("이미 최대레벨입니다.");
            }

            return;
        }

        int requireGoods = suhopetTableData.Requirevalue[currentLevel];

        int currentGoods = (int)ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeed].Value;

        if (currentGoods < requireGoods)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type.SuhoPetFeed))}이 부족합니다.");
            return;
        }

        suhoSuhoPetServerData.level.Value++;

        ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeed].Value -= requireGoods;

        if (syncRoutine != null)
        {
            CoroutineExecuter.Instance.StopCoroutine(syncRoutine);
        }

        syncRoutine = CoroutineExecuter.Instance.StartCoroutine(SyncRoutine());
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SyncRoutine()
    {
        yield return waitDelay;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.SuhoPetFeed, ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeed].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        Param suhoPetParam = new Param();
        suhoPetParam.Add(suhopetTableData.Stringid, suhoSuhoPetServerData.ConvertToString());
        transactions.Add(TransactionValue.SetUpdate(SuhoAnimalServerTable.tableName, SuhoAnimalServerTable.Indate, suhoPetParam));

        ServerData.SendTransaction(transactions);
    }

    public void OnClickEquipButton()
    {
        ServerData.equipmentTable.ChangeEquip(EquipmentTable.SuhoAnimal, this.suhopetTableData.Id);
    }
}