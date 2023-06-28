using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;

public class YorinAttendCell : MonoBehaviour
{

    private YorinAttendData tableData;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Button getButton;

    [SerializeField]
    private TextMeshProUGUI buttonText;

    [SerializeField] private Image buttonImage;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.yorinAttendRewarded).AsObservable().Subscribe(e =>
        {
            UpdateUi();
        }).AddTo(this);
    }
    public void Initialize(YorinAttendData data)
    {
        tableData = data;   
        UpdateUi();
    }

    private void UpdateUi()
    {
        titleText.SetText($"요린이\n{tableData.Unlockday}일차 출석체크");


        string descrpition = "";
        for (int i = 0; i < tableData.Reward.Length; i++)
        {
            if (Utils.IsSleepItem((Item_Type)tableData.Reward[i]))
            {
                descrpition += $"<color=yellow>{CommonString.GetItemName((Item_Type)tableData.Reward[i])}</color> \n";
            }
            else
            {
                descrpition += CommonString.GetItemName((Item_Type)tableData.Reward[i]) + Utils.ConvertNum(tableData.Reward_Value[i])+"개\n";
            }
        }
        descriptionText.SetText($"{descrpition}");
        if (CanGetReward())
        {
            getButton.interactable = true;
        }
        else
        {
            getButton.interactable = false;
        }
        if (IsRewarded())
        {
            buttonText.SetText("획득 완료");
        }
        else
        {
            buttonText.SetText("보상 받기");
        }
        buttonImage.enabled = IsRewarded()==false;
    }
    private bool CanGetReward()
    {
        return ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_100Day).Value >= tableData.Unlockday;
    }

    private bool IsBeforeRewarded()
    {
        //0일때 1
        return (int)ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.yorinAttendRewarded).Value + 1 == tableData.Unlockday;
    }

    private bool IsRewarded()
    {
        return (int)ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.yorinAttendRewarded).Value >= tableData.Unlockday;
    }

    public void OnClickButton()
    {
        if (CanGetReward()==false)
        {
            PopupManager.Instance.ShowAlarmMessage("일 수가 부족합니다!");
            return;
        }
        else if (IsRewarded()==true)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 보상을 받았습니다!");
            return;
        }
        else if (IsBeforeRewarded()==false)
        {
            PopupManager.Instance.ShowAlarmMessage("이전 보상을 받아주세요!");
            return;
        }
        double currentSleepTime = ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value;

        if (currentSleepTime >= 1800)
        {
            PopupManager.Instance.ShowAlarmMessage("휴식 보상은 메인 화면 좌측 오프라인 휴식 보상 터치를 통해서 획득할 수 있습니다 ! ");
            return;
        }
        List<TransactionValue> transactions = new List<TransactionValue>();
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.yorinAttendRewarded).Value++;
        Param goodsParam = new Param();
        for (int i = 0; i < tableData.Reward.Length; i++)
        {
            if (Utils.IsSleepItem((Item_Type)tableData.Reward[i]))
            {
                transactions.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)tableData.Reward[i], tableData.Reward_Value[i]));
                UiSleepRewardIndicator.Instance.ActiveButton();
                SleepRewardReceiver.Instance.SetComplete = false;
            }
            else
            {
                ServerData.goodsTable.GetTableData((Item_Type)tableData.Reward[i]).Value += tableData.Reward_Value[i];
                goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward[i]), ServerData.goodsTable.GetTableData(ServerData.goodsTable.ItemTypeToServerString((Item_Type)tableData.Reward[i])).Value);    
            }
            
        }


        Param userInfo2Param = new Param();
        userInfo2Param.Add(UserInfoTable_2.yorinAttendRewarded, ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.yorinAttendRewarded].Value);
        
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, userInfo2Param));

  

        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            PopupManager.Instance.ShowAlarmMessage("보상획득!");
        });
        
    }
}
