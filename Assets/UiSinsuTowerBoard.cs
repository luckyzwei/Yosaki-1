using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UiRewardView;

public class UiSinsuTowerBoard : MonoBehaviour
{
    [FormerlySerializedAs("uiTower3RewardView")] [SerializeField]
    private UiTower5RewardView uiTower5RewardView;

    [SerializeField]
    private TextMeshProUGUI currentStageText;

    [SerializeField]
    private GameObject normalRoot;

    [SerializeField]
    private GameObject allClearRoot;

    void OnEnable()
    {
        SetStageText();
        SetReward();
    }

    private bool IsAllClear()
    {
        int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx6).Value;

        return currentFloor >= TableManager.Instance.sinsuTower.dataArray.Length;
    }

    private void SetStageText()
    {
        if (IsAllClear() == false)
        {
            int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx6).Value;
            currentStageText.SetText($"{currentFloor + 1}층 입장");
        }
        else
        {
            currentStageText.SetText($"업데이트 예정 입니다");
        }

    }

    private void SetReward()
    {
        bool isAllClear = IsAllClear();

        normalRoot.SetActive(isAllClear == false);
        allClearRoot.SetActive(isAllClear == true);

        if (isAllClear == false)
        {
            int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx6).Value;

            if (currentFloor >= TableManager.Instance.sinsuTower.dataArray.Length)
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"잘못된 데이터 idx : {currentFloor}", null);
                return;
            }

            var towerTableData = TableManager.Instance.sinsuTower.dataArray[currentFloor];

            uiTower5RewardView.UpdateRewardView(towerTableData.Id);
        }


    }

    public void OnClickEnterButton()
    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 할까요?", () =>
        {

            GameManager.Instance.LoadContents(GameManager.ContentsType.SinsuTower);

        }, () => { });
    }
}
