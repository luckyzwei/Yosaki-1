﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using static UiTwelveRewardPopup;
public class UiGangChulRewardPopup : SingletonMono<UiGangChulRewardPopup>
{
    [SerializeField]
    private GameObject rootObject;

    private TwelveBossTableData bossTableData;

    [FormerlySerializedAs("uiTwelveBossRewardView")] [SerializeField]
    private UiGangChulBossRewardView uiGangChulBossRewardView;

    [SerializeField] private GameObject EndlockMask;

    [SerializeField]
    private TextMeshProUGUI damText;
    [SerializeField]
    private TextMeshProUGUI damRequireText;

    private TwelveBossRewardInfo requireRewardInfo;
    private void OnEnable()
    {
        Initialize(20);
    }


    public void Initialize(int bossId)
    {
        bossTableData = TableManager.Instance.TwelveBossTable.dataArray[bossId];

        var bossServerData = ServerData.bossServerTable.TableDatas[bossTableData.Stringid];

        double currentDamage = 0f;

        if (string.IsNullOrEmpty(bossServerData.score.Value) == false)
        {
            currentDamage = double.Parse(bossServerData.score.Value);
        }

        damText.SetText($"현재 피해량 : {Utils.ConvertBigNum(currentDamage)}");

        var requireIdx = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.gangchulRewardIdx).Value + 1;
        //length= 3 , requireIdx=2
        if (bossTableData.Rewardvalue.Length <= requireIdx)
        {
            // 마지막 단계 도착.
            rootObject.SetActive(false);
            EndlockMask.SetActive(true);
            requireIdx = bossTableData.Rewardvalue.Length-1;
            requireRewardInfo = new TwelveBossRewardInfo(requireIdx, bossTableData.Rewardcut[requireIdx],
                bossTableData.Rewardtype[requireIdx], bossTableData.Rewardvalue[requireIdx],
                bossTableData.Cutstring[requireIdx], currentDamage);
            damRequireText.SetText($"다음 보상 : {Utils.ConvertBigNum(bossTableData.Rewardcut[requireIdx])}");
            damRequireText.color = bossTableData.Rewardcut[requireIdx] > currentDamage ? Color.red :  Color.cyan;
        }
        else
        {
            rootObject.SetActive(true);
            EndlockMask.SetActive(false);
            requireRewardInfo = new TwelveBossRewardInfo(requireIdx, bossTableData.Rewardcut[requireIdx], bossTableData.Rewardtype[requireIdx], bossTableData.Rewardvalue[requireIdx], bossTableData.Cutstring[requireIdx], currentDamage);
            damRequireText.SetText($"다음 보상 : {Utils.ConvertBigNum(bossTableData.Rewardcut[requireIdx])}");
            damRequireText.color = bossTableData.Rewardcut[requireIdx] > currentDamage ? Color.red :  Color.cyan;
        }
        
        uiGangChulBossRewardView.Initialize(requireRewardInfo, bossServerData);
        
    }
    private Coroutine syncRoutine;

    private WaitForSeconds syncDelay = new WaitForSeconds(0.1f);
    
    public IEnumerator SyncRoutine()
    {
        yield return syncDelay;
        
        bossTableData = TableManager.Instance.TwelveBossTable.dataArray[20];

        List<TransactionValue> transactions = new List<TransactionValue>();

        var requireIdx = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.gangchulRewardIdx).Value + 1;
    
        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.gangchulRewardIdx, ServerData.userInfoTable.GetTableData(UserInfoTable.gangchulRewardIdx).Value);
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        Param goodsParam = new Param();
        goodsParam.Add(ServerData.goodsTable.ItemTypeToServerString((Item_Type)bossTableData.Rewardtype[requireIdx]), ServerData.goodsTable.GetTableData((Item_Type)bossTableData.Rewardtype[requireIdx]).Value);
        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        
        ServerData.SendTransaction(transactions, successCallBack: () =>
        {
            Debug.LogError("보내기!");
            //LogManager.Instance.SendLogType("chuseokExchange", "Costume", ((Item_Type)tableData.Itemtype).ToString());
        });
    }
    
    public void OnClickGetReward()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            PopupManager.Instance.ShowAlarmMessage("인터넷 연결을 확인해 주세요!");
            return;
        }
        if (requireRewardInfo.currentDamage < requireRewardInfo.damageCut)
        {
            PopupManager.Instance.ShowAlarmMessage("최대 피해량이 부족 합니다.");
            return;
        }
        //받은 인덱스
        var currentIdx = ServerData.userInfoTable.GetTableData(UserInfoTable.gangchulRewardIdx).Value;
        if (currentIdx >= requireRewardInfo.idx)
        {
            PopupManager.Instance.ShowAlarmMessage("이미 획득한 보상입니다.");
            return;
        }

        ServerData.AddLocalValue((Item_Type)requireRewardInfo.rewardType, requireRewardInfo.rewardAmount);
        ServerData.userInfoTable.GetTableData(UserInfoTable.gangchulRewardIdx).Value++;
        Initialize(20);
        PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.GrowthStone)} {Utils.ConvertBigNum(requireRewardInfo.rewardAmount)}개 획득!");
        if (syncRoutine != null)
        {
            CoroutineExecuter.Instance.StopCoroutine(syncRoutine);
        }
        syncRoutine = CoroutineExecuter.Instance.StartCoroutine(SyncRoutine());
        
    }
}
