using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class SoulForestPassIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.relicKillCount).AsObservable().Subscribe(e =>
        {
            killCountText.SetText($"영혼의 숲 처치 수 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
