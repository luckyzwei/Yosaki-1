﻿using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UiChuseokAttenSystem : MonoBehaviour
{
    [SerializeField]
    private UiChuseokAttenCell uiPassCellPrefab;

    [SerializeField]
    private Transform cellParent;

    private List<UiChuseokAttenCell> uiPassCellContainer = new List<UiChuseokAttenCell>();

    private ObscuredString passShopId;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.userInfoTable.GetTableData(UserInfoTable.attenCountChuSeok).Value += 1;
        }
    }
#endif

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        var tableData = TableManager.Instance.chuSeokAtten.dataArray;

        int interval = tableData.Length - uiPassCellContainer.Count;

        for (int i = 0; i < interval; i++)
        {
            var prefab = Instantiate<UiChuseokAttenCell>(uiPassCellPrefab, cellParent);
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
                passInfo.rewardType_Free_Key = ChildPassServerTable.childFree_Atten;

                passInfo.rewardType_IAP = tableData[i].Reward2;
                passInfo.rewardTypeValue_IAP = tableData[i].Reward2_Value;
                passInfo.rewardType_IAP_Key = ChildPassServerTable.childAd_Atten;

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
        string freeKey = ChildPassServerTable.childFree_Atten;
        string adKey = ChildPassServerTable.childAd_Atten;

        List<int> splitData_Free = GetSplitData(ChildPassServerTable.childFree_Atten);
        List<int> splitData_Ad = GetSplitData(ChildPassServerTable.childAd_Atten);

        var tableData = TableManager.Instance.chuSeokAtten.dataArray;

        int rewardedNum = 0;

        string free = ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childFree_Atten].Value;
        string ad = ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childAd_Atten].Value;

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
            ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childFree_Atten].Value = free;
            ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childAd_Atten].Value = ad;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
            goodsParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
            goodsParam.Add(GoodsTable.RelicTicket, ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value);
            goodsParam.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value);
            goodsParam.Add(GoodsTable.SmithFire, ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value);
            goodsParam.Add(GoodsTable.SwordPartial, ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value);
            goodsParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);

            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            Param passParam = new Param();

            passParam.Add(ChildPassServerTable.childFree_Atten, ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childFree_Atten].Value);
            passParam.Add(ChildPassServerTable.childAd_Atten, ServerData.childPassServerTable.TableDatas[ChildPassServerTable.childAd_Atten].Value);

            transactions.Add(TransactionValue.SetUpdate(ChildPassServerTable.tableName, ChildPassServerTable.Indate, passParam));

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


    private bool CanGetReward(int require)
    {
        int killCountTotal = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.attenCountChuSeok).Value;
        return killCountTotal >= require;
    }
    public bool HasReward(List<int> splitData, int id)
    {
        return splitData.Contains(id);
    }

    private bool HasPassItem()
    {
        bool hasIapProduct = ServerData.iapServerTable.TableDatas[UiChildPassBuyButton.PassKey].buyCount.Value > 0;

        return hasIapProduct;
    }

    
    public List<int> GetSplitData(string key)
    {
        List<int> returnValues = new List<int>();

        var splits = ServerData.childPassServerTable.TableDatas[key].Value.Split(',');

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
