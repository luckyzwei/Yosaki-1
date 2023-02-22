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
        CheckEventEnd();
    }

    private void CheckEventEnd()
    {
        var severTime = ServerData.userInfoTable.currentServerTime;
#if UNITY_EDITOR
#else
        if (severTime.Month == 1 && severTime.Day < 20)
        {
            this.gameObject.SetActive(false);
            PopupManager.Instance.ShowAlarmMessage("1월 20일 부터 이벤트 시작!");
            return;
        }
#endif
        if (severTime.Month >= 3)
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
