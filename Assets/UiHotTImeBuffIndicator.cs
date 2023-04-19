using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiHotTImeBuffIndicator : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI timeDescription;

    void Start()
    {
        SetDescription();
        Subscribe();
    }

    void Subscribe()
    {
        ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.AsObservable().Subscribe(
            e =>
            {
                SetDescription();
            }).AddTo(this);
    }
    
    private void SetDescription()
    {
        string desc = string.Empty;

        //desc += $"<color=yellow>매일 20~22시\n";
        if (ServerData.userInfoTable.IsHotTimeEvent())
        {
            float exp = GameBalance.HotTimeEvent_Exp;
            float gold = GameBalance.HotTimeEvent_Gold;
            float growthStone = GameBalance.HotTimeEvent_GrowthStone;
            float marble = GameBalance.HotTimeEvent_Marble;
            if (ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0)
            {
                exp += GameBalance.HotTimeEvent_Ad_Exp;
                gold += GameBalance.HotTimeEvent_Ad_Gold;
                growthStone += GameBalance.HotTimeEvent_Ad_GrowthStone;
                marble += GameBalance.HotTimeEvent_Ad_Marble;
            }
            if (ServerData.userInfoTable.IsHotTime())
            {
                exp += GameBalance.HotTime_Exp;
                gold += GameBalance.HotTime_Gold;
                growthStone += GameBalance.HotTime_GrowthStone;
                marble += GameBalance.HotTime_Marble;
            }
            desc += $"경험치 +{exp * 100f}%\n";
            desc += $"금화 +{gold * 100f}%\n";
            desc += $"수련의돌 +{growthStone * 100f}%\n";
            desc += $"여우구슬 +{marble * 100f}%";
        }
        else
        {
            if (ServerData.userInfoTable.IsWeekend() == false)
            {
                desc += $"경험치 +{GameBalance.HotTime_Exp * 100f}%\n";
                //desc += $"경험치 +{GameBalance.HotTime_Exp * 100f}%\n";
                desc += $"금화 +{GameBalance.HotTime_Gold * 100f}%\n";
                desc += $"수련의돌 +{GameBalance.HotTime_GrowthStone * 100f}%\n";
                desc += $"여우구슬 +{GameBalance.HotTime_Marble * 100f}%";
            }
            else
            {
                desc += $"경험치 +{GameBalance.HotTime_Exp_Weekend * 100f}%\n";
                //desc += $"경험치 +{GameBalance.HotTime_Exp_Weekend * 100f}%\n";
                desc += $"금화 +{GameBalance.HotTime_Gold_Weekend * 100f}%\n";
                desc += $"수련의돌 +{GameBalance.HotTime_GrowthStone_Weekend * 100f}%\n";
                desc += $"여우구슬 +{GameBalance.HotTime_Marble_Weekend * 100f}%";
            }
        }

        description.SetText(desc);

        //string timeDesc = string.Empty;

        //timeDesc += "핫타임 진행중!";

        //timeDescription.SetText(timeDesc);
    }

}
