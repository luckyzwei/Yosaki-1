using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class GangChulPassDamageIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.bossServerTable.TableDatas["boss20"].score.AsObservable().Subscribe(e =>
        {
            if (string.IsNullOrEmpty(e))
            {
                killCountText.SetText($"강철이에게 입힌 피해 : 0");
            }
            else
            {
                killCountText.SetText($"강철이에게 입힌 피해 : {Utils.ConvertBigNum(double.Parse(e))}");
            }
        }).AddTo(this);
    }
}
