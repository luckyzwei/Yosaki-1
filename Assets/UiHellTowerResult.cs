﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiRewardView;

public class UiHellTowerResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private DungeonRewardView dungeonRewardView;

    [SerializeField]
    private GameObject failObject;
    [SerializeField]
    private TextMeshProUGUI stageChangeText;
    [SerializeField]
    private GameObject stageChangeButton;
    [SerializeField]
    private GameObject successObject;

    public void Initialize(ContentsState state, List<RewardData> rewardDatas) 
    {
        resultText.SetText(GetTitleText(state));

        successObject.SetActive(state == ContentsState.Clear);
        failObject.SetActive(state != ContentsState.Clear);

        dungeonRewardView.gameObject.SetActive(rewardDatas != null);

        if (rewardDatas != null) 
        {
            dungeonRewardView.Initalize(rewardDatas);
        }
    }

    private string GetTitleText(ContentsState contentsState) 
    {
        switch (contentsState)
        {
            case ContentsState.Dead:
                stageChangeText.SetText("재도전");
                return "실패!";
            case ContentsState.TimerEnd:
                stageChangeText.SetText("재도전");
                return "시간초과!";
            case ContentsState.Clear:
                switch (GameManager.contentsType)
                {
                    case GameManager.ContentsType.DokebiTower when (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx3).Value < (TableManager.Instance.towerTable3.dataArray.Length):
                        stageChangeText.SetText("다음 스테이지");
                        break;
                    case GameManager.ContentsType.DokebiTower:
                        stageChangeButton.SetActive(false);
                        break;
                    case GameManager.ContentsType.RoyalTombTower when (int)ServerData.userInfoTable.GetTableData(UserInfoTable.RoyalTombFloorIdx).Value < (TableManager.Instance.royalTombTowerTable.dataArray.Length):
                        stageChangeText.SetText("다음 스테이지");
                        break;
                    case GameManager.ContentsType.RoyalTombTower:
                        stageChangeButton.SetActive(false);
                        break;
                    case GameManager.ContentsType.SinsuTower when (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx6).Value < (TableManager.Instance.sinsuTower.dataArray.Length):
                        stageChangeText.SetText("다음 스테이지");
                        break;
                    case GameManager.ContentsType.SealSwordTower when (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx9).Value < (TableManager.Instance.sealSwordTable.dataArray.Length):
                        stageChangeText.SetText("다음 스테이지");
                        break;
                    case GameManager.ContentsType.SinsuTower:
                        stageChangeButton.SetActive(false);
                        break;
                }
                
                return "클리어!!";
        }

        return "미등록";
    }
}