using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class SeasonKillCountIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;

    [SerializeField] private string key;
    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(key).AsObservable().Subscribe(e =>
        {
            killCountText.SetText($"처치 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
