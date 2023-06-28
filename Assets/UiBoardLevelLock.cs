using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiBoardLevelLock : MonoBehaviour
{
    [SerializeField]
    private int unlockLevel;


    private void OnEnable()
    {
        if (ServerData.statusTable.GetTableData(StatusTable.Level).Value < unlockLevel)
        {
            gameObject.SetActive(false);
        }
    }


}
