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
            killCountText.SetText($"월간 출석일 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
