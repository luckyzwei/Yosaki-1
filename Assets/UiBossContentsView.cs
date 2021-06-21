﻿using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UiRewardView;

public class UiBossContentsView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private GameObject lockObject;

    [SerializeField]
    private Image bossIcon;

    [SerializeField]
    private List<Sprite> bossSprites;

    private BossTableData bossTableData;

    [SerializeField]
    private Button enterButton;

    [SerializeField]
    private Button clearButton;

    private ObscuredInt instantOpenAmount = 1;

    //private ContentsRewardManager

    public void OnClickToggle_1(bool isOn)
    {
        if (isOn)
            instantOpenAmount = 1;
    }

    public void OnClickToggle_2(bool isOn)
    {
        if (isOn)
            instantOpenAmount = 10;
    }

    public void OnClickToggle_3(bool isOn)
    {
        if (isOn)
            instantOpenAmount = 30;
    }

    public void Initialize(BossTableData bossTableData)
    {
        this.bossTableData = bossTableData;
        title.SetText(bossTableData.Name);
        description.SetText(bossTableData.Description);

        lockObject.SetActive(bossTableData.Islock);

        bossIcon.sprite = CommonUiContainer.Instance.bossIcon[bossTableData.Id];
    }

    public void OnClickEnterButton()
    {
        int price = 1;
        int currentTicketNum = (int)DatabaseManager.goodsTable.GetTableData(GoodsTable.Ticket).Value;

        if (currentTicketNum < price)
        {
            PopupManager.Instance.ShowAlarmMessage("티켓이 부족합니다.");
            return;
        }

        enterButton.interactable = false;

        DatabaseManager.goodsTable.GetTableData(GoodsTable.Ticket).Value--;

        DatabaseManager.goodsTable.SyncToServerEach(GoodsTable.Ticket, () =>
        {
            //
            Param param = new Param();
            param.Add("티켓사용함", $"남은 갯수 : { DatabaseManager.goodsTable.GetTableData(GoodsTable.Ticket).Value}");

            SendQueue.Enqueue(Backend.GameLog.InsertLog, "Ticket", param, (brk) => { });
            //로그

            GameManager.Instance.SetBossId(bossTableData.Id);
            GameManager.Instance.LoadContents(GameManager.ContentsType.SingleRaid);
        },
        () =>
        {
            enterButton.interactable = true;

        });
    }

    public void OnClickInstantClearButton()
    {
        int price = 1;

        int currentTicketNum = (int)DatabaseManager.goodsTable.GetTableData(GoodsTable.Ticket).Value;

        if (currentTicketNum < price)
        {
            PopupManager.Instance.ShowAlarmMessage("티켓이 부족합니다.");
            return;
        }

        clearButton.interactable = false;

        int clearAmount = Mathf.Min(instantOpenAmount, currentTicketNum);

        if (bossTableData.Id == 0) 
        {
            RankManager.Instance.RequestMyBoss0Rank(e =>
            {
                if (e != null)
                {
                    PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{Utils.ConvertBigNum(e.Score)}점으로 {clearAmount}번\n소탕 하시겠습니까?", () =>
                    {
                        InstantClearReceive(e.Score, clearAmount);
                    },
                    () =>
                    {
                        clearButton.interactable = true;
                    });
                }
                else
                {
                    PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
                    clearButton.interactable = true;
                }
            });
        }
        else if (bossTableData.Id == 1) 
        {
            RankManager.Instance.RequestMyBoss1Rank(e =>
            {
                if (e != null)
                {
                    PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{Utils.ConvertBigNum(e.Score)}점으로 {clearAmount}번\n소탕 하시겠습니까?", () =>
                    {
                        InstantClearReceive(e.Score, clearAmount);
                    },
                    () =>
                    {
                        clearButton.interactable = true;
                    });
                }
                else
                {
                    PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
                    clearButton.interactable = true;
                }
            });
        } 
    
    }

    private void InstantClearReceive(float score,int clearAmount) 
    {
        var rewardList = SingleRaidManager.GetRewawrdData(bossTableData, score, clearAmount);

        if (rewardList.Find(element => element.itemType == Item_Type.Ticket) == null)
        {
            //티켓 차감도 트랜젝션에 전송되도록
            rewardList.Add(new UiRewardView.RewardData(Item_Type.Ticket, 0));
        }

        //티켓차감
        DatabaseManager.goodsTable.GetTableData(GoodsTable.Ticket).Value -= clearAmount;

        DatabaseManager.SendTransaction(rewardList, addValue: null,
         completeCallBack: () =>
         {
             clearButton.interactable = true;
         }
        , successCallBack: () =>
        {
            clearButton.interactable = true;
            DailyMissionManager.UpdateDailyMission(DailyMissionKey.RewardedBossContents, clearAmount);
            WhenClearSuccess(rewardList);
        });
    }

    private void WhenClearSuccess(List<RewardData> rewardData)
    {
        var zeroData = rewardData.FindAll(e => e.amount == 0);
        for (int i = 0; i < zeroData.Count; i++)
        {
            rewardData.Remove(zeroData[i]);
        }

        UiBossContentsClearBoard.Instance.Initialize(rewardData);
    }
}
