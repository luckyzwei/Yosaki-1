using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDosulSkillBoard : MonoBehaviour
{
    [SerializeField]
    private DosulSkillCell skillCell;

    [SerializeField]
    private Transform cellParent;

    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var tableData = TableManager.Instance.dosulTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if(tableData[i].Unlock_Skill_Id==0)continue;

            var cell = Instantiate<DosulSkillCell>(skillCell, cellParent);
            cell.Initialize(tableData[i]);
        }
    }

}
