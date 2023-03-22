using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class UiContentsPopup2 : MonoBehaviour
{
    private enum ContentsBoard
    {
        UiRelicBoard,
        YoguisogulBoard,
        GumiHoBoard,
        SonBoard,
        SusanoBoard,
        SinBoard,
        GangchulBoard,
        FoxMaskBoard,
        SuhosinBoard,
        HellBoard,
        ChunBoard,
        YumAndOkBoard,
        GradeTestBoard,
        DokebiBoard,
        SasinsuBoard,
        SumisanBoard,
        SahyungsuBoard,
        UiThiefBoard,
        SmithBoard,
        SuhoAnimal,
        SinsuTower,
    }
    [SerializeField]
    private List<GameObject> lastBoards;
    [SerializeField]
    private List <UiContentsEnterButton> uiContentsEnterButtons;
    void Start()
    {
        
        GameManager.ContentsType type = GameManager.Instance.lastContentsType2;
        int id = GameManager.Instance.bossId;
        switch (type)
        {
            case GameManager.ContentsType.InfiniteTower:
                UiContentsEnterPopup.Instance.Initialize(type,0);
                break;
            case GameManager.ContentsType.InfiniteTower2:
                UiContentsEnterPopup.Instance.Initialize(type,0);
                break;
                
            case  GameManager.ContentsType.RelicDungeon:
                lastBoards[(int)ContentsBoard.UiRelicBoard].SetActive(true);
                break;
            case GameManager.ContentsType.TwelveDungeon:
                switch (id)
                {
                    //십이지
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        uiContentsEnterButtons[0].OnClickButton();
                        break;
                    //신요괴
                    case 13:
                    case 14:
                    case 19:
                    case 21:
                    case 24:
                    case 26:
                    case 28:
                        lastBoards[(int)ContentsBoard.SinBoard].SetActive(true);
                        break;
                    //강철이
                    case 20:
                        lastBoards[(int)ContentsBoard.GangchulBoard].SetActive(true);
                        break;
                    //수호신
                    case 22:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 39:
                        lastBoards[(int)ContentsBoard.SuhosinBoard].SetActive(true);
                        break;
                    //구미호
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                        lastBoards[(int)ContentsBoard.GumiHoBoard].SetActive(true);
                        break;
                    //지옥요괴전
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    //지옥보스전
                    case 52:
                    case 54:
                    case 56:
                        lastBoards[(int)ContentsBoard.HellBoard].SetActive(true);
                        break;
                    //여래
                    case 50:
                        lastBoards[(int)ContentsBoard.SonBoard].SetActive(true);
                        break;
                    //칠선녀
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    //천계보물창고
                    case 66:
                    case 67:
                    case 70:
                    case 71:
                    //서재
                    case 72:
                        lastBoards[(int)ContentsBoard.ChunBoard].SetActive(true);
                        break;
                    //귀신나무
                    case 69:
                        lastBoards[(int)ContentsBoard.FoxMaskBoard].SetActive(true);
                        break;
                    //악의씨앗
                    case 84:
                        lastBoards[(int)ContentsBoard.SusanoBoard].SetActive(true);
                        break;
                    //도깨비 보스
                    case 75:
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                    case 80:
                    case 81:
                    //지키미
                    case 82:
                    //보물도깨비
                    case 83:
                    //도깨비 우두머리
                    case 85:
                    case 86:
                        lastBoards[(int)ContentsBoard.DokebiBoard].SetActive(true);
                    break;
                    //수미산
                    case 87:
                    case 88:
                    case 89:
                    case 90:
                        //수미산지키미
                    case 92:
                    //우두머리
                    case 93:
                    case 94:
                    case 95:
                        //수미숲지키미
                    case 96:
                        lastBoards[(int)ContentsBoard.SumisanBoard].SetActive(true);
                    break;
                    //사신수-황룡
                    case 97:
                        lastBoards[(int)ContentsBoard.SasinsuBoard].SetActive(true);
                    break;
                    //사흉수
                    case 98:
                    case 99:
                    case 100:
                    case 101:
                        lastBoards[(int)ContentsBoard.SahyungsuBoard].SetActive(true);
                    break;
                    //도둑들
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 107:
                    case 108:
                        lastBoards[(int)ContentsBoard.UiThiefBoard].SetActive(true);
                    break;
                }
                break;
            case GameManager.ContentsType.YoguiSoGul:
                lastBoards[(int)ContentsBoard.YoguisogulBoard].SetActive(true);
                break;
            case GameManager.ContentsType.Son:
                lastBoards[(int)ContentsBoard.SonBoard].SetActive(true);
                break;
            case GameManager.ContentsType.SonClone:
                lastBoards[(int)ContentsBoard.SonBoard].SetActive(true);
                break;
            case GameManager.ContentsType.Susano:
                lastBoards[(int)ContentsBoard.SusanoBoard].SetActive(true);
                break;
            case GameManager.ContentsType.FoxMask:
                lastBoards[(int)ContentsBoard.FoxMaskBoard].SetActive(true);
                break;
            case GameManager.ContentsType.Hell:
            case GameManager.ContentsType.HellRelic:
            case GameManager.ContentsType.HellWarMode:
            case GameManager.ContentsType.DokebiTower:
                lastBoards[(int)ContentsBoard.HellBoard].SetActive(true);
                break;
            case GameManager.ContentsType.ChunFlower:
                lastBoards[(int)ContentsBoard.ChunBoard].SetActive(true);
                break;
            case GameManager.ContentsType.Yum:
            case GameManager.ContentsType.Ok:
            case GameManager.ContentsType.Do:
            case GameManager.ContentsType.Sumi:
                lastBoards[(int)ContentsBoard.YumAndOkBoard].SetActive(true);
                break;
            case GameManager.ContentsType.GradeTest:
                lastBoards[(int)ContentsBoard.GradeTestBoard].SetActive(true);
                break;
            case GameManager.ContentsType.DokebiFire:
                lastBoards[(int)ContentsBoard.DokebiBoard].SetActive(true);
                break;
            case GameManager.ContentsType.Sasinsu:
                lastBoards[(int)ContentsBoard.SasinsuBoard].SetActive(true);
                break;
            case GameManager.ContentsType.SumiFire:
            case GameManager.ContentsType.SumisanTower:
                lastBoards[(int)ContentsBoard.SumisanBoard].SetActive(true);
                break;
            case GameManager.ContentsType.RoyalTombTower :
                lastBoards[(int)ContentsBoard.UiThiefBoard].SetActive(true);
                break;    
            case GameManager.ContentsType.Smith :
            case GameManager.ContentsType.SmithTree :
                lastBoards[(int)ContentsBoard.SmithBoard].SetActive(true);
                break;   
            case GameManager.ContentsType.SuhoAnimal :
                lastBoards[(int)ContentsBoard.SuhoAnimal].SetActive(true);
                break;  
            case GameManager.ContentsType.SinsuTower :
                lastBoards[(int)ContentsBoard.SinsuTower].SetActive(true);
                break;

        }
        
        GameManager.Instance.ResetLastContents2();
    }
}
