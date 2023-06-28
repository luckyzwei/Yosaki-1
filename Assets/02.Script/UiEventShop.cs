                                                         using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiEventShop : MonoBehaviour
{
    
    [SerializeField]
    private Transform cellParent;
    [SerializeField]
    private UiEventMission2ShopCell shopCell;


    private void Awake()
    {
        Initialize();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value += 100;
        }

    }
#endif

    private void Initialize()
    {
        var tableData = TableManager.Instance.xMasCollection.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].COMMONTABLEEVENTTYPE != CommonTableEventType.Flower) continue;
            if (tableData[i].Active == false) continue;
            var cell = Instantiate<UiEventMission2ShopCell>(shopCell, cellParent);

            cell.Initialize(i);
        }
    }
    public void OnClickAllReceive()
    {
        var tableData = TableManager.Instance.EventMission.dataArray;
        int rewardedNum = 0;
        List<int> rewardTypeList = new List<int>();
        
        List<string> stringIdList = new List<string>();
        for (int i = 0; i < tableData.Length; i++)
        {
            //Second가 아니라면
            if (tableData[i].EVENTMISSIONTYPE != EventMissionType.SECOND) continue;
            //Enable을 껐다면
            if (tableData[i].Enable == false) continue;
            //보상을 받았다면
            if (ServerData.eventMissionTable.CheckMissionRewardCount(tableData[i].Stringid) > 0) continue;
            //깨지 않았다면
            if (ServerData.eventMissionTable.CheckMissionClearCount(tableData[i].Stringid) <
                tableData[i].Rewardrequire) continue;

            //보상
            ServerData.eventMissionTable.TableDatas[tableData[i].Stringid].rewardCount.Value++;
            //패스사면 두배
            if (ServerData.iapServerTable.TableDatas[UiEventPassBuyButton.productKey].buyCount.Value > 0)
            {
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Rewardtype, tableData[i].Rewardvalue * 2);
            }
            else
            {
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Rewardtype, tableData[i].Rewardvalue);
                //삿을떄 보너스 추가
                ServerData.goodsTable.AddLocalData(GoodsTable.Event_Mission_All, tableData[i].Rewardvalue);
            }

            if (rewardTypeList.Contains(tableData[i].Rewardtype) == false)
            {
                rewardTypeList.Add(tableData[i].Rewardtype);
            }
            if (stringIdList.Contains(tableData[i].Stringid) == false)
            {
                stringIdList.Add(tableData[i].Stringid);
            }
            rewardedNum++;
        }

        if (rewardedNum > 0)
        {
            List<TransactionValue> transactions = new List<TransactionValue>();
            
            var e = rewardTypeList.GetEnumerator();

            Param goodsParam = new Param();
            while (e.MoveNext())
            {
                goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)e.Current), ServerData.goodsTable.GetTableData((Item_Type)e.Current).Value);
            }
            goodsParam.Add(GoodsTable.Event_Mission_All, ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value);;
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        
            using var stringName = stringIdList.GetEnumerator();
        
            Param eventMissionParam = new Param();
            while(stringName.MoveNext())
            {
                string updateValue = ServerData.eventMissionTable.TableDatas[stringName.Current].ConvertToString();
                eventMissionParam.Add(stringName.Current, updateValue);
            }
            transactions.Add(TransactionValue.SetUpdate(EventMissionTable.tableName, EventMissionTable.Indate, eventMissionParam));
    
            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "보상을 전부 수령했습니다", null);
                //LogManager.Instance.SendLogType("ChildPass", "A", "A");
            });
        }
        else
        {
            PopupManager.Instance.ShowAlarmMessage("수령할 보상이 없습니다.");
        }
    }
}
