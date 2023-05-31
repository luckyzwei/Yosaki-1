using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UiGoodsViewController : MonoBehaviour
{
    [SerializeField] private GameObject goldObject;
    [SerializeField] private GameObject goldBarObject;
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.graduateGold).AsObservable().Subscribe(e =>
        {
            goldObject.SetActive(e < 1);
            goldBarObject.SetActive(e > 0);
        }).AddTo(this);
    }
}
