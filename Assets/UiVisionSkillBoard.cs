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

    private List<SkillTableData> visionSkillData = new List<SkillTableData>();
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

            visionSkillData.Add(tableData[i]);
        }

        //데미지 순으로 정렬
        visionSkillData.Sort((x, y) => x.Damageper.CompareTo(y.Damageper));
        
        for (int i = 0; i < visionSkillData.Count; i++)
        {
            var cell = Instantiate<UiVisionSkillCell>(uiSkillCell, skillCellParent);

            cell.Initialize(visionSkillData[i]);
        }
        
    }
}
