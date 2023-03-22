using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;


public class SinsuMarbleDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI marbleDescription;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {

        
        ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).AsObservable().Subscribe(e =>
        {
            marbleDescription.SetText($"{CommonString.GetItemName(Item_Type.SinsuMarble)} 1개당 신수베기 효과 {PlayerStats.sinSuUpgradeValue * 100f}% 강화");
        }).AddTo(this);

    }
}
