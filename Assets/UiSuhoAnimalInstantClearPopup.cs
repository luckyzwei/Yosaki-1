using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiSuhoAnimalInstantClearPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rewardDescription;

    [SerializeField]
    private TextMeshProUGUI allRewardDescription;

    [SerializeField]
    private UiAnimalView petIcon;

    [SerializeField]
    private GameObject notHasObject;

    [SerializeField]
    private GameObject hasObject;

    private void Start()
    {
        SetAllRewardDescription();
    }

    private void SetAllRewardDescription()
    {
        var tableData = TableManager.Instance.suhoPetTable.dataArray;

        string description = string.Empty;

        for (int i = 0; i < tableData.Length; i++)
        {
            description += $"{tableData[i].Id+1}단계({tableData[i].Name}) 1회 소탕시 {tableData[i].Sweepvalue}개 획득";

            if (i != tableData.Length - 1)
            {
                description += "\n";
            }
        }

        allRewardDescription.SetText(description);
    }

    private void OnEnable()
    {
        UpdateUi();
    }

    private void UpdateUi()
    {
        notHasObject.gameObject.SetActive(false);
        hasObject.gameObject.SetActive(false);

        int lastIdx = ServerData.suhoAnimalServerTable.GetLastPetId();

        if (lastIdx == -1)
        {
            notHasObject.gameObject.SetActive(true);
            return;
        }

        hasObject.gameObject.SetActive(true);
        
        var petTableData = TableManager.Instance.suhoPetTable.dataArray[lastIdx];
        
        petIcon.Initialize(petTableData);

        rewardDescription.SetText(
            $"현재 {petTableData.Id + 1}단계 1회 소탕시 {CommonString.GetItemName(Item_Type.SuhoPetFeed)} {petTableData.Sweepvalue}개 획득!");
    }
    
}