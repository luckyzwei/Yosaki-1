using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;
public class UiFoxTowerStartObject : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.foxTowerStart].AsObservable().Subscribe(e =>
        {
            
            rootObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    
    public void OnClickStartButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.foxTowerStart].Value != 0) return;

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.FoxRelicClearTicket].Value += GameBalance.FoxTowerTicketDailyGetAmount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.foxTowerStart].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.FoxRelicClearTicket,ServerData.goodsTable.TableDatas[GoodsTable.FoxRelicClearTicket].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable.foxTowerStart,ServerData.userInfoTable.TableDatas[UserInfoTable.foxTowerStart].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"여우굴이 시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.FoxRelicClearTicket)}를 {GameBalance.FoxTowerTicketDailyGetAmount}개씩 획득 합니다!", null);
            });
    }
}
