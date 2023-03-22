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
    private TextMeshProUGUI upgradePrice;

    [SerializeField]
    private TextMeshProUGUI equipButtonDesc;

    [SerializeField]
    private Image levelUpButtonImage;

    [SerializeField]
    private Image equipButtonImage;

    [SerializeField]
    private Sprite greenButtonSprite;

    [SerializeField]
    private Sprite purpleButtonSprite;


    private SuhopetTableData suhopetTableData;

    private SuhoSuhoPetServerData suhoSuhoPetServerData;

    private Coroutine syncRoutine;

    private WaitForSeconds waitDelay = new WaitForSeconds(0.5f);

    [SerializeField]
    private GameObject lockObject;


    public void Initialize(SuhopetTableData suhopetTableData)
    {
        this.suhopetTableData = suhopetTableData;

        suhoSuhoPetServerData = ServerData.suhoAnimalServerTable.TableDatas[suhopetTableData.Stringid];

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
                upgradePrice.SetText("최대레벨");
            }
            else
            {
                upgradePrice.SetText(suhopetTableData.Requirevalue[currentLevel].ToString());
            }

            abilDescription.SetText(
                $"보유효과\n{CommonString.GetStatusName(StatusType.SuperCritical11DamPer)} {suhopetTableData.Abilvalue[currentLevel] * 100}%");

        }).AddTo(this);
        
        suhoSuhoPetServerData.hasItem.AsObservable().Subscribe(e =>
        {
            
            lockObject.SetActive(e == 0);
            
        }).AddTo(this);
        
        
    }

    public void OnClickLevelUpButton()
    {
        int currentLevel = suhoSuhoPetServerData.level.Value;

        if (currentLevel >= suhopetTableData.Maxlevel)
        {
            PopupManager.Instance.ShowAlarmMessage("최고레벨 입니다");
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
            StopCoroutine(syncRoutine);
        }

        syncRoutine = StartCoroutine(SyncRoutine());
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SyncRoutine()
    {
        yield return waitDelay;
        
        List<TransactionValue> transactions = new List<TransactionValue>();
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.SuhoPetFeed, ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeed].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));
        
        Param suhoPetParam = new Param();
        suhoPetParam.Add(suhopetTableData.Stringid,suhoSuhoPetServerData.ConvertToString());
        transactions.Add(TransactionValue.SetUpdate(SuhoAnimalServerTable.tableName,SuhoAnimalServerTable.Indate,suhoPetParam));
        
        ServerData.SendTransaction(transactions);
    }

    public void OnClickEquipButton()
    {
        ServerData.equipmentTable.ChangeEquip(EquipmentTable.SuhoAnimal, this.suhopetTableData.Id);
    }
}