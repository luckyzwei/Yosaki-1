using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMagicBookCollectBoard : MonoBehaviour
{
    [SerializeField]
    private UiMagicBookCollectCell cellPrefab;
    [SerializeField]
    private Transform cellParent;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tableData = TableManager.Instance.SkillTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].SKILLCASTTYPE != SkillCastType.Player) continue;
            if(tableData[i].Skilltype>=4) continue;
            var cell = Instantiate<UiMagicBookCollectCell>(cellPrefab, cellParent);
            cell.Initialize(tableData[i]);
        }
    }
}
