﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHellGodDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI immuneDescription;

    [SerializeField]
    private TextMeshProUGUI gradeText;

    [SerializeField]
    private TextMeshProUGUI unlockDesc;

    [SerializeField]
    private GameObject equipFrame;

    [SerializeField]
    private Image image;

    private int currentIdx;

    private void Start()
    {
        currentIdx = PlayerStats.GetHellGodGrade();

        Initialize(currentIdx);
    }

    public void Initialize(int idx)
    {
        if (idx == -1) idx = 0;

        var tableData = TableManager.Instance.TestHell.dataArray[idx];

        unlockDesc.SetText($"{Utils.ConvertBigNumForRewardCell(tableData.Score)}");

        equipFrame.gameObject.SetActive(idx == PlayerStats.GetHellGodGrade());

        gradeText.SetText($"{idx + 1}단계");


        abilDescription.SetText($"불멸석 능력치 효과 {Utils.ConvertBigNum(tableData.Abilvalue0*100)}% 증가\n" +
                                $"불멸석 방치 획득량 {Utils.ConvertBigNum(tableData.Abilvalue1*100)}% 증가");
        


        image.sprite = Resources.Load<Sprite>($"TestHell/0");
    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.TestHell.dataArray.Length - 1);

        Initialize(currentIdx);

        if (currentIdx == -1)
        {
            PopupManager.Instance.ShowAlarmMessage("처음 단계 입니다!");
        }
    }

    public void OnClickRightButton()
    {
        if (currentIdx == TableManager.Instance.TestHell.dataArray.Length - 1)
        {
            PopupManager.Instance.ShowAlarmMessage("마지막 단계입니다!");
        }

        currentIdx++;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.TestHell.dataArray.Length - 1);

        Initialize(currentIdx);

    }
}
