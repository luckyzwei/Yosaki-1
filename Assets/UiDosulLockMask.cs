using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UniRx;
using UnityEngine;
using TMPro;

public class UiDosulLockMask : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private TextMeshProUGUI lockDescription;
    
    void Start()
    {
        Subscribe();
        
        lockDescription.SetText($"매일 {CommonString.GetItemName(Item_Type.DosulClear)}을 {GameBalance.dailyDosulClearTicketGetValue}개 획득 합니다." +
                                $"\n{CommonString.GetItemName(Item_Type.DosulClear)}으로 {CommonString.GetItemName(Item_Type.DosulGoods)}를 획득해\n도술 능력을 얻어보세요!");
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulStart].AsObservable().Subscribe(e =>
        {
            
            rootObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    public void OnClickUnlockButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.topClearStageId].Value < GameBalance.dosulUnlockStage)
        {
            PopupManager.Instance.ShowAlarmMessage($"{GameBalance.dosulUnlockStage}스테이지 이상부터 가능합니다!");
            return;
        }
        
        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.DosulClear].Value += GameBalance.dailyDosulClearTicketGetValue;
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulStart].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.DosulClear,ServerData.goodsTable.TableDatas[GoodsTable.DosulClear].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable_2.dosulStart,ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulStart].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName,UserInfoTable_2.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.DosulClear)}를 {GameBalance.dailyDosulClearTicketGetValue}개씩 획득 합니다!", null);
            });
        
    }
    
}
