using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;

public class UiSealSwordStartObject : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.sealSwordStart].AsObservable().Subscribe(e =>
        {
            
            rootObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    
    public void OnClickStartButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.sealSwordStart].Value != 0) return;

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.SealWeaponClear].Value += GameBalance.SealSwordTicketDailyGetAmount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.sealSwordStart].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.SealWeaponClear,ServerData.goodsTable.TableDatas[GoodsTable.SealWeaponClear].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable.sealSwordStart,ServerData.userInfoTable.TableDatas[UserInfoTable.sealSwordStart].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.SealWeaponClear)}를 {GameBalance.SealSwordTicketDailyGetAmount}개씩 획득 합니다!", null);
            });
    }
}
