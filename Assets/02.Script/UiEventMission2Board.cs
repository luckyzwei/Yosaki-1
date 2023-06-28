﻿                                                         using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiEventMission2Board : MonoBehaviour
{
 

    private void OnEnable()
    {
        CheckEventEnd();
    }

    private void CheckEventEnd()
    {
        var severTime = ServerData.userInfoTable.currentServerTime;

        if (severTime.Month >= 9)
        {
            this.gameObject.SetActive(false);
            PopupManager.Instance.ShowAlarmMessage("이벤트가 종료됐습니다.");
            return;
        }
    }


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value += 100;
        }

    }
#endif

}
