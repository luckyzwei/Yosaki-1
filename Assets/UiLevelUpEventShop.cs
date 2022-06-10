﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Purchasing;
using BackEnd;
using TMPro;

public class UiLevelUpEventShop : SingletonMono<UiLevelUpEventShop>
{
    [SerializeField]
    private UiIapItemCell iapCellPrefab;

    [SerializeField]
    private Transform cellRoot;

    [SerializeField]
    private TextMeshProUGUI topClearStageId;

    private void Start()
    {
        Initialize();
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.topClearStageId].AsObservable().Subscribe(e =>
        {
            topClearStageId.SetText($"최고 스테이지 : {e + 1}");
        }).AddTo(this);
    }

    private void Initialize()
    {
        var e = TableManager.Instance.InAppPurchaseData.GetEnumerator();

        while (e.MoveNext())
        {
            if (e.Current.Value.SHOPCATEGORY == ShopCategory.LevelUp)
            {
                var cell = Instantiate<UiIapItemCell>(iapCellPrefab, cellRoot);
                cell.Initialize(e.Current.Value);
                cell.gameObject.SetActive(true);
            }
        }
    }

    public void GetPackageItem(string productId)
    {

        if (TableManager.Instance.InAppPurchaseData.TryGetValue(productId, out var tableData) == false)
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 상품 id {productId}", null);
            return;
        }
        else
        {
            // PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{tableData.Title} 구매 성공!", null);
        }

        if (tableData.SELLWHERE != SellWhere.Shop) return;
        if (tableData.BUYTYPE == BuyType.Pension) return;

        //아이템 수령처리
        Param goodsParam = null;
        Param costumeParam = null;
        Param petParam = null;
        Param iapParam = new Param();
        Param iapTotalParam = new Param();
        Param magicStoneBuffParam = null;
        Param weaponParam = null;
        Param norigaeParam = null;
        Param skillParam = null;

        List<TransactionValue> transactionList = new List<TransactionValue>();

        string logString = string.Empty;

        for (int i = 0; i < tableData.Rewardtypes.Length; i++)
        {
            Item_Type rewardType = (Item_Type)tableData.Rewardtypes[i];
            int rewardAmount = tableData.Rewardvalues[i];

            if (rewardType.IsGoodsItem())
            {
                AddGoodsParam(ref goodsParam, rewardType, rewardAmount);
            }
        }

        //재화
        if (goodsParam != null)
        {
            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        }

        if (ServerData.iapServerTable.TableDatas.ContainsKey(tableData.Productid) == false)
        {
            Debug.LogError($"@@@product Id {tableData.Productid}");
            return;
        }
        else
        {
            ServerData.iapServerTable.TableDatas[tableData.Productid].buyCount.Value++;
            ServerData.iAPServerTableTotal.TableDatas[tableData.Productid].buyCount.Value++;
        }

        iapParam.Add(tableData.Productid, ServerData.iapServerTable.TableDatas[tableData.Productid].ConvertToString());

        iapTotalParam.Add(tableData.Productid, ServerData.iAPServerTableTotal.TableDatas[tableData.Productid].ConvertToString());

        transactionList.Add(TransactionValue.SetUpdate(IAPServerTable.tableName, IAPServerTable.Indate, iapParam));

        transactionList.Add(TransactionValue.SetUpdate(IAPServerTableTotal.tableName, IAPServerTableTotal.Indate, iapTotalParam));

        ServerData.SendTransaction(transactionList, successCallBack: () =>
          {
              PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "보상 획득 완료!", null);
          });
    }

    public void AddGoodsParam(ref Param param, Item_Type type, int amount)
    {
        if (param == null)
        {
            param = new Param();
        }

        switch (type)
        {
            case Item_Type.Jade:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += amount;
                    param.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
                }
                break;
            case Item_Type.GrowthStone:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value += amount;
                    param.Add(GoodsTable.GrowthStone, ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value);
                }
                break;
            case Item_Type.Ticket:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value += amount;
                    param.Add(GoodsTable.Ticket, ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value);
                }
                break;
            case Item_Type.Marble:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += amount;
                    param.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
                }
                break;
            case Item_Type.Songpyeon:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Songpyeon).Value += amount;
                    param.Add(GoodsTable.Songpyeon, ServerData.goodsTable.GetTableData(GoodsTable.Songpyeon).Value);
                }
                break;
            case Item_Type.RelicTicket:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value += amount;
                    param.Add(GoodsTable.RelicTicket, ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value);
                }
                break;
            case Item_Type.Event_Item_0:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value += amount;
                    param.Add(GoodsTable.Event_Item_0, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value);
                }
                break;

            case Item_Type.Event_Item_1:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value += amount;
                    param.Add(GoodsTable.Event_Item_1, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value);
                }
                break;   
            case Item_Type.Event_Item_Summer:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_Summer).Value += amount;
                    param.Add(GoodsTable.Event_Item_Summer, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_Summer).Value);
                }
                break;

            case Item_Type.SulItem:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value += amount;
                    param.Add(GoodsTable.SulItem, ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value);
                }
                break;

            case Item_Type.FeelMulStone:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value += amount;
                    param.Add(GoodsTable.FeelMulStone, ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value);
                }
                break;

            case Item_Type.Asura0:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value += amount;
                    param.Add(GoodsTable.Asura0, ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value);
                }
                break;

            case Item_Type.Asura1:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value += amount;
                    param.Add(GoodsTable.Asura1, ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value);
                }
                break;

            case Item_Type.Asura2:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value += amount;
                    param.Add(GoodsTable.Asura2, ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value);
                }
                break;

            case Item_Type.Asura3:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value += amount;
                    param.Add(GoodsTable.Asura3, ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value);
                }
                break;
            case Item_Type.Asura4:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value += amount;
                    param.Add(GoodsTable.Asura4, ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value);
                }
                break;

            case Item_Type.Asura5:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value += amount;
                    param.Add(GoodsTable.Asura5, ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value);
                }
                break;
            case Item_Type.Aduk:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value += amount;
                    param.Add(GoodsTable.Aduk, ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value);
                }
                break;

            case Item_Type.LeeMuGiStone:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value += amount;
                    param.Add(GoodsTable.Aduk, ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value);
                }
                break;

            //
            case Item_Type.SinSkill0:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value += amount;
                    param.Add(GoodsTable.SinSkill0, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value);
                }
                break;
            case Item_Type.SinSkill1:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value += amount;
                    param.Add(GoodsTable.SinSkill1, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value);
                }
                break;
            case Item_Type.SinSkill2:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value += amount;
                    param.Add(GoodsTable.SinSkill2, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value);
                }
                break;
            case Item_Type.SinSkill3:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value += amount;
                    param.Add(GoodsTable.SinSkill3, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value);
                }
                break;     
            case Item_Type.NataSkill:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value += amount;
                    param.Add(GoodsTable.NataSkill, ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value);
                }
                break;
            //

            case Item_Type.SmithFire:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value += amount;
                    param.Add(GoodsTable.SmithFire, ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value);
                }
                break;

            case Item_Type.StageRelic:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value += amount;
                    param.Add(GoodsTable.StageRelic, ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value);
                }
                break;

            case Item_Type.PeachReal:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += amount;
                    param.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value);
                }
                break;
            case Item_Type.GuildReward:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += amount;
                    param.Add(GoodsTable.GuildReward, ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value);
                }
                break;    
            case Item_Type.SP:
                {
                    ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value += amount;
                    param.Add(GoodsTable.SwordPartial, ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value);
                }
                break;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.statusTable.GetTableData(StatusTable.Level).Value += 10000;
        }
    }

#endif
}
