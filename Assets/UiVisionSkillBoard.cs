using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BackEnd;

public class UiVisionSkillBoard : SingletonMono<UiSkillBoard>
{
    [SerializeField]
    private UiVisionSkillCell uiSkillCell;

    [SerializeField]
    private Transform skillCellParent;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tableData = TableManager.Instance.SkillTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].SKILLCASTTYPE != SkillCastType.Vision) continue;

            var cell = Instantiate<UiVisionSkillCell>(uiSkillCell, skillCellParent);

            cell.Initialize(tableData[i]);
        }
    }
}
