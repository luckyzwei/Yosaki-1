using System;
using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static UiTwelveRewardPopup;

public class UiGangChulBossRewardView : MonoBehaviour
{
    
    [SerializeField] private Image itemIcon;

    [SerializeField] private TextMeshProUGUI itemDescription;


    [SerializeField] private TextMeshProUGUI rewardAmount;


    [SerializeField] private TextMeshProUGUI gradeText;


    private CompositeDisposable disposable = new CompositeDisposable();

    private void OnDestroy()
    {
        disposable.Dispose();
    }

    public void Initialize(TwelveBossRewardInfo rewardInfo, BossServerData bossServerData)
    {
        
        var bossTableData = TableManager.Instance.TwelveBossTable.dataArray[20];
        
        itemIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)rewardInfo.rewardType);

        itemDescription.SetText($"{CommonString.GetItemName((Item_Type)rewardInfo.rewardType)}");

        rewardAmount.SetText($"{Utils.ConvertBigNum(rewardInfo.rewardAmount)}개");

        if (gradeText != null)
        {
            gradeText.SetText($"{rewardInfo.idx + 1}단계\n({rewardInfo.idx + 1}점)");

            //문파만
            if (bossServerData.idx == 12)
            {
                if (rewardInfo.currentDamage >= rewardInfo.damageCut)
                {
                    if (UiGuildBossView.Instance != null && UiGuildBossView.Instance.rewardGrade < rewardInfo.idx + 1)
                    {
                        UiGuildBossView.Instance.rewardGrade = rewardInfo.idx + 1;
                    }
                }

                //var bossTableData = TableManager.Instance.TwelveBossTable.dataArray[20];

                var bsd = ServerData.bossServerTable.TableDatas[bossTableData.Stringid];

                double currentDamage = 0f;

                if (string.IsNullOrEmpty(bsd.score.Value) == false)
                {
                    currentDamage = double.Parse(bsd.score.Value);
                }

                if (currentDamage >= rewardInfo.damageCut)
                {
                    //강철은 구미호와 같은 점수 계산 사용
                    if (UiGangChulView.Instance != null && UiGangChulView.Instance.rewardGrade < rewardInfo.idx + 1)
                    {
                        UiGangChulView.Instance.rewardGrade = rewardInfo.idx + 1;
                    }

                    if (UiGuildBossView.Instance != null &&
                        UiGuildBossView.Instance.rewardGrade_GangChul < rewardInfo.idx + 1)
                    {
                        UiGuildBossView.Instance.rewardGrade_GangChul = rewardInfo.idx + 1;
                    }
                }
            }
        }

    }



}