using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiGangChulView : SingletonMono<UiGangChulView>
{
    [SerializeField]
    private UiTwelveBossContentsView bossContentsView;

    [SerializeField]
    private GameObject guildRecordObject;

    public ObscuredInt rewardGrade = 0;

    [SerializeField]
    private Button recordButton;
  
    [SerializeField]
    private Button resetButton;
    
    void Start()
    {
        Initialize();

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable2.TableDatas[UserInfoTable2.GangChulReset].AsObservable().Subscribe(e => { resetButton.interactable = e == 0; }).AddTo(this);
    }
    private void OnEnable()
    {
 
    }

    private void Initialize()
    {
        
    }

    public void RecordGuildScoreButton()
    {
      
    }
    
    public void OnClickGangChulRewardResetButton()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            PopupManager.Instance.ShowAlarmMessage("인터넷 연결이 불안정 합니다 잠시후 다시 시도해 주세요");
            return;
        }

        if (ServerData.userInfoTable2.TableDatas[UserInfoTable2.GangChulReset].Value > 0)
        {
            PopupManager.Instance.ShowAlarmMessage("강철이 보상은 월 1회만 가능 합니다!\n(매월 1일 초기화)");
            return;
        }
        
        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice,"강철이 보상을 초기화 할까요?\n(월 1회 초기화 가능)", () =>
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                PopupManager.Instance.ShowAlarmMessage("인터넷 연결이 불안정 합니다 잠시후 다시 시도해 주세요");
                return;
            }
            
            if (ServerData.userInfoTable2.TableDatas[UserInfoTable2.GangChulReset].Value > 0)
            {
                PopupManager.Instance.ShowAlarmMessage("강철이 보상은 월 1회만 가능 합니다!\n(매월 1일 초기화)");
                return;
            }
            
            ServerData.userInfoTable.TableDatas[UserInfoTable.gangchulRewardIdx].Value = -1;
            ServerData.userInfoTable2.TableDatas[UserInfoTable2.GangChulReset].Value = 1;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param UserInfoParam = new Param();
            UserInfoParam.Add(UserInfoTable.gangchulRewardIdx, ServerData.userInfoTable.TableDatas[UserInfoTable.gangchulRewardIdx].Value);

            Param UserInfo2Param = new Param();
            UserInfo2Param.Add(UserInfoTable2.GangChulReset, ServerData.userInfoTable2.TableDatas[UserInfoTable2.GangChulReset].Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, UserInfoParam));
            transactions.Add(TransactionValue.SetUpdate(UserInfoTable2.tableName, UserInfoTable2.Indate, UserInfo2Param));

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                UiGangChulRewardPopup.Instance.Initialize(20);
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "강철이 보상 초기화 완료!", null);
            });
            
        },null);
        
    }
}
