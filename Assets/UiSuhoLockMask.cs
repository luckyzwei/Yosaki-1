using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;

public class UiSuhoLockMask : MonoBehaviour
{
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.suhoAnimalStart].AsObservable().Subscribe(e =>
        {
            
            this.gameObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    public void OnClickStartButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.suhoAnimalStart].Value != 0) return;

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeedClear].Value += GameBalance.DailyPetFeedClearGetValue;
        ServerData.userInfoTable.TableDatas[UserInfoTable.suhoAnimalStart].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.SuhoPetFeedClear,ServerData.goodsTable.TableDatas[GoodsTable.SuhoPetFeedClear].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable.suhoAnimalStart,ServerData.userInfoTable.TableDatas[UserInfoTable.suhoAnimalStart].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"수호요괴전이 시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.SuhoPetFeedClear)}를 {GameBalance.DailyPetFeedClearGetValue}개씩 획득 합니다!", null);
            });
    }
    
    
    
}