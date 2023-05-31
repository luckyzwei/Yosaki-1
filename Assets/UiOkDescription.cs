﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiOkDescription : MonoBehaviour
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
        currentIdx = PlayerStats.GetOkGrade();

        Initialize(currentIdx);
    }

    public void Initialize(int idx)
    {
        if (idx == -1) idx = 0;

        var tableData = TableManager.Instance.okTable.dataArray[idx];

        unlockDesc.SetText($"{Utils.ConvertBigNumForRewardCell(tableData.Score)}");

        equipFrame.gameObject.SetActive(idx == PlayerStats.GetOkGrade());

        gradeText.SetText($"{idx + 1}단계");


        abilDescription.SetText($"{CommonString.GetStatusName(StatusType.SuperCritical4DamPer)}{Utils.ConvertBigNum(tableData.Abilvalue0 * 100f)}");
        

        

        if (idx/3 < 5)
        {
            image.sprite = Resources.Load<Sprite>($"Ok/{idx / 3}");
        }
        else
        {
            image.sprite = Resources.Load<Sprite>($"Ok/5");
        }
    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.okTable.dataArray.Length - 1);

        Initialize(currentIdx);

        if (currentIdx == -1)
        {
            PopupManager.Instance.ShowAlarmMessage("처음 단계 입니다!");
        }
    }

    public void OnClickRightButton()
    {
        if (currentIdx == TableManager.Instance.okTable.dataArray.Length - 1)
        {
            PopupManager.Instance.ShowAlarmMessage("마지막 단계입니다!");
        }

        currentIdx++;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.okTable.dataArray.Length - 1);

        Initialize(currentIdx);

    }
}
