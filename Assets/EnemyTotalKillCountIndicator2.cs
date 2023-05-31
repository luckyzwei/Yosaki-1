using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class EnemyTotalKillCountIndicator2 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.oddMonthKillCount).AsObservable().Subscribe(e =>
        {
            killCountText.SetText($"처치 : {Utils.ConvertBigNum(e)}");
        }).AddTo(this);
    }
}
