using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiDarkCleansBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilDescription;


    private void OnEnable()
    {
        UpdateAbilText1((int)ServerData.goodsTable.GetTableData(GoodsTable.DarkTreasure).Value);
    }

    private void UpdateAbilText1(int currentLevel)
    {
        var tableData = TableManager.Instance.DarkTreasureAbilBase.dataArray;

        string abilDesc = string.Empty;

        for (int i = 0; i < tableData.Length; i++)
        {
            StatusType type = (StatusType)tableData[i].Abiltype;
            
            abilDesc += $"{CommonString.GetStatusName(type)} {PlayerStats.GetDarkTreasureAbilHasEffect(type) * 100f}\n";
        }

        abilDescription.SetText(abilDesc);
    }
}