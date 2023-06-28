using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using BackEnd;
using UnityEngine.UI;

public class UiVisionTowerBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    
    private void Start()
    {
        Initialize();
        
        GainVisionSkills();
    }

    private void GainVisionSkills()
    {
        //첫번째 기술 획득
        if (PlayerStats.GetVisionTowerGrade() >=GameBalance.visionSkill6GainIdx )
        {
            if (ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill6).Value<1)
            {
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill6).Value = 1;
                ServerData.goodsTable.UpData(GoodsTable.VisionSkill6, false);    
                PopupManager.Instance.ShowConfirmPopup("알림","궁극기 획득!",null);
                LogManager.Instance.SendLogType("Funnel_Tutorial", "complete", $"Gain First VisionSkill");
            }
        }
        //두번째 기술 획득
        if (PlayerStats.GetVisionTowerGrade() >=GameBalance.visionSkill7GainIdx )
        {
            if (ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill7).Value<1)
            {
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill7).Value = 1;
                ServerData.goodsTable.UpData(GoodsTable.VisionSkill7, false);
                PopupManager.Instance.ShowConfirmPopup("알림","궁극기 획득!",null);
            }
        }
        
    }
    private void Initialize()
    {
        scoreText.SetText($"최고 등급 : {Utils.ConvertBigNum(ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.visionTowerScore].Value)}");
    }

    public void OnClickEnterButton()
    {
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, "입장 하시겠습니까?", () =>
        {
            GameManager.Instance.LoadContents(GameManager.ContentsType.VisionTower);
        }, () => { });
    }

}
