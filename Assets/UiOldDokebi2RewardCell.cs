﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiOldDokebi2RewardCell : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI itemDescription;

    [SerializeField]
    private Button rewardButton;

    [SerializeField]
    private TextMeshProUGUI rewardButtonDescription;

    [SerializeField]
    private TextMeshProUGUI rewardAmount;

    [SerializeField]
    private GameObject rewardLockMask;

    [SerializeField]
    private TextMeshProUGUI lockDescription;

    [SerializeField]
    private GameObject rewardedIcon;

    private OldDokebi2TableData tableData;

    private int lastClearStageId = 0;

    public void Initialize(OldDokebi2TableData tableData)
    {
        lastClearStageId = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.oldDokebi2LastClear].Value;

        this.tableData = tableData;

        rewardLockMask.SetActive(lastClearStageId < tableData.Stage);

        itemIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)tableData.Rewardtype);

        itemDescription.SetText($"{CommonString.GetItemName((Item_Type)tableData.Rewardtype)}({tableData.Stage}단계)");

        rewardAmount.SetText($"{Utils.ConvertBigNum(tableData.Rewardvalue)}개");

        lockDescription.SetText($"{tableData.Stage}단계 돌파시 해금");

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.etcServerTable.TableDatas[EtcServerTable.oldDokebi2Reward].AsObservable().Subscribe(e =>
        {
            bool rewarded = ServerData.etcServerTable.OldDokebi2Rewarded(tableData.Stage);

            rewardButtonDescription.SetText(rewarded ? "완료" : "받기");

            rewardedIcon.SetActive(rewarded);

        }).AddTo(this);
    }
    public bool AlreadyReceiveReward()
    {
        return ServerData.etcServerTable.OldDokebi2Rewarded(tableData.Stage);
    }
    public bool NotEnoughCondition()
    {
        return lastClearStageId < tableData.Stage;
    }

    public bool CanGetReward()
    {
        return AlreadyReceiveReward() == false && NotEnoughCondition() == false;
    }

    public void OnClickGetButton()
    {
        if (NotEnoughCondition())
        {
            PopupManager.Instance.ShowAlarmMessage("해당 단계를 클리어 해야 합니다.");
            return;
        }

        if (AlreadyReceiveReward())
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 받았습니다.");
            return;
        }

        rewardButton.interactable = false;

        Item_Type type = (Item_Type)tableData.Rewardtype;

        float amount = tableData.Rewardvalue;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param rewardParam = new Param();

        ServerData.etcServerTable.TableDatas[EtcServerTable.oldDokebi2Reward].Value += $"{BossServerTable.rewardSplit}{tableData.Stage}";

        rewardParam.Add(EtcServerTable.oldDokebi2Reward, ServerData.etcServerTable.TableDatas[EtcServerTable.oldDokebi2Reward].Value);

        transactions.Add(TransactionValue.SetUpdate(EtcServerTable.tableName, EtcServerTable.Indate, rewardParam));

        transactions.Add(ServerData.GetItemTypeTransactionValueForAttendance(type, (int)amount));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            PopupManager.Instance.ShowAlarmMessage("보상을 받았습니다!");
            SoundManager.Instance.PlaySound("Reward");
            rewardButton.interactable = true;
        });
    }

}
