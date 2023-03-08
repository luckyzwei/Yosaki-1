using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class PassCountIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText;

    [SerializeField] private string key;
    [SerializeField] private string defix;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(key).AsObservable().Subscribe(e =>
        {
            killCountText.SetText($"{Utils.ConvertBigNum(e)}{defix}");
        }).AddTo(this);
    }
}