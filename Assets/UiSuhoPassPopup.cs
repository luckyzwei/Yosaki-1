﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSuhoPassPopup : MonoBehaviour
{

    private void OnEnable()
    {
        var severTime = ServerData.userInfoTable.currentServerTime;
        
        if (severTime.Month >= 6)
        {
            this.gameObject.SetActive(false);
            PopupManager.Instance.ShowAlarmMessage("이벤트가 종료됐습니다.");
            return;
        }
    }
}
