using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiRewardView;

public class UiDokebiResultPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Transform rewardParent;

    public void OnClickReturnButton()
    {
        GameManager.Instance.LoadNormalField();
    }

    public void Initialize(int defeatEnemiesNum)
    {
        SoundManager.Instance.PlaySound("BonusEnd");

        description.SetText($"{defeatEnemiesNum} 처치 완료!");

        SetReward(defeatEnemiesNum);
    }

    private void SetReward(int defeatEnemiesNum)
    {
        
//        var prefab = CommonPrefabContainer.Instance.uiRewardViewPrefab;

    //    int rewardNum = defeatEnemiesNum;

  //      var rewardPrefab = Instantiate<UiRewardView>(prefab, rewardParent);

        //RewardData rewardData = new RewardData(Item_Type.Dokebi, rewardNum);

       // rewardPrefab.Initialize(rewardData);

        int prefMaxKillCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount3).Value;
        

        if (defeatEnemiesNum > prefMaxKillCount)
        {
                ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount3).Value = defeatEnemiesNum;
                ServerData.userInfoTable.UpData(UserInfoTable.dokebiKillCount3, false);
        }
    }
}
