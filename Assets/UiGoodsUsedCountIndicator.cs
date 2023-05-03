using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;
using UniRx;

public class UiGoodsUsedCountIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usedCountText;



    void Start()
    {
        //
        Subscribe();
    }


    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.usedSnowManCollectionCount).AsObservable().Subscribe(e =>
        {
            usedCountText.SetText($"교환한 {CommonString.GetItemName(ServerData.goodsTable.ServerStringToItemType(GoodsTable.Event_Item_SnowMan))} 수 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
