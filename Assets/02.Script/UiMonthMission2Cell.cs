using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BackEnd;

public class UiMonthMission2Cell : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon_free;

    [SerializeField]
    private TextMeshProUGUI itemName_free;

    [SerializeField]
    private Image itemIcon_ad;

    [SerializeField]
    private TextMeshProUGUI itemName_ad;

    [SerializeField]
    private TextMeshProUGUI itemAmount_free;

    [SerializeField]
    private TextMeshProUGUI itemAmount_ad;
    
    
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI gaugeText;

    [SerializeField]
    private Button getButton;

    [SerializeField]
    private TextMeshProUGUI rewardNum;

    [SerializeField]
    private TextMeshProUGUI exchangeNum;

    private MonthMission2Data tableData;

    [SerializeField]
    private GameObject lockMask;
    [SerializeField]
    private GameObject lockMaskAd;
    [SerializeField]
    private GameObject beforeLockMask;
    [SerializeField]
    private GameObject beforeLockMaskAd;


    private int getAmountFactor;
    private void SetItemIcon()
    {
        itemIcon_free.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)(int)tableData.Reward1);
        itemIcon_ad.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)(int)tableData.Reward2);

        itemName_free.SetText(CommonString.GetItemName((Item_Type)(int)tableData.Reward1));
        itemName_ad.SetText(CommonString.GetItemName((Item_Type)(int)tableData.Reward2));
    }
    private void SetAmount()
    {
        itemAmount_free.SetText(Utils.ConvertBigNum(tableData.Reward1_Value));
        itemAmount_ad.SetText(Utils.ConvertBigNum(tableData.Reward2_Value));
    }
    public void Initialize(MonthMission2Data tableData)
    {
        if (tableData.Enable == false)
        {
            this.gameObject.SetActive(false);
            return;
        }

        this.tableData = tableData;

        //exchangeNum.SetText($"매일 교환 : {ServerData.eventMissionTable.TableDatas[tableData.Stringid].rewardCount}/{TableManager.Instance.EventMission.dataArray[tableData.Id].Dailymaxclear}");

        title.SetText(tableData.Title);

        Subscribe();

        SetAmount();
        
        SetItemIcon();
    }

    private void Subscribe()
    {
        ServerData.eventMissionTable.TableDatas[tableData.Stringid].clearCount.AsObservable().Subscribe(e =>
            {
                WhenMissionCountChanged(e);
                beforeLockMask.SetActive(e < tableData.Rewardrequire);
                beforeLockMaskAd.SetActive(e < tableData.Rewardrequire);

            }).AddTo(this);
        ServerData.eventMissionTable.TableDatas[tableData.Stringid].rewardCount.AsObservable().Subscribe(e=>
        {
             //exchangeNum.SetText($"매일 교환 : {ServerData.eventMissionTable.TableDatas[tableData.Stringid].rewardCount}/{TableManager.Instance.EventMission.dataArray[tableData.Id].Dailymaxclear}");
            if(e>=TableManager.Instance.EventMission.dataArray[tableData.Id].Dailymaxclear)
            {
                lockMask.SetActive(true);
            }
            else
            {
                lockMask.SetActive(false);
            }
        }).AddTo(this);
        ServerData.eventMissionTable.TableDatas[tableData.Stringid].adrewardCount.AsObservable().Subscribe(e=>
        {
             //exchangeNum.SetText($"매일 교환 : {ServerData.eventMissionTable.TableDatas[tableData.Stringid].rewardCount}/{TableManager.Instance.EventMission.dataArray[tableData.Id].Dailymaxclear}");
            if(e>=TableManager.Instance.EventMission.dataArray[tableData.Id].Dailymaxclear)
            {
                lockMaskAd.SetActive(true);
            }
            else
            {
                lockMaskAd.SetActive(false);
            }
        }).AddTo(this);
    }

    private void OnEnable()
    {
        if (tableData != null)
        {
            WhenMissionCountChanged(ServerData.eventMissionTable.TableDatas[tableData.Stringid].clearCount.Value);
        }

    }

    private void WhenMissionCountChanged(int count)
    {
        if (this.gameObject.activeInHierarchy == false) return;


        gaugeText.SetText($"{count}/{tableData.Rewardrequire}");

        getButton.interactable = count >= tableData.Rewardrequire;



        // float passBonus = 0;
        // if (ServerData.iapServerTable.TableDatas[UiNewYearPassBuyButton.productKey].buyCount.Value > 0)
        // {
        //     passBonus = tableData.Reward1_Value;
        // }

        rewardNum.SetText($"{tableData.Reward1_Value}개");
    }


    public void OnClickGetButton()
    {
        if (ServerData.eventMissionTable.TableDatas[tableData.Stringid].rewardCount.Value>0)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 수령하였습니다.");
            return;
        }
        if (tableData.Rewardrequire > ServerData.eventMissionTable.TableDatas[tableData.Stringid].clearCount.Value)
        {
            PopupManager.Instance.ShowAlarmMessage("클리어 조건 미달성");
            return;
        }

        //로컬 갱신
        EventMissionManager.UpdateEventMissionClear((MonthMission2Key)(tableData.Id), -tableData.Rewardrequire );
        EventMissionManager.UpdateEventMissionReward((MonthMission2Key)(tableData.Id), 1);

        ServerData.goodsTable.AddLocalData(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1),
            tableData.Reward1_Value);

        PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type)tableData.Reward1)} {tableData.Reward1_Value}개 획득!!");
        SoundManager.Instance.PlaySound("GoldUse");
        
        List<TransactionValue> transactionList = new List<TransactionValue>();

        Param eventMissionParam = new Param();
        Param goodsParam = new Param();

        //미션 카운트 차감
        eventMissionParam.Add(tableData.Stringid, ServerData.eventMissionTable.TableDatas[tableData.Stringid].ConvertToString());
        transactionList.Add(TransactionValue.SetUpdate(EventMissionTable.tableName, EventMissionTable.Indate, eventMissionParam));

        //재화 추가
        goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1),
            ServerData.goodsTable
                .GetTableData(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1)).Value);
        transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        ServerData.SendTransaction(transactionList);
    }
    public void OnClickGetAdButton()
    {
        if (ServerData.iapServerTable.TableDatas[UiMonthPassBuyButton2.monthPassKey].buyCount.Value <1)
        {
            PopupManager.Instance.ShowAlarmMessage("월간 패스권이 필요합니다!");
            return;
        }
        if (ServerData.eventMissionTable.TableDatas[tableData.Stringid].adrewardCount.Value>0)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 수령하였습니다.");
            return;
        }
        if (tableData.Rewardrequire > ServerData.eventMissionTable.TableDatas[tableData.Stringid].clearCount.Value)
        {
            PopupManager.Instance.ShowAlarmMessage("클리어 조건 미달성");
            return;
        }
        
   


        
        //로컬 갱신
        EventMissionManager.UpdateEventMissionClear((MonthMission2Key)(tableData.Id), -tableData.Rewardrequire );
        EventMissionManager.UpdateEventMissionAdReward((MonthMission2Key)(tableData.Id), 1);

        ServerData.goodsTable.AddLocalData(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1),
            tableData.Reward1_Value);

        PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type)tableData.Reward1)} {tableData.Reward1_Value}개 획득!!");
        SoundManager.Instance.PlaySound("GoldUse");
        
        List<TransactionValue> transactionList = new List<TransactionValue>();

        Param eventMissionParam = new Param();
        Param goodsParam = new Param();

        //미션 카운트 차감
        eventMissionParam.Add(tableData.Stringid, ServerData.eventMissionTable.TableDatas[tableData.Stringid].ConvertToString());
        transactionList.Add(TransactionValue.SetUpdate(EventMissionTable.tableName, EventMissionTable.Indate, eventMissionParam));

        //재화 추가
        goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1),
            ServerData.goodsTable
                .GetTableData(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward1)).Value);
        transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        ServerData.SendTransaction(transactionList);
    }

}
