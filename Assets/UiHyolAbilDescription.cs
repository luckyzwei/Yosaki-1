using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiHyolAbilDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI description_total;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        description_total.SetText(($"{PlayerStats.GetSuperCritical8DamPer()}"));
    }

    public void OnClick(int idx)
    {
        var tableData = TableManager.Instance.gyungRockTowerTable.dataArray;

        if (idx >= tableData.Length) return;

        description.SetText(
            $"{CommonString.GetStatusName((StatusType)tableData[idx].Rewardtype)}{tableData[idx].Rewardvalue * 100f}%");
    }
}