using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class AttendCountIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.monthAttendCount).AsObservable().Subscribe(e =>
        {
            var serverTime = ServerData.userInfoTable.currentServerTime;
            
            killCountText.SetText($"{serverTime.Month}월 출석일 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
