using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSubMenues : SingletonMono<UiSubMenues>
{
    [SerializeField] private MainTabButtons _mainTabButtons;
    private void Start()
    {
        if ((GameManager.Instance.lastContentsType2 != GameManager.ContentsType.NormalField) &&
            (GameManager.contentsType == GameManager.ContentsType.NormalField))
        {
            if (GameManager.Instance.lastContentsType2 == GameManager.ContentsType.TwelveDungeon &&
                (GameManager.Instance.bossId == 12||
                 GameManager.Instance.bossId == 15||
                 GameManager.Instance.bossId == 16||
                 GameManager.Instance.bossId == 17||
                 GameManager.Instance.bossId == 18||
                 GameManager.Instance.bossId == 57
                )) return;
            switch (GameManager.Instance.lastContentsType2)
            {
                case GameManager.ContentsType.NorigaeSoul:
                case GameManager.ContentsType.GumGiSoul:
                case GameManager.ContentsType.Smith:
                case GameManager.ContentsType.SmithTree:
                case GameManager.ContentsType.GumGi:
                case GameManager.ContentsType.GyungRockTower:
                    return;
            }
            _mainTabButtons.OnClickButton();
        }
    }
    

    public void ActiveOnlineRaidLobby() 
    {
        PartyRaidManager.Instance.ActivePartyRaidBoard();
    }
}
