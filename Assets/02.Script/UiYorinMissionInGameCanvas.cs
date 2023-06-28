using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiYorinMissionInGameCanvas : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.SetActive(IsAllClear()==false);


    }

    private bool IsAllClear()
    {
        var tabledata = TableManager.Instance.YorinMission.dataArray;

        //출석 7일 수령 x 
        if (ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.yorinAttendRewarded).Value < 7) return false; 
        for (int i = 0; i < tabledata.Length; i++)
        {
            //깬것
            if (ServerData.yorinMissionServerTable.TableDatas[tabledata[i].Stringid].rewardCount.Value > 0) continue;
            //안깬것
            else
            {
                return false;
            }
        }

        return true;
    }
}
