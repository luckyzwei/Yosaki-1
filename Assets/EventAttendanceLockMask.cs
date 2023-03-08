using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine;

public class EventAttendanceLockMask : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    // Start is called before the first frame update
    
     
    private void OnEnable()
    {
        if (ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value != 0 ||
            ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_Dol).Value > 0
            )
        {
            rootObject.gameObject.SetActive(false);
        }
    }

    public void AttendStartButton()
    {
        if (ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value > 0)
        {
            return;
        }

        ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value = 1;
       

        List<TransactionValue> transactionList = new List<TransactionValue>();

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.EventDice, ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value);

        transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        rootObject.gameObject.SetActive(false);

        ServerData.SendTransaction(transactionList, successCallBack:()=>
        {
            SoundManager.Instance.PlaySound("Reward");
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "이벤트 시작!!", null);
        });
    }
}
