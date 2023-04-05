using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;
public class UiGuildTowerStartObject : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.guildTowerStart].AsObservable().Subscribe(e =>
        {
            
            rootObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    
    public void OnClickStartButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.guildTowerStart].Value != 0) return;

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.GuildTowerClearTicket].Value += GameBalance.GuildTowerTicketDailyGetAmount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.guildTowerStart].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.GuildTowerClearTicket,ServerData.goodsTable.TableDatas[GoodsTable.GuildTowerClearTicket].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable.guildTowerStart,ServerData.userInfoTable.TableDatas[UserInfoTable.guildTowerStart].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"전갈굴이 시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.GuildTowerClearTicket)}를 {GameBalance.GuildTowerTicketDailyGetAmount}개씩 획득 합니다!", null);
            });
    }
}
