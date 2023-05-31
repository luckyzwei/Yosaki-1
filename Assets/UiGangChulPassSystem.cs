﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiGangChulPassSystem : MonoBehaviour
{
    [SerializeField]
    private UiGangChulPassCell uiPassCellPrefab;
    [SerializeField]
    private GCPassTab tabPrefab;
    [SerializeField]
    private GCPassScrollRect scrollRectPrefab;

    [SerializeField]
    private List<Transform> cellParents;

    [SerializeField] private SeletableTab _seletableTab;
    
    [SerializeField]
    private Transform tabParent;

    [SerializeField] private Transform scrollRectTransform;

    private List<UiGangChulPassCell> uiPassCellContainer = new List<UiGangChulPassCell>();

    private List<int> splitData_Free;
    private List<int> splitData_Ad;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        var tableData = TableManager.Instance.GCPass.dataArray;

        //int interval = tableData.Length - uiPassCellContainer.Count;
        int tabCnt = 0;
        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Tabtype==tabCnt)
            {
                var prefab = Instantiate<GCPassTab>(tabPrefab, tabParent);
                prefab.Initialize(tabCnt, _seletableTab: _seletableTab);
                var prefab2 = Instantiate<GCPassScrollRect>(scrollRectPrefab, scrollRectTransform);
                prefab2.Initialize(_seletableTab: _seletableTab);
                cellParents.Add(prefab2.GetRectTransform());
                tabCnt++;
            }
        }
        for (int i = 0; i < tableData.Length; i++)
        {
            var prefab = Instantiate<UiGangChulPassCell>(uiPassCellPrefab, cellParents[tableData[i].Tabtype]);
            uiPassCellContainer.Add(prefab);
        }

        for (int i = 0; i < uiPassCellContainer.Count; i++)
        {
            if (i < tableData.Length)
            {
                var passInfo = new DamagePassInfo();

                passInfo.require = tableData[i].Unlockdamage;
                passInfo.id = tableData[i].Id;

                passInfo.rewardType_Free = tableData[i].Reward1;
                passInfo.rewardTypeValue_Free = tableData[i].Reward1_Value;
                passInfo.rewardType_Free_Key = ColdSeasonPassServerTable.gangChulFree;

                passInfo.rewardType_IAP = tableData[i].Reward2;
                passInfo.rewardTypeValue_IAP = tableData[i].Reward2_Value;
                passInfo.rewardType_IAP_Key = ColdSeasonPassServerTable.gangChuldAd;

                uiPassCellContainer[i].gameObject.SetActive(true);
                uiPassCellContainer[i].Initialize(passInfo);
            }
            else
            {
                uiPassCellContainer[i].gameObject.SetActive(false);
            }
        }
        _seletableTab.OnSelect(0);
        // cellParent.transform.localPosition = new Vector3(0f, cellParent.transform.localPosition.y, cellParent.transform.localPosition.z);
    }

    public void OnClickAllReceiveButton()
    {
        splitData_Free = GetSplitData(ColdSeasonPassServerTable.gangChulFree);
        splitData_Ad = GetSplitData(ColdSeasonPassServerTable.gangChuldAd);

        var tableData = TableManager.Instance.GCPass.dataArray;

        int rewardedNum = 0;

        List<int> typeList = new List<int>();
        
        string free = ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChulFree].Value;
        string ad = ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChuldAd].Value;


        for (int i = 0; i < tableData.Length; i++)
        {
            bool canGetReward = CanGetReward(tableData[i].Unlockdamage);

            if (canGetReward == false) break;

            //무료보상
            if (HasReward(splitData_Free, tableData[i].Id) == false)
            {
                free += $",{tableData[i].Id}";
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Reward1, tableData[i].Reward1_Value);
                rewardedNum++;
                if(!typeList.Contains(tableData[i].Reward1))
                {
                    typeList.Add(tableData[i].Reward1);
                }
            }

            //유로보상
            if (HasPassItem() && HasReward(splitData_Ad, tableData[i].Id) == false)
            {
                ad += $",{tableData[i].Id}";
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Reward2, tableData[i].Reward2_Value);
                rewardedNum++;
                
                if(!typeList.Contains(tableData[i].Reward2))
                {
                    typeList.Add(tableData[i].Reward2);
                }
            }
        }

        if (rewardedNum > 0)
        {
            ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChulFree].Value = free;
            ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChuldAd].Value = ad;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param goodsParam = new Param();

            var e = typeList.GetEnumerator();
            while (e.MoveNext())
            {
                goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)e.Current), ServerData.goodsTable.GetTableData((Item_Type)e.Current).Value);
            }
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            Param passParam = new Param();

            passParam.Add(ColdSeasonPassServerTable.gangChulFree, ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChulFree].Value);
            passParam.Add(ColdSeasonPassServerTable.gangChuldAd, ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.gangChuldAd].Value);

            transactions.Add(TransactionValue.SetUpdate(ColdSeasonPassServerTable.tableName, ColdSeasonPassServerTable.Indate, passParam));

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "보상을 전부 수령했습니다", null); 
                //LogManager.Instance.SendLogType("GCPass", "A", "A");
            });
        }
        else
        {
            PopupManager.Instance.ShowAlarmMessage("수령할 보상이 없습니다.");
        }

    }

    public bool CanGetReward(double require)
    {
        var gangChulScore = double.Parse(ServerData.bossServerTable.TableDatas["boss20"].score.Value);
        return gangChulScore >= require;
    }

    private bool HasReward(List<int> splitData, int id)
    {
        return splitData.Contains(id);
    }

    public List<int> GetSplitData(string key)
    {
        List<int> returnValues = new List<int>();

        var splits = ServerData.coldSeasonPassServerTable.TableDatas[key].Value.Split(',');

        for (int i = 0; i < splits.Length; i++)
        {
            if (int.TryParse(splits[i], out var result))
            {
                returnValues.Add(result);
            }
        }

        return returnValues;
    }

    private bool HasPassItem()
    {
        bool hasIapProduct = ServerData.iapServerTable.TableDatas["gcpass"].buyCount.Value > 0;

        return hasIapProduct;
    }
}
