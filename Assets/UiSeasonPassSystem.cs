﻿using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UiSeasonPassSystem : MonoBehaviour
{
    [SerializeField]
    private UiSeasonPassCell uiPassCellPrefab;

    [SerializeField]
    private Transform cellParent;

    private List<UiSeasonPassCell> uiPassCellContainer = new List<UiSeasonPassCell
>();

    private ObscuredString passShopId;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.userInfoTable.GetTableData(UserInfoTable.killCountTotalSeason2).Value += 1000000;
        }
    }
#endif

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        var tableData = TableManager.Instance.newSemesterPass.dataArray;

        int interval = tableData.Length - uiPassCellContainer.Count;

        for (int i = 0; i < interval; i++)
        {
            var prefab = Instantiate<UiSeasonPassCell>(uiPassCellPrefab, cellParent);
            uiPassCellContainer.Add(prefab);
        }

        for (int i = 0; i < uiPassCellContainer.Count; i++)
        {
            if (i < tableData.Length)
            {
                var passInfo = new PassInfo();

                passInfo.require = tableData[i].Unlockamount;
                passInfo.id = tableData[i].Id;

                passInfo.rewardType_Free = tableData[i].Reward1;
                passInfo.rewardTypeValue_Free = tableData[i].Reward1_Value;
                passInfo.rewardType_Free_Key = ColdSeasonPassServerTable.seasonFree;

                passInfo.rewardType_IAP = tableData[i].Reward2;
                passInfo.rewardTypeValue_IAP = tableData[i].Reward2_Value;
                passInfo.rewardType_IAP_Key = ColdSeasonPassServerTable.seasonAd;

                uiPassCellContainer[i].gameObject.SetActive(true);
                uiPassCellContainer[i].Initialize(passInfo);
            }
            else
            {
                uiPassCellContainer[i].gameObject.SetActive(false);
            }
        }

        // cellParent.transform.localPosition = new Vector3(0f, cellParent.transform.localPosition.y, cellParent.transform.localPosition.z);
    }

    public void OnClickAllReceiveButton()
    {
        string freeKey = ColdSeasonPassServerTable.seasonFree;
        string adKey = ColdSeasonPassServerTable.seasonAd
;

        List<int> splitData_Free = GetSplitData(ColdSeasonPassServerTable.seasonFree);
        List<int> splitData_Ad = GetSplitData(ColdSeasonPassServerTable.seasonAd
);

        List<int> rewardTypeList = new List<int>();

        var tableData = TableManager.Instance.newSemesterPass.dataArray;

        int rewardedNum = 0;

        string free = ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonFree].Value;
        string ad = ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonAd
].Value;

        bool hasCostumeItem = false;

        for (int i = 0; i < tableData.Length; i++)
        {
            bool canGetReward = CanGetReward(tableData[i].Unlockamount);

            if (canGetReward == false) break;

            //무료보상
            if (HasReward(splitData_Free, tableData[i].Id) == false)
            {
                if (((Item_Type)(tableData[i].Reward1)).IsCostumeItem())
                {
                    hasCostumeItem = true;
                    break;
                }

                free += $",{tableData[i].Id}";
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Reward1, tableData[i].Reward1_Value);
                if (rewardTypeList.Contains(tableData[i].Reward1) == false)
                {
                    rewardTypeList.Add(tableData[i].Reward1);
                }
                rewardedNum++;
            }

            //유료보상
            if (HasPassItem() && HasReward(splitData_Ad, tableData[i].Id) == false)
            {
                if (((Item_Type)(tableData[i].Reward2)).IsCostumeItem())
                {
                    hasCostumeItem = true;
                    break;
                }

                ad += $",{tableData[i].Id}";
                ServerData.AddLocalValue((Item_Type)(int)tableData[i].Reward2, tableData[i].Reward2_Value);

                if (rewardTypeList.Contains(tableData[i].Reward1) == false)
                {
                    rewardTypeList.Add(tableData[i].Reward1);
                }

                rewardedNum++;
            }
        }

        if (hasCostumeItem)
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "외형 아이템은 직접 수령해야 합니다.", null);
            return;
        }

        if (rewardedNum > 0)
        {
            ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonFree].Value = free;
            ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonAd].Value = ad;

            List<TransactionValue> transactions = new List<TransactionValue>();
            var e = rewardTypeList.GetEnumerator();

            Param goodsParam = new Param();
            while (e.MoveNext())
            {
                goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)e.Current), ServerData.goodsTable.GetTableData((Item_Type)e.Current).Value);
            }
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            Param passParam = new Param();

            passParam.Add(ColdSeasonPassServerTable.seasonFree, ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonFree].Value);
            passParam.Add(ColdSeasonPassServerTable.seasonAd, ServerData.coldSeasonPassServerTable.TableDatas[ColdSeasonPassServerTable.seasonAd].Value);

            transactions.Add(TransactionValue.SetUpdate(ColdSeasonPassServerTable.tableName, ColdSeasonPassServerTable.Indate, passParam));

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "보상을 전부 수령했습니다", null);
                //LogManager.Instance.SendLogType("coldSeasonPass", "A", "A");
            });
        }
        else
        {
            PopupManager.Instance.ShowAlarmMessage("수령할 보상이 없습니다.");
        }
    }


    private bool CanGetReward(int require)
    {
        int killCountTotal = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.killCountTotalSeason2).Value;
        return killCountTotal >= require;
    }
    public bool HasReward(List<int> splitData, int id)
    {
        return splitData.Contains(id);
    }

    private bool HasPassItem()
    {
        bool hasIapProduct = ServerData.iapServerTable.TableDatas[UiSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0;

        return hasIapProduct;
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

    public void RefreshCells()
    {
        for (int i = 0; i < uiPassCellContainer.Count; i++)
        {
            uiPassCellContainer[i].RefreshParent();
        }
    }
}
