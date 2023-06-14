using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
public class UiHyunsangIndicator : MonoBehaviour
{
    [SerializeField]
    private string goodsKey;

    [SerializeField]
    private TextMeshProUGUI goodsText;

 
    
    void Start()
    {
        Subscribe();
    }
 

    private void Subscribe()
    {
        if (ServerData.goodsTable.TableDatas.ContainsKey(goodsKey))
        {
            ServerData.goodsTable.GetTableData(goodsKey).AsObservable().Subscribe(goods =>
            {
                goodsText.SetText($"{CommonString.GetStatusName(StatusType.SuperCritical19DamPer)} : {Utils.ConvertNum(PlayerStats.GetSuperCritical19DamPer() * 100f,1)} (1개당 1%)");
            }).AddTo(this);
        }
    }
}
