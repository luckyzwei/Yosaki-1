using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using System;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiSinsunMarbleBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilDescription;


    private void OnEnable()
    {
        UpdateAbilText1((int)ServerData.goodsTable.GetTableData(GoodsTable.SinsunTreasure).Value);
    }

    private void UpdateAbilText1(int currentLevel)
    {
        var tableData = TableManager.Instance.SinsunTreasureAbilBase.dataArray;

        string abilDesc = string.Empty;

        for (int i = 0; i < tableData.Length; i++)
        {
            StatusType type = (StatusType)tableData[i].Abiltype;
            
            abilDesc += $"{CommonString.GetStatusName(type)} {PlayerStats.GetSinsunTreasureAbilHasEffect(type) * 100f}\n";
        }

        abilDescription.SetText(abilDesc);
    }
}