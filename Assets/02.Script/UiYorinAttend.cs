using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BackEnd;
using UnityEngine.Serialization;

public class UiYorinAttend : MonoBehaviour
{
    [SerializeField] private YorinAttendCell yorinAttendCell;

    [SerializeField] private Transform cellParentTransform;

    [SerializeField] private TextMeshProUGUI day;
    
    private void Start()
    {
        Initialize();
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_100Day).AsObservable().Subscribe(e =>
        {
            day.SetText($"출석일 : {Mathf.Min((int)e, 7)}일");
        });
    }
    private void Initialize()
    {
        var tableData = TableManager.Instance.YorinAttend.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            var prefab = Instantiate<YorinAttendCell>(yorinAttendCell, cellParentTransform);
            prefab.Initialize(tableData[i]);
        }
    }
}
