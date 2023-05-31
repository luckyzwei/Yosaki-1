using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using UnityEngine.UI;

public class UiYumBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gradeText;

    [SerializeField]
    private TextMeshProUGUI transAfterText;
    [SerializeField] private GameObject transBefore;
    [SerializeField] private GameObject transAfter;
    private void Start()
    {
        Initialize();
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.KingTrialGraduateIdx).AsObservable().Subscribe(e =>
        {
            transBefore.SetActive(e == 0);
            transAfter.SetActive(e != 0);
        }).AddTo(this);
    }

    private void Initialize()
    {
        scoreText.SetText($"최고 점수 : {Utils.ConvertBigNum(ServerData.userInfoTable.TableDatas[UserInfoTable.yumScore].Value * GameBalance.BossScoreConvertToOrigin)}");

        int grade = PlayerStats.GetYumGrade();

        if (grade != -1)
        {
            gradeText.SetText($"{grade + 1}단계");
        }
        else
        {
            gradeText.SetText("없음");
        }
        
        transAfterText.SetText($"각성효과로 강화됩니다.\n염라대왕 능력치 {GameBalance.yumGraduateValue}배 증가");
    }

    public void OnClickEnterButton()
    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 하시겠습니까?", () =>
        {
            GameManager.Instance.LoadContents(GameManager.ContentsType.Yum);
        }, () => { });
    }
    public void OnClickTransButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.yumScore].Value * GameBalance.BossScoreConvertToOrigin < GameBalance.yumGraduateScore)
        {
            PopupManager.Instance.ShowAlarmMessage($"데미지 {Utils.ConvertBigNum(GameBalance.yumGraduateScore)} 이상일때 각성 가능!");
        }
        else if (ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.KingTrialGraduateIdx].Value != 0)
        {
            PopupManager.Instance.ShowAlarmMessage($"이전 각성을 완료해주세요!");
        }
        else
        {
            PopupManager.Instance.ShowYesNoPopup(CommonString.Notice,
                $"염라대왕 효과가 강화됩니다.({GameBalance.yumGraduateValue * 100}%)\n" +
                "각성 하시겠습니까??", () =>
                {

                    ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.KingTrialGraduateIdx].Value = 1;
                    ServerData.userInfoTable_2.UpData(UserInfoTable_2.KingTrialGraduateIdx, false);
                    PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "각성 완료!!", null);

                }, null);
        }

    }
}
