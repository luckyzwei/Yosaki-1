﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSubMenues : SingletonMono<UiSubMenues>
{
    [SerializeField] private MainTabButtons _mainTabButtons;
    [SerializeField] private MainTabButtons _GangChulButton;
    [SerializeField] private MainTabButtons _SealSwordButton;

    private void Start()
    {
        if ((GameManager.Instance.lastContentsType2 != GameManager.ContentsType.NormalField) &&
            (GameManager.contentsType == GameManager.ContentsType.NormalField))
        {
            if (GameManager.Instance.lastContentsType2 == GameManager.ContentsType.TwelveDungeon &&
                (GameManager.Instance.bossId == 12 ||
                 GameManager.Instance.bossId == 15 ||
                 GameManager.Instance.bossId == 16 ||
                 GameManager.Instance.bossId == 17 ||
                 GameManager.Instance.bossId == 18 ||
                 GameManager.Instance.bossId == 20 || //강철이
                 GameManager.Instance.bossId == 57
                ))
            {
                //강철이
                if (GameManager.Instance.bossId == 20)
                {
                    _GangChulButton.OnClickButton();
                    //아무거나로 변경
                    GameManager.Instance.SetBossId(12);
                }

                return;
                
            }

            switch (GameManager.Instance.lastContentsType2)
            {
                case GameManager.ContentsType.NorigaeSoul:
                case GameManager.ContentsType.GumGiSoul:
                case GameManager.ContentsType.GumGi:
                case GameManager.ContentsType.GyungRockTower:
                case GameManager.ContentsType.GuildTower:
                case GameManager.ContentsType.GyungRockTower2:
                    return;
                case GameManager.ContentsType.SealSwordTower:
                case GameManager.ContentsType.SealAwake:
                {
                    GameManager.Instance.ResetLastContents2();
                    _SealSwordButton.OnClickButton();
                    return;
                }
            }

            _mainTabButtons.OnClickButton();
        }
    }


    public void ActiveOnlineRaidLobby()
    {
        PartyRaidManager.Instance.ActivePartyRaidBoard();
    }
}