using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TMPro;
using UniRx;
using UnityEngine;
public class UiStartObject : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private string userInfoKey;

    [SerializeField]
    private string goodsKey;

    [SerializeField]
    private TextMeshProUGUI description;
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[userInfoKey].AsObservable().Subscribe(e =>
        {
            
            rootObject.SetActive(e == 0);
            
        }).AddTo(this);
    }

    
    public void OnClickStartButton()
    {
        if (ServerData.userInfoTable.TableDatas[userInfoKey].Value != 0) return;

        List<TransactionValue> transactions = new List<TransactionValue>();

        ServerData.goodsTable.TableDatas[GoodsTable.SealWeaponClear].Value += GameBalance.SealSwordTicketDailyGetAmount;
        ServerData.userInfoTable.TableDatas[userInfoKey].Value = 1;
        
        Param goodsParam = new Param();
        goodsParam.Add(goodsKey,ServerData.goodsTable.TableDatas[goodsKey].Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName,GoodsTable.Indate,goodsParam));

        Param userinfoParam = new Param();
        userinfoParam.Add(userInfoKey,ServerData.userInfoTable.TableDatas[userInfoKey].Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userinfoParam));
        
        ServerData.SendTransaction(transactions,
            successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
                    $"???이 시작됐습니다!\n매일 자동으로 {CommonString.GetItemName(Item_Type.SealWeaponClear)}를 {GameBalance.FoxTowerTicketDailyGetAmount}개씩 획득 합니다!", null);
            });
    }
}
