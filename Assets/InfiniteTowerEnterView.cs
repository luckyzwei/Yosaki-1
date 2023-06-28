﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiRewardView;

using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class InfiniteTowerEnterView : MonoBehaviour
{
    [SerializeField]
    private DungeonRewardView dungeonRewardView;

    [SerializeField]
    private TextMeshProUGUI currentStageText;

    [SerializeField]
    private GameObject normalRoot;

    [SerializeField]
    private GameObject allClearRoot;

    [SerializeField]
    private GameObject startButtonRoot;

    [SerializeField]
    private Toggle towerAutoMode;

    public void OnAutoToggleChanged(bool onOff)
    {
        UiLastContentsFunc.AutoInfiniteTower = onOff;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(SettingKey.towerAutoMode) == false)
            PlayerPrefs.SetInt(SettingKey.towerAutoMode, 1);     
        
        towerAutoMode.isOn = PlayerPrefs.GetInt(SettingKey.towerAutoMode) == 1;
    }
    public void AutoModeOnOff(bool on)
    {
        if (on)
        {
            SoundManager.Instance.PlayButtonSound();
        }

        SettingData.towerAutoMode.Value = on ? 1 : 0;
    }

    void OnEnable()
    {
        SetStageText();
        SetReward();
    }

    private bool IsAllClear()
    {
        int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value;

        return currentFloor >= TableManager.Instance.TowerTableData.Count;
    }

    private void SetStageText()
    {
        if (IsAllClear() == false)
        {
            int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value;
            currentStageText.SetText($"{currentFloor + 1}층 도전");
        }
        else
        {
            currentStageText.SetText($"아직 발견되지 않은 구역 입니다.");
        }

    }

    private void SetReward()
    {
        bool isAllClear = IsAllClear();

        normalRoot.SetActive(isAllClear == false);
        allClearRoot.SetActive(isAllClear == true);
        startButtonRoot.SetActive(isAllClear == false);

        if (isAllClear == false)
        {
            int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value;

            if (TableManager.Instance.TowerTableData.TryGetValue(currentFloor, out var towerTableData) == false)
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"잘못된 데이터 idx : {towerTableData}", null);
                return;
            }

            List<RewardData> rewardDatas = new List<RewardData>();

            var rewardData = new RewardData((Item_Type)towerTableData.Rewardtype, towerTableData.Rewardvalue);

            rewardDatas.Add(rewardData);

            dungeonRewardView.Initalize(rewardDatas);
        }


    }
}
