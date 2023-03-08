using Photon.Pun.Demo.Cockpit;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiGyungRockMassgeBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFloorDescription;
    [SerializeField] private TextMeshProUGUI currentFloorAbil;
    [SerializeField] private TextMeshProUGUI totalAbil;
    [SerializeField] private TextMeshProUGUI buttonDescription;
    [SerializeField] private TextMeshProUGUI nextFloorDescription;

    [SerializeField] private List<Image> markLists;
    [SerializeField] private List<Animator> markList_Anim;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        totalAbil.SetText(
            ($"{CommonString.GetStatusName((StatusType.SuperCritical8DamPer))} 총 {PlayerStats.GetSuperCritical8DamPer() * 100f}% 적용됨"));

        int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx5).Value;

        currentFloorDescription.SetText($"{currentFloor}단계");

        currentFloorAbil.SetText($"혈자리 {currentFloor}개 개방됨");

        if (currentFloor >= TableManager.Instance.gyungRockTowerTable.dataArray.Length)
        {
            buttonDescription.SetText(($"최고단계"));
        }
        else
        {
            buttonDescription.SetText($"{currentFloor + 1}단계 도전");
        }

        for (int i = 0; i < markLists.Count; i++)
        {
            markLists[i].color = currentFloor > i ? Color.green : Color.red;

            markList_Anim[i].enabled = currentFloor > i;
        }
    }

    public void OnClickEnterButton()
    {
        int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx5).Value;

        if (currentFloor < 0 || currentFloor >= TableManager.Instance.gyungRockTowerTable.dataArray.Length)
        {
            PopupManager.Instance.ShowAlarmMessage(("최고 단계 입니다!"));
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 하시겠습니까?", () =>
        {
            GameManager.Instance.LoadContents(GameManager.ContentsType.GyungRockTower);
        }, () => { });
    }
}