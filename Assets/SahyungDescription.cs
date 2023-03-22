using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class SahyungDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI marbleDescription;

    [SerializeField]
    private bool useSpace = true;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {

        ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).AsObservable().Subscribe(e =>
        {
            if (useSpace)
            {
            marbleDescription.SetText($"{CommonString.GetItemName(Item_Type.SahyungTreasure)} 1개당\n 흉수베기 효과 {PlayerStats.sahyungUpgradeValue * 100f}% 강화");
                
            }
            else
            {
                
            marbleDescription.SetText($"{CommonString.GetItemName(Item_Type.SahyungTreasure)} 1개당 흉수베기 효과 {PlayerStats.sahyungUpgradeValue * 100f}% 강화");
            }
        }).AddTo(this);

    }
}
