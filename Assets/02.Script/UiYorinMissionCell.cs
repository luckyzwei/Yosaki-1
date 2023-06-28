using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BackEnd;

public class UiYorinMissionCell : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private TextMeshProUGUI description;
    
    [SerializeField]
    private TextMeshProUGUI ItemName;
    [SerializeField]
    private TextMeshProUGUI ItemAmount;

    [SerializeField]
    private Button getButton;
    
    [SerializeField]
    private TextMeshProUGUI buttonText;

    
    [SerializeField]
    private GameObject rewardedText;

    private YorinMissionData tableData;
    
    [SerializeField]
    private Image itemIcon;

    
    [SerializeField]
    private GameObject lockMask;


    [SerializeField]
    private Image buttonImage;
    [SerializeField] private Sprite greenButtonSprite;
    [SerializeField] private Sprite redButtonSprite;

    public int dayIdx=0;
    public void Initialize(YorinMissionData tableData)
    {
        if (tableData.Enable == false)
        {
            this.gameObject.SetActive(false);
            return;
        }
        
        this.tableData = tableData;

        dayIdx = tableData.Missionday;
        
        UpdateUi();

        Subscribe();
    }

    private void UpdateUi()
    {
        title.SetText(tableData.Title);
        
        description.SetText(tableData.Description);
        
        ItemName.SetText($"{CommonString.GetItemName((Item_Type)tableData.Reward1)}");
        
        ItemAmount.SetText($"{Utils.ConvertNum(tableData.Reward1_Value)}");
    
        itemIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)(int)tableData.Reward1);

        getButton.gameObject.SetActive(ServerData.yorinMissionServerTable.CheckMissionRewardCount(tableData.Stringid) < 1);//보상을 받은적이 없으면 true(보여야함)

        rewardedText.SetActive(ServerData.yorinMissionServerTable.CheckMissionRewardCount(tableData.Stringid) > 0);

    }
    
    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_100Day).AsObservable().Subscribe(e =>
        {
            lockMask.SetActive(e < tableData.Missionday);
        }).AddTo(this);
        
        ServerData.yorinMissionServerTable.TableDatas[tableData.Stringid].clearCount.AsObservable().Subscribe(WhenMissionCountChanged).AddTo(this);
        
        ServerData.yorinMissionServerTable.TableDatas[tableData.Stringid].rewardCount.AsObservable().Subscribe(e=>
        {
            if (e > 0)
            {
                buttonText.SetText("획득완료");
                this.gameObject.transform.SetAsLastSibling();
            }
            else
            {
                buttonText.SetText("보상 받기");
            }
        }).AddTo(this);
    }

    private bool IsLogMission(YorinMissionKey type)
    {

        return type == YorinMissionKey.YMission1_4 ||
               type == YorinMissionKey.YMission1_6 ||
               type == YorinMissionKey.YMission2_2 ||
               type == YorinMissionKey.YMission2_6 ||
               type == YorinMissionKey.YMission3_1 ||
               type == YorinMissionKey.YMission3_3 ||
               type == YorinMissionKey.YMission4_2 ||
               type == YorinMissionKey.YMission4_3 ||
               type == YorinMissionKey.YMission5_4 ||
               type == YorinMissionKey.YMission5_6 ||
               type == YorinMissionKey.YMission6_1 ||
               type == YorinMissionKey.YMission6_4 ||
               type == YorinMissionKey.YMission7_2 ||
               type == YorinMissionKey.YMission7_5;
    } 
    public void ReArrange()
    { 
        if (ServerData.yorinMissionServerTable.TableDatas[tableData.Stringid].rewardCount.Value > 0)
        {
            buttonText.SetText("획득완료");
            this.gameObject.transform.SetAsLastSibling();
        }
        else
        {
            buttonText.SetText("보상 받기");
        }
    }
    private void OnEnable()
    {
        if (tableData != null)
        {
            WhenMissionCountChanged(ServerData.yorinMissionServerTable.TableDatas[tableData.Stringid].clearCount.Value);
        }
    }

    private void WhenMissionCountChanged(int count)
    {
        if (this.gameObject.activeInHierarchy == false) return;

        if (count >= tableData.Rewardrequire)
        {
            buttonImage.sprite = greenButtonSprite;
        }
        else
        {
            buttonImage.sprite = redButtonSprite;
        }
    }


    public void OnClickGetButton()
    {
        if (ServerData.yorinMissionServerTable.CheckMissionClearCount(tableData.Stringid) < tableData.Rewardrequire)
        {
            //모두받기용이며 클릭버튼은 interactable false로 막혀있기에 알람안띄움.
            PopupManager.Instance.ShowAlarmMessage($"임무를 클리어 해야 합니다!");
            return;
        }
        
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_100Day).Value < tableData.Missionday)
        {
            PopupManager.Instance.ShowAlarmMessage($"해당 보상은 {tableData.Missionday}일차에 획득할 수 있습니다!");
            return;
        }
        
        if (ServerData.yorinMissionServerTable.CheckMissionRewardCount(tableData.Stringid) > 0)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 획득하셨습니다!");
            return;
        }


        if (Utils.IsSleepItem((Item_Type)tableData.Reward1))
        {
            double currentSleepTime = ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value;

            if (currentSleepTime >= 1800)
            {
                PopupManager.Instance.ShowAlarmMessage("휴식보상을 사용 후 수령해주세요!");
                return;
            }
            UiSleepRewardIndicator.Instance.ActiveButton();
            SleepRewardReceiver.Instance.SetComplete = false;
            
            SoundManager.Instance.PlaySound("GoldUse");
        
            YorinMissionManager.UpdateYorinMissionReward((YorinMissionKey)(tableData.Id), 1);
        
            List<TransactionValue> transactionList = new List<TransactionValue>();

            transactionList.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)tableData.Reward1, tableData.Reward1_Value));
        
            ServerData.SendTransaction(transactionList, successCallBack: () =>
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type)tableData.Reward1)} {Utils.ConvertNum(tableData.Reward1_Value)}개 획득!!");
                UpdateUi();
                if (IsLogMission((YorinMissionKey)tableData.Id))
                {
                    LogManager.Instance.SendLogType("Funnel_Tutorial","complete",$"newrin_{tableData.Stringid}");
                }
            });

        }
        else
        {
            SoundManager.Instance.PlaySound("GoldUse");
        
            YorinMissionManager.UpdateYorinMissionReward((YorinMissionKey)(tableData.Id), 1);
        
            List<TransactionValue> transactionList = new List<TransactionValue>();

            transactionList.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)tableData.Reward1, tableData.Reward1_Value));
        
            ServerData.SendTransaction(transactionList, successCallBack: () =>
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type)tableData.Reward1)} {Utils.ConvertNum(tableData.Reward1_Value)}개 획득!!");
                UpdateUi();
                if (IsLogMission((YorinMissionKey)tableData.Id))
                {
                    LogManager.Instance.SendLogType("Funnel_Tutorial","complete",$"newrin_{tableData.Stringid}");
                }
            });
        }
       
        
    }
    
    public Tuple<Item_Type, float> OnClickGetButtonAll()
    {
        
        if (ServerData.yorinMissionServerTable.CheckMissionClearCount(tableData.Stringid) < tableData.Rewardrequire)
        {
            return new Tuple<Item_Type, float>(Item_Type.None, 0);
        }

        if (ServerData.userInfoTable.GetTableData(UserInfoTable.attendanceCount_100Day).Value < tableData.Missionday)
        {
            return new Tuple<Item_Type, float>(Item_Type.None, 0);
        }
        
        if (ServerData.yorinMissionServerTable.CheckMissionRewardCount(tableData.Stringid) > 0)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 획득하셨습니다!");
            return new Tuple<Item_Type, float>(Item_Type.None, 0);
        }

        if (Utils.IsSleepItem((Item_Type)tableData.Reward1))
        {
            PopupManager.Instance.ShowAlarmMessage("휴식 보상은 직접 수령해주세요!");
            return new Tuple<Item_Type, float>(Item_Type.None, 0);
        }
        SoundManager.Instance.PlaySound("GoldUse");
        
        YorinMissionManager.UpdateYorinMissionReward((YorinMissionKey)(tableData.Id), 1);
        
        List<TransactionValue> transactionList = new List<TransactionValue>();

        transactionList.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)tableData.Reward1, tableData.Reward1_Value));
        
        ServerData.SendTransaction(transactionList, successCallBack: () =>
        {
            //PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName((Item_Type)tableData.Reward1)} {Utils.ConvertNum(tableData.Reward1_Value)}개 획득!!");
            UpdateUi();
            if (IsLogMission((YorinMissionKey)tableData.Id))
            {
                LogManager.Instance.SendLogType("Funnel_Tutorial","complete",$"newrin_{tableData.Stringid}");
            }
        });
        return new Tuple<Item_Type, float>((Item_Type)tableData.Reward1, tableData.Reward1_Value);
        
    }

}
