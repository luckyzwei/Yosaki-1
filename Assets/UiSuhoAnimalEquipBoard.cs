using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSuhoAnimalEquipBoard : MonoBehaviour
{
    [SerializeField]
    private UiInventoryAnimalView uiInventoryAnimalView;

    [SerializeField]
    private Transform cellParent;
    [SerializeField]
    private Transform cellParent_special;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tableData = TableManager.Instance.suhoPetTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].SUHOPETTYPE == SuhoPetType.Basic)
            {
                var cell = Instantiate<UiInventoryAnimalView>(uiInventoryAnimalView, cellParent);
                cell.Initialize(tableData[i]);
            }
            else if (tableData[i].SUHOPETTYPE == SuhoPetType.Special)
            {
                var cell = Instantiate<UiInventoryAnimalView>(uiInventoryAnimalView, cellParent_special);
                cell.Initialize(tableData[i]);
            }
        }
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value += 1000;
        }
    }
#endif

    private void OnDisable()
    {
        PlayerStats.ResetSuperCritical11CalculatedValue();
    }
}