using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using UnityEngine.UI;

public class UiNorigeaSoulBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gradeText;
    [SerializeField]
    private TextMeshProUGUI transAfterText;

    
    [SerializeField] private GameObject transBeforeObject;
    [SerializeField] private GameObject transAfterObject;
    
    private void Start()
    {
        Initialize();
        SubScribe();
    }

    private void SubScribe()
    {

        ServerData.userInfoTable.GetTableData(UserInfoTable.graduateNorigaeSoul).AsObservable().Subscribe(e =>
        {
            transBeforeObject.SetActive(e < 1);
            transAfterObject.SetActive(e >= 1);

        }).AddTo(this);
    }

    private void Initialize()
    {
        scoreText.SetText($"최고 점수 : {Utils.ConvertBigNum(ServerData.userInfoTable.TableDatas[UserInfoTable.norigaeScore].Value * GameBalance.BossScoreConvertToOrigin)}");
        transAfterText.SetText($"각성 효과로 강화됩니다.\n노리개 수호령 능력치 {GameBalance.NorigaeSoulGraduatePlusValue}배 증가");

        int grade = PlayerStats.GetNorigaeSoulGrade();

        if (grade != -1)
        {
            gradeText.SetText($"{grade + 1}단계");
        }
        else
        {
            gradeText.SetText("없음");
        }


    }

    public void OnClickEnterButton()

    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 하시겠습니까?", () =>
        {
            GameManager.Instance.LoadContents(GameManager.ContentsType.NorigaeSoul);
        }, () => { });
    }
    
    
    public void OnClickTransButton()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.norigaeScore].Value/GameBalance.BossScoreSmallizeValue < GameBalance.NorigaeSoulGraduateScore)
        {
            PopupManager.Instance.ShowAlarmMessage($"최고 점수 {Utils.ConvertBigNumForRewardCell(GameBalance.NorigaeSoulGraduateScore)} 이상일때 각성 가능!");
        }
        else
        {
            PopupManager.Instance.ShowYesNoPopup(CommonString.Notice,
                $"수호령을 각성하려면 점수가 {Utils.ConvertBigNumForRewardCell(GameBalance.NorigaeSoulFixedScore)}이상 이어야 합니다. \n" +
                $"각성시 수호령 효과가 {(GameBalance.NorigaeSoulGraduatePlusValue-1)*100f}% 강화 됩니다.\n" +
                "각성 하시겠습니까??", () =>
                {
                    ServerData.userInfoTable.TableDatas[UserInfoTable.graduateNorigaeSoul].Value = 1;
                    ServerData.userInfoTable.TableDatas[UserInfoTable.norigaeScore].Value = GameBalance.NorigaeSoulFixedScore * GameBalance.BossScoreSmallizeValue;
                    
                    List<TransactionValue> transactions = new List<TransactionValue>();
                    
                    Param userInfoParam = new Param();
                    userInfoParam.Add(UserInfoTable.graduateNorigaeSoul, ServerData.userInfoTable.TableDatas[UserInfoTable.graduateNorigaeSoul].Value);
                    userInfoParam.Add(UserInfoTable.norigaeScore, ServerData.userInfoTable.TableDatas[UserInfoTable.norigaeScore].Value);

                    transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName,UserInfoTable.Indate,userInfoParam));
                    
                    ServerData.SendTransaction(transactions,successCallBack: () =>
                    {
                       Initialize();
                    });
                    
                    PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "각성 완료!!", null);
              
                }, null);
        }
    }
}
