using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiGoldAbilAddDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.equipmentTable.TableDatas[EquipmentTable.MagicBook].AsObservable().Subscribe(e =>
        {
            description.SetText(
                ($"노리개,장식 효과로 공격 능력치 {PlayerStats.GetGoldAbilAddRatio() * PlayerStats.GetNorigaeSoulGradeValue()}배 강화됨"));
        }).AddTo(this);
    }
    
}