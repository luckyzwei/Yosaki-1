using System;
using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiNewTitleLevelCell : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI currentAbilDescription;
    
    private Title_LevelData requireTableData;

    [SerializeField] private GameObject lockMask;
    
    [SerializeField] private Button rewardButton;
    
    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI rewardAmount;

    private void OnEnable()
    {
        Initialize();
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).AsObservable().Subscribe(e =>
        {
            ButtonInteractableCheck();
            lockMask.SetActive(e + 1 >= TableManager.Instance.titleLevel.dataArray.Length);
        }).AddTo(this);
    }

    private void ButtonInteractableCheck()
    {
        rewardButton.interactable = ServerData.statusTable.GetTableData(StatusTable.Level).Value >=
                                    requireTableData.Condition;
    }
    
    public void Initialize()
    {
        PlayerStats.ResetTitleHas();
        var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value;


        requireTableData = TableManager.Instance.titleLevel.dataArray[Mathf.Min((int)currentLevel + 1,TableManager.Instance.titleLevel.dataArray.Length - 1)];
        

        description.SetText(requireTableData.Description);

        abilDescription.SetText(GetAbilDescription());

        itemIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)requireTableData.Rewardtype);

        rewardAmount.SetText(Utils.ConvertBigNum(requireTableData.Rewardvalue));
        
        currentAbilDescription.SetText($"{CommonString.GetStatusName((StatusType)requireTableData.Abiltype1)} {Utils.ConvertBigNum(PlayerStats.GetTitleLevelAbilValue((StatusType)requireTableData.Abiltype1))} 증가");
        
        ButtonInteractableCheck();
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
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value + 1 >=
            TableManager.Instance.titleLevel.dataArray.Length)
        {
            PopupManager.Instance.ShowAlarmMessage("다음 업데이트를 기다려주세요!");
            return;
        }
        
        var currentLevel = ServerData.statusTable.GetTableData(StatusTable.Level).Value;
        if (currentLevel < requireTableData.Condition)
        {
            PopupManager.Instance.ShowAlarmMessage("레벨이 낮습니다.");
            return;
        }

        ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value++;
        
        List<TransactionValue> transactions = new List<TransactionValue>();

        Param userinfoParam = new Param();

        userinfoParam.Add(UserInfoTable.titleLevel,
            ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value);
        
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userinfoParam));

        transactions.Add(ServerData.GetItemTypeTransactionValueForAttendance((Item_Type)requireTableData.Rewardtype, requireTableData.Rewardvalue));
        
        Initialize();
        
        ServerData.SendTransaction(transactions, successCallBack: () =>
          {
              PopupManager.Instance.ShowAlarmMessage( "보상 수령 완료!");
              ButtonInteractableCheck();
              //     LogManager.Instance.SendLogType("TitleReward", tableData.Id.ToString(), "");
          });
    }


}
