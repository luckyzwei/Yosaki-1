using System;
using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UiUpgradeGoldPopUp : MonoBehaviour
{

    [SerializeField] private SeletableTab _seletableTab;

    private void Start()
    {
        InitializeTab();
    }

    private void InitializeTab()
    {
        _seletableTab.OnSelect((int)ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.graduateGold).Value);
    }
}
