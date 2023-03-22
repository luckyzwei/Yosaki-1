                                                         using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiEventMission2Board : MonoBehaviour
{
    [SerializeField]
    private UiEventMission2Cell missionCell;

    [SerializeField]
    private Transform cellParent;

    private Dictionary<int, UiEventMission2Cell> cellContainer = new Dictionary<int, UiEventMission2Cell>();


    private void OnEnable()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateChun).Value > 0)
        {
            string key = TableManager.Instance.EventMissionDatas[(int)EventMissionKey.S_ClearChunFlower].Stringid;
            ServerData.eventMissionTable.UpdateMissionClearToCount(key, 1);
        }    
        CheckEventEnd();
    }

    private void CheckEventEnd()
    {
        var severTime = ServerData.userInfoTable.currentServerTime;

        if (severTime.Month >= 5)
        {
            this.gameObject.SetActive(false);
            PopupManager.Instance.ShowAlarmMessage("이벤트가 종료됐습니다.");
            return;
        }
    }

    private void Awake()
    {
        Initialize();
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

    private void Initialize()
    {
        var tableData = TableManager.Instance.EventMission.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].EVENTMISSIONTYPE != EventMissionType.SECOND) continue;
            var cell = Instantiate<UiEventMission2Cell>(missionCell, cellParent);

            cell.Initialize(tableData[i]);

            cellContainer.Add(tableData[i].Id, cell);
        }
    }
}
