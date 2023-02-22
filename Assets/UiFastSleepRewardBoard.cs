﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiFastSleepRewardBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonDescription;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Button button;

    [SerializeField]
    private GameObject waitDescription;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.TableDatas[UserInfoTable.dailySleepRewardReceiveCount].AsObservable().Subscribe(e =>
        {

            if (e == 0)
            {
                buttonDescription.SetText($"무료 받기\n{e}/{GameBalance.fastSleepRewardMaxCount}");
            }
            else
            {
                buttonDescription.SetText($"보상 받기\n{e}/{GameBalance.fastSleepRewardMaxCount}");
            }

        }).AddTo(this);
    }

    public void OnClickAddRewardButton()
    {
        double currentSleepTime = ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value;

        if (currentSleepTime >= GameBalance.sleepRewardMaxValue)
        {
            PopupManager.Instance.ShowAlarmMessage("휴식 보상이 최대 입니다.\n먼저 휴식 보상을 사용 해 주세요!");
            return;
        }

        int rewardedCount = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.dailySleepRewardReceiveCount].Value;

        if (rewardedCount >= GameBalance.fastSleepRewardMaxCount)
        {
            PopupManager.Instance.ShowAlarmMessage("오늘은 더이상 받으실 수 없습니다.");
            return;
        }

        //
        if (rewardedCount != 0)
        {
            AdManager.Instance.ShowRewardedReward(RewardRoutine);
        }
        else
        {
            RewardRoutine();
        }
    }

    private void RewardRoutine()
    {
        button.interactable = false;
        waitDescription.SetActive(true);

        //24시간 예외처리
        ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value += GameBalance.fastSleepRewardTimeValue;

        ServerData.userInfoTable.TableDatas[UserInfoTable.dailySleepRewardReceiveCount].Value++;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param userinfoParam = new Param();
        userinfoParam.Add(UserInfoTable.sleepRewardSavedTime, ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value);
        userinfoParam.Add(UserInfoTable.dailySleepRewardReceiveCount, ServerData.userInfoTable.TableDatas[UserInfoTable.dailySleepRewardReceiveCount].Value);

        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userinfoParam));

        EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearFast, 1);
        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            button.interactable = true;
            waitDescription.SetActive(false);

            UiSleepRewardIndicator.Instance.ActiveButton();
            SleepRewardReceiver.Instance.SetComplete = false;

            PopupManager.Instance.ShowAlarmMessage("휴식보상이 추가 됐습니다(1시간)");
        });
    }
}
