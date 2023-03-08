﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiWeaponEquipmentCollection : MonoBehaviour
{
    [SerializeField]
    private UiWeaponCollectionView weaponCollectionViewPrefab;

    [SerializeField]
    private Transform cellParent;

    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI rewardDescription;


    [SerializeField]
    private Button getWeaponButton;

    [SerializeField]
    private TextMeshProUGUI rewardButtonDescription;

    [SerializeField]
    private TextMeshProUGUI abilList;


    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        UpdateDescription();
    }

    private void Initialize()
    {
        var tableData = TableManager.Instance.WeaponTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if ((tableData[i].WEAPONTYPE == WeaponType.View)||
                (tableData[i].WEAPONTYPE == WeaponType.Basic) )
            {
                continue;
            }
            var cell = Instantiate<UiWeaponCollectionView>(weaponCollectionViewPrefab, cellParent);

            cell.Initialize(tableData[i]);
            //사신수와 사흉수
            if (tableData[i].WEAPONTYPE == WeaponType.HasEffectOnly)
            {
                //사흉수 위치변경
                if (tableData[i].Id == 91 || tableData[i].Id == 92 || tableData[i].Id == 93 || tableData[i].Id == 94)
                {
                    cell.gameObject.transform.SetSiblingIndex(i - 56);
                }
            }
        }
        
    }
    
    private void SetTransform()
    {

    }
    private void UpdateDescription()
    {
        SetAbilText();

        SetRewardText();

    }

    private void SetAbilText()
    {
        
        var tableData = TableManager.Instance.WeaponTable.dataArray;
        
        var serverData = ServerData.weaponTable.TableDatas;
        Dictionary<StatusType, float> rewards = new Dictionary<StatusType, float>();

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].WEAPONTYPE == WeaponType.View) continue;
            if (tableData[i].WEAPONTYPE == WeaponType.Basic) continue;

            StatusType abilType = (StatusType)tableData[i].Collectioneffecttype;

            if (rewards.ContainsKey(abilType) == false)
            {
                var ret = PlayerStats.GetWeaponCollectionHasValue(abilType);
                if (ret != 0)
                {
                    rewards.Add(abilType, ret);
                }
            }
        }
        
        var e = rewards.GetEnumerator();

        string description = "";

        while (e.MoveNext())
        {
            if (Utils.IsPercentStat(e.Current.Key))
            {
                description += $"{CommonString.GetStatusName(e.Current.Key)} {Utils.ConvertBigNum(e.Current.Value * 100f)} 증가\n";
            }
            else
            {
                description += $"{CommonString.GetStatusName(e.Current.Key)} {Utils.ConvertBigNum(e.Current.Value)} 증가\n";
            }
        }

        if (rewards.Count == 0)
        {
            abilDescription.SetText("무기가 없습니다.");
        }
        else
        {
            abilDescription.SetText(description);
        }

        string abils = string.Empty;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].WEAPONTYPE == WeaponType.View) continue;
            if (tableData[i].WEAPONTYPE == WeaponType.Basic) continue;
            if(Utils.IsPercentStat((StatusType)tableData[i].Collectioneffecttype))
            {
                abils += $"{tableData[i].Name} 보유 : {CommonString.GetStatusName((StatusType)tableData[i].Collectioneffecttype)} {Utils.ConvertBigNum(tableData[i].Collectioneffectvalue * 100f)}\n";
            }
            else
            {
                abils += $"{tableData[i].Name} 보유 : {CommonString.GetStatusName((StatusType)tableData[i].Collectioneffecttype)} {Utils.ConvertBigNum(tableData[i].Collectioneffectvalue)}\n";
            }
        }

        abils += "<color=red>모든 효과는 중첩됩니다!</color>";


        abilList.SetText(abils);
    }

    private void SetRewardText()
    {

        var tableData = TableManager.Instance.WeaponTable.dataArray;

        Dictionary<Item_Type, float> rewards = new Dictionary<Item_Type, float>();

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].WEAPONTYPE == WeaponType.View) continue;

            Item_Type rewardType = (Item_Type)tableData[i].Rewardtype0;
            float rewardValue = tableData[i].Rewardvalue0;

            if (rewards.ContainsKey(rewardType) == false)
            {
                rewards.Add(rewardType, 0f);
            }

            rewards[rewardType] += rewardValue;
        }

        var e = rewards.GetEnumerator();

        string description = "";

        while (e.MoveNext())
        {
            description += $"{CommonString.GetItemName(e.Current.Key)} {Utils.ConvertBigNum(e.Current.Value)}개\n";
        }

        if (rewards.Count == 0) 
        {
            rewardDescription.SetText("무기가 없습니다.");
        }
        else 
        {
            rewardDescription.SetText(description);
        }

     
    }
}
