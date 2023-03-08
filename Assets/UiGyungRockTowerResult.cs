using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiRewardView;
public class UiGyungRockTowerResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private DungeonRewardView dungeonRewardView;

    [SerializeField]
    private GameObject failObject;
    [SerializeField]
    private TextMeshProUGUI stageChangeText;
    [SerializeField]
    private GameObject stageChangeButton;
    [SerializeField]
    private GameObject successObject;

    public void Initialize(ContentsState state, List<RewardData> rewardDatas) 
    {
        resultText.SetText(GetTitleText(state));

        successObject.SetActive(state == ContentsState.Clear);
        failObject.SetActive(state != ContentsState.Clear);

        dungeonRewardView.gameObject.SetActive(rewardDatas != null);

        if (rewardDatas != null) 
        {
            dungeonRewardView.Initalize(rewardDatas);
        }
    }

    private string GetTitleText(ContentsState contentsState) 
    {
        switch (contentsState)
        {
            case ContentsState.Dead:
                stageChangeText.SetText("재도전");
                return "실패!";
            case ContentsState.TimerEnd:
                stageChangeText.SetText("재도전");
                return "시간초과!";
            case ContentsState.Clear:
                if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx5).Value < (TableManager.Instance.gyungRockTowerTable.dataArray.Length))
                {
                    stageChangeText.SetText("다음 스테이지");
                }
                else
                {
                    stageChangeButton.SetActive(false);
                }
                return "클리어!!";
        }

        return "미등록";
    }
}
