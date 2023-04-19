using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDarkCleansBossBoard : MonoBehaviour
{
    [SerializeField]
    private Transform cellParents;

    [SerializeField]
    private UiDarkAbilCell cellPrefab;

    void Start()
    {
        Intialize();
    }

    private void Intialize()
    {
        var tableDatas = TableManager.Instance.DarkAbil.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            var cell = Instantiate<UiDarkAbilCell>(cellPrefab, cellParents);
            cell.Initialize(tableDatas[i]);
        }
    }
}
