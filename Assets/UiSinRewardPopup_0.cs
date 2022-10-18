﻿using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UiTwelveRewardPopup;

public class UiSinRewardPopup_0 : SingletonMono<UiSinRewardPopup_0>
{
    [SerializeField]
    private GameObject rootObject;

    private TwelveBossTableData bossTableData;

    [SerializeField]
    private UiTwelveBossRewardView uiTwelveBossRewardView;

    private List<UiTwelveBossRewardView> uiTwelveBossRewardViews = new List<UiTwelveBossRewardView>();

    [SerializeField]
    private Transform cellParent;

    [SerializeField]
    private TextMeshProUGUI RecommendCount;

    [SerializeField]
    private TextMeshProUGUI damText;

    public int bossId = 13;

    private void OnEnable()
    {
        Initialize(bossId);
    }


    public void Initialize(int bossId)
    {
        var bossTableData = TableManager.Instance.TwelveBossTable.dataArray[bossId];

        var bossServerData = ServerData.bossServerTable.TableDatas[bossTableData.Stringid];

        double currentDamage = 0f;

        if (string.IsNullOrEmpty(bossServerData.score.Value) == false)
        {
            currentDamage = double.Parse(bossServerData.score.Value);
        }

        if (damText != null)
        {
            damText.SetText($"최고 피해량 : {Utils.ConvertBigNum(currentDamage)}");
        }

        if (RecommendCount != null)
        {
            RecommendCount.SetText($"받은 추천 : {ServerData.bossServerTable.TableDatas["b68"].score.Value}");
        }

        if (rootObject != null)
        {
            rootObject.SetActive(true);
        }

        bossTableData = TableManager.Instance.TwelveBossTable.dataArray[bossId];

        int makeCellAmount = bossTableData.Rewardcut.Length - uiTwelveBossRewardViews.Count;

        for (int i = 0; i < makeCellAmount; i++)
        {
            var cell = Instantiate<UiTwelveBossRewardView>(uiTwelveBossRewardView, cellParent);

            uiTwelveBossRewardViews.Add(cell);
        }

        if (bossId != 24)
        {
            for (int i = 0; i < uiTwelveBossRewardViews.Count; i++)
            {
                if (i < bossTableData.Rewardcut.Length)
                {
                    uiTwelveBossRewardViews[i].gameObject.SetActive(true);

                    TwelveBossRewardInfo info = new TwelveBossRewardInfo(i, bossTableData.Rewardcut[i], bossTableData.Rewardtype[i], bossTableData.Rewardvalue[i], bossTableData.Cutstring[i], currentDamage);

                    uiTwelveBossRewardViews[i].Initialize(info, bossServerData);
                }
                else
                {
                    uiTwelveBossRewardViews[i].gameObject.SetActive(false);
                }

            }
        }
        else
        {
            for (int i = 0; i < uiTwelveBossRewardViews.Count; i++)
            {
                if (i < bossTableData.Rewardcut.Length)
                {
                    if (i == 12)
                    {
                        uiTwelveBossRewardViews[i].gameObject.SetActive(true);

                        TwelveBossRewardInfo info = new TwelveBossRewardInfo(14, bossTableData.Rewardcut[14], bossTableData.Rewardtype[14], bossTableData.Rewardvalue[14], bossTableData.Cutstring[14], currentDamage);

                        uiTwelveBossRewardViews[i].Initialize(info, bossServerData);
                    }
                    else if (i == 13)
                    {
                        uiTwelveBossRewardViews[i].gameObject.SetActive(true);

                        TwelveBossRewardInfo info = new TwelveBossRewardInfo(12, bossTableData.Rewardcut[12], bossTableData.Rewardtype[12], bossTableData.Rewardvalue[12], bossTableData.Cutstring[12], currentDamage);

                        uiTwelveBossRewardViews[i].Initialize(info, bossServerData);
                    }
                    else if (i == 14)
                    {
                        uiTwelveBossRewardViews[i].gameObject.SetActive(true);

                        TwelveBossRewardInfo info = new TwelveBossRewardInfo(13, bossTableData.Rewardcut[13], bossTableData.Rewardtype[13], bossTableData.Rewardvalue[13], bossTableData.Cutstring[13], currentDamage);

                        uiTwelveBossRewardViews[i].Initialize(info, bossServerData);
                    }
                    else
                    {
                        uiTwelveBossRewardViews[i].gameObject.SetActive(true);

                        TwelveBossRewardInfo info = new TwelveBossRewardInfo(i, bossTableData.Rewardcut[i], bossTableData.Rewardtype[i], bossTableData.Rewardvalue[i], bossTableData.Cutstring[i], currentDamage);

                        uiTwelveBossRewardViews[i].Initialize(info, bossServerData);
                    }

                }
                else
                {
                    uiTwelveBossRewardViews[i].gameObject.SetActive(false);
                }

            }
        }

    }

    public void OnClickAllReceiveButton()
    {
        //int rewardCount = 0;

        //for (int i = 0; i < uiTwelveBossRewardViews.Count; i++)
        //{
        //    bool hasReward = uiTwelveBossRewardViews[i].GetRewardByScript();

        //    if (hasReward)
        //    {
        //        rewardCount++;
        //    }
        //}

        //if (rewardCount != 0)
        //{
        //    List<TransactionValue> transactions = new List<TransactionValue>();

        //    Param bossParam = new Param();
        //    bossParam.Add("boss12", ServerData.bossServerTable.TableDatas["boss12"].ConvertToString());
        //    transactions.Add(TransactionValue.SetUpdate(BossServerTable.tableName, BossServerTable.Indate, bossParam));

        //    Param goodsParam = new Param();
        //    goodsParam.Add(GoodsTable.GuildReward, ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value);
        //    transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

        //    ServerData.SendTransaction(transactions, successCallBack: () =>
        //    {
        //        PopupManager.Instance.ShowAlarmMessage("보상을 받았습니다!");
        //        SoundManager.Instance.PlaySound("Reward");
        //    });
        //}
        //else
        //{
        //    PopupManager.Instance.ShowAlarmMessage("받을수 있는 보상이 없습니다.");
        //}
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.userInfoTable.TableDatas[UserInfoTable.LastLogin].Value = 0;
        }
    }
#endif
}
