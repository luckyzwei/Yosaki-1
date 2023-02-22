using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class SonTransAbil : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI transDescription;


    void Start()
    {

        Subscribe();

    }

    private void Subscribe()
    {

        ServerData.goodsTable.GetTableData(GoodsTable.Ym).AsObservable().Subscribe(e =>
        {
            transDescription.SetText($"각성시 보유효과 {PlayerStats.SonTransAddValue *100}% 증가");
        }).AddTo(this);

    }

}
