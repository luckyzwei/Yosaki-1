using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiRankBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI markApplyDescription;

    private void OnEnable()
    {
        RankManager.Instance.UpdateStage_Score(ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value);
        RankManager.Instance.UpdateUserRank_Level();
        //RankManager.Instance.RequestMyLevelRank();
        //RankManager.Instance.RequestMyStageRank();

        //if (GameManager.contentsType == GameManager.ContentsType.NormalField)
        //{
        //    RankManager.Instance.RequestMyBossRank();
        //    RankManager.Instance.RequestMyRealBossRank();
        //    RankManager.Instance.RequestMyRelicRank();
        //    RankManager.Instance.RequestMyMiniGameRank();
        //}
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.hellMark].AsObservable().Subscribe(e =>
        {
            if (e != 0)
            {
                int idx = (int)e;

                if (idx < GameBalance.warMarkAbils.Count)
                {
                    markApplyDescription.SetText($"{CommonString.GetHellMarkAbilName(idx)} 적용 : 경험치 획득(%) +{GameBalance.warMarkAbils[idx] * 100f}");
                }
                else
                {
                    markApplyDescription.SetText($"증표 없음");
                }
            }
            else
            {
                markApplyDescription.SetText($"증표 없음");
            }

        }).AddTo(this);
    }

}
