using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiContentsExitButton : MonoBehaviour
{
    [SerializeField]
    private bool ShowWarningMessage = true;
    [SerializeField]
    private GameObject buttonRootObject;

    private void OnEnable()
    {
        if (buttonRootObject != null)
        {
            buttonRootObject.SetActive(NextStageCheck());
        }
    }
    private bool NextStageCheck()
    {
        if (GameManager.contentsType == GameManager.ContentsType.InfiniteTower2 || GameManager.contentsType == GameManager.ContentsType.DokebiTower ||
            GameManager.contentsType == GameManager.ContentsType.FoxMask || GameManager.contentsType == GameManager.ContentsType.Yum ||
            GameManager.contentsType == GameManager.ContentsType.Ok || GameManager.contentsType == GameManager.ContentsType.Do ||
            GameManager.contentsType == GameManager.ContentsType.Sumi ||
            GameManager.contentsType == GameManager.ContentsType.GradeTest || GameManager.contentsType == GameManager.ContentsType.Sasinsu ||
            GameManager.contentsType == GameManager.ContentsType.SumisanTower||
            GameManager.contentsType == GameManager.ContentsType.GyungRockTower||
            GameManager.contentsType == GameManager.ContentsType.RoyalTombTower ||
            GameManager.contentsType == GameManager.ContentsType.SinsuTower ||
            GameManager.contentsType == GameManager.ContentsType.GuildTower 
            )
        {
            return true;
        }

        if (GameManager.contentsType == GameManager.ContentsType.TwelveDungeon)
        {
            var tableData = TableManager.Instance.TwelveBossTable.dataArray[GameManager.Instance.bossId];
            //악귀퇴치 등의 이뮨을 무시하는 컨텐츠라면.
            if (tableData.NOTIMMUNETYPE != NotImmuneType.Normal)
            {
                return true;
            }
        }

        switch (GameManager.contentsType)
        {
            //산신령 & 서재 & 지키미 & 보도 & 수미산 지키미 
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 57:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 72:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 82:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 83:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 92:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 96:
            //도깨비 보스 & 수미산 사천왕
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 85:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 86:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 87:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 88:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 93:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 94:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 95:
            case GameManager.ContentsType.TwelveDungeon when GameManager.Instance.bossId == 97:
                return true;
            default:
                return false;
        }
    }
    public void OnClickExitButton()
    {
        if (ShowWarningMessage == true)
        {
            PopupManager.Instance.ShowYesNoPopup("알림", "포기하고 나가시겠습니까?", () =>
            {
                BuffOff();
                GameManager.Instance.LoadNormalField();
            }, null);
        }
        else
        {
            BuffOff();
            GameManager.Instance.LoadNormalField();
        }
    }
    public void OnClickNextStageButton()
    {


        //if (GameManager.contentsType == GameManager.ContentsType.InfiniteTower)
        //{
        //    if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx).Value < (TableManager.Instance.TowerTable.dataArray.Length))
        //    {
        //        GameManager.Instance.LoadContents(GameManager.ContentsType.InfiniteTower);
        //    }
        //}
        //다음스테이지가 있는 타워인 경우
        if (GameManager.contentsType == GameManager.ContentsType.InfiniteTower2)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx2).Value < (TableManager.Instance.TowerTable2.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.InfiniteTower2);
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else if (GameManager.contentsType == GameManager.ContentsType.DokebiTower)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx3).Value < (TableManager.Instance.towerTable3.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.DokebiTower);
            }  
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else if (GameManager.contentsType == GameManager.ContentsType.FoxMask)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.foxMask).Value <
                (TableManager.Instance.FoxMask.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.FoxMask);
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else if (GameManager.contentsType == GameManager.ContentsType.RoyalTombTower)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.RoyalTombFloorIdx).Value <
                (TableManager.Instance.royalTombTowerTable.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.RoyalTombTower);
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else if (GameManager.contentsType == GameManager.ContentsType.SinsuTower)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx6).Value <
                (TableManager.Instance.sinsuTower.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.SinsuTower);
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else if (GameManager.contentsType == GameManager.ContentsType.GuildTower)
        {
            if ((int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value <
                (TableManager.Instance.guildTowerTable.dataArray.Length))
            {
                GameManager.Instance.LoadContents(GameManager.ContentsType.GuildTower);
            }
            else
            {
                PopupManager.Instance.ShowAlarmMessage("최종 단계 입니다.");
            }
        }
        else
        {
            var type = GameManager.contentsType;
            GameManager.Instance.LoadContents(type);
            if (buttonRootObject != null)
            {
                buttonRootObject.SetActive(false);
            }
        }



    }

    public void OnClickExitButton_ForPartyRaid()
    {
        PopupManager.Instance.ShowYesNoPopup("알림", "포기하고 나가시겠습니까?", () =>
        {
            BuffOff();

            PartyRaidManager.Instance.OnClickCloseButton();
            GameManager.Instance.LoadNormalField();
        }, null);
    }


    private void BuffOff()
    {
        UiSusanoBuff.isImmune.Value = false;
        UiDokebiBuff.isImmune.Value = false;
    }
}
