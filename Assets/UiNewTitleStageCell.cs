using System;
using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiNewTitleStageCell : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI currentAbilDescription;
    
    private Title_StageData requireTableData;

    [SerializeField] private Button rewardButton;
    
    [SerializeField]
    private Image itemIcon;

    [SerializeField] private GameObject lockMask;
    
    [SerializeField]
    private TextMeshProUGUI rewardAmount;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).AsObservable().Subscribe(e =>
        {
            ButtonInteractableCheck();
            //다음단계가 Length 이상이 될시.
            lockMask.SetActive(e + 1 >= TableManager.Instance.titleStage.dataArray.Length);
        }).AddTo(this);
    }
    private void OnEnable()
    {
        Initialize();
    }


    public void Initialize()
    {
        var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value;

        if (currentLevel + 1 >= TableManager.Instance.titleStage.dataArray.Length)
        {
            lockMask.SetActive(true);
        }
        else
        {
            lockMask.SetActive(false);
        }
        requireTableData = TableManager.Instance.titleStage.dataArray[Mathf.Min((int)currentLevel + 1,TableManager.Instance.titleStage.dataArray.Length - 1)];
        
        description.SetText(requireTableData.Description);

        abilDescription.SetText(GetAbilDescription());

        itemIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)requireTableData.Rewardtype);

        rewardAmount.SetText(Utils.ConvertBigNum(requireTableData.Rewardvalue));
        
        currentAbilDescription.SetText($"{CommonString.GetStatusName((StatusType)requireTableData.Abiltype1)} {Utils.ConvertBigNum(PlayerStats.GetTitleStageAbilValue((StatusType)requireTableData.Abiltype1)*100)}% 증가");
    }
    private void ButtonInteractableCheck()
    {
        rewardButton.interactable = ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value >=
                                    requireTableData.Condition;
    }
    private string GetAbilDescription()
    {
        StatusType type = (StatusType)requireTableData.Abiltype1;

        string abilValue = string.Empty;

        if (type.IsPercentStat())
        {
            float abil =   (requireTableData.Abilvalue1 * 100);

            abilValue = Utils.ConvertBigNum(abil) + "%";
        }
        else
        {
            float abil =  (requireTableData.Abilvalue1);

            abilValue = Utils.ConvertBigNum(abil);
        }

        return $"{CommonString.GetStatusName(type)} {abilValue}";
    }


    public void OnClickRewardButton()
    {

        if (ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value + 1 >=
            TableManager.Instance.titleStage.dataArray.Length)
        {
            PopupManager.Instance.ShowAlarmMessage("다음 업데이트를 기다려주세요!");
            return;
        }

        var currentStage = ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value;
        if (currentStage < requireTableData.Condition)
        {
            PopupManager.Instance.ShowAlarmMessage("스테이지가 낮습니다.");
            return;
        }

        ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value++;
        
        List<TransactionValue> transactions = new List<TransactionValue>();

        Param userinfoParam = new Param();

        userinfoParam.Add(UserInfoTable.titleStage,
            ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value);
        
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userinfoParam));

        transactions.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)requireTableData.Rewardtype, requireTableData.Rewardvalue));
        PlayerStats.ResetTitleHas();
        Initialize();
        
        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            PopupManager.Instance.ShowAlarmMessage( "보상 수령 완료!");
            ButtonInteractableCheck();
            //     LogManager.Instance.SendLogType("TitleReward", tableData.Id.ToString(), "");
        });
    }


}
