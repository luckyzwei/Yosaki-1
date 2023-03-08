using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiRewardCollection : MonoBehaviour
{
    [SerializeField]
    private Button oniButton;

    [SerializeField]
    private Button oniButton2;

    [SerializeField]
    private Button baekGuiButton;

    [SerializeField]
    private Button sonButton;

    [SerializeField]
    private Button gangChulButton;

    [SerializeField]
    private Button gumGiButton;

    [SerializeField]
    private Button hellFireButton;

    [SerializeField]
    private Button chunFlowerButton;

    [SerializeField]
    private Button hellRelicButton;

    [SerializeField]
    private Button dokebiClearButton;

    [SerializeField]
    private Button sumiClearButton;

    [SerializeField]
    private Button newGachaButton;

    [SerializeField]
    private GameObject Bandi0;
    [SerializeField]
    private GameObject Bandi1;
    
    [SerializeField]
    private GameObject SonObject;
    [SerializeField]
    private GameObject HelObject;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.statusTable.GetTableData(StatusTable.Level).AsObservable().Subscribe(e =>
        {
            oniButton.interactable = e >= 300;
            baekGuiButton.interactable = e >= 3000;
            sonButton.interactable = e >= 5000;
            gangChulButton.interactable = e >= 30000;
            gumGiButton.interactable = e >= 50000;
            hellFireButton.interactable = e >= 50000;
            chunFlowerButton.interactable = e >= 200000;
            hellRelicButton.interactable = e >= 50000;
            dokebiClearButton.interactable = e >= 500000;
            sumiClearButton.interactable = e >= 1000000;
            oniButton2.interactable = e >= 500000;


            if (e >= GameBalance.banditUpgradeLevel)
            {
                Bandi0.SetActive(false);
            }
            else
            {
                Bandi1.SetActive(false);
            }
        }).AddTo(this);

        ServerData.userInfoTable.GetTableData(UserInfoTable.relicKillCount).AsObservable().Subscribe(e =>
        {
            newGachaButton.interactable = e >= 25000;
        }).AddTo(this);

        ServerData.userInfoTable.GetTableData(UserInfoTable.graduateSon).AsObservable().Subscribe(e =>
        {
            SonObject.SetActive(e == 0);
        }).AddTo(this);
        ServerData.userInfoTable.GetTableData(UserInfoTable.graduateHel).AsObservable().Subscribe(e =>
        {
            HelObject.SetActive(e == 0);
        }).AddTo(this);
    }

    public void OnClickBanditReward()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonEnterCount).Value >= GameBalance.bonusDungeonEnterCount)
        {
            PopupManager.Instance.ShowAlarmMessage("오늘은 더이상 소탕할 수 없습니다.");
            return;
        }

        int killCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonMaxKillCount).Value;

        if (killCount == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("기록이 없습니다.");
            return;
        }

        int clearCount = GameBalance.bonusDungeonEnterCount - (int)ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonEnterCount).Value;

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"처치 <color=yellow>{killCount}</color>로 <color=yellow>{clearCount}회</color> 소탕 합니까?\n{CommonString.GetItemName(Item_Type.Jade)} {killCount * GameBalance.bonusDungeonGemPerEnemy * (GameBalance.bandiPlusStageJadeValue * (int)Mathf.Floor(Mathf.Max(1000f, (float)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value) + 2) / GameBalance.bandiPlusStageDevideValue) * clearCount}개\n{CommonString.GetItemName(Item_Type.Marble)} {killCount * GameBalance.bonusDungeonMarblePerEnemy * (GameBalance.bandiPlusStageMarbleValue * (int)Mathf.Floor(Mathf.Max(1000f, (float)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value) + 2) / GameBalance.bandiPlusStageDevideValue) * clearCount}개", () =>
            {
                // enterButton.interactable = false;
                if (ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonEnterCount).Value >= GameBalance.bonusDungeonEnterCount)
                {
                    PopupManager.Instance.ShowAlarmMessage("오늘은 더이상 소탕할 수 없습니다.");
                    return;
                }

                int rewardNumJade = (killCount * GameBalance.bonusDungeonGemPerEnemy) * (GameBalance.bandiPlusStageJadeValue * (int)Mathf.Floor(Mathf.Max(1000f, (float)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value) + 2) / GameBalance.bandiPlusStageDevideValue) * clearCount;
                int rewardNumMarble = killCount * GameBalance.bonusDungeonMarblePerEnemy * (GameBalance.bandiPlusStageMarbleValue * (int)Mathf.Floor(Mathf.Max(1000f, (float)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value) + 2) / GameBalance.bandiPlusStageDevideValue) * clearCount;
                ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonEnterCount).Value += clearCount;

                ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += rewardNumJade;
                ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += rewardNumMarble;

                //데이터 싱크
                List<TransactionValue> transactionList = new List<TransactionValue>();

                Param goodsParam = new Param();
                goodsParam.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
                goodsParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
                transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

                Param userInfoParam = new Param();
                userInfoParam.Add(UserInfoTable.bonusDungeonEnterCount, ServerData.userInfoTable.GetTableData(UserInfoTable.bonusDungeonEnterCount).Value);
                transactionList.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

                ServerData.SendTransaction(transactionList,
                    successCallBack: () =>
                    {
                        DailyMissionManager.UpdateDailyMission(DailyMissionKey.ClearBonusDungeon, 1);
                    },
                    completeCallBack: () =>
                    {
                        // enterButton.interactable = true;
                    });

                EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearBandit, clearCount);
                EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearBandit, clearCount);

                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{clearCount}회 소탕 완료!\n{CommonString.GetItemName(Item_Type.Jade)} {rewardNumJade}개\n{CommonString.GetItemName(Item_Type.Marble)} {rewardNumMarble}개 획득!", null);
                SoundManager.Instance.PlaySound("GoldUse");


            }, null);

    }//★

    public void OnClickDayofWeekReward()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getDayOfWeek).Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"요일 보상은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.DayOfWeekClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }


        var tabledata = TableManager.Instance.dayOfWeekDungeon.dataArray;


        float multipleValue = 0f;
        for (int i = 0; i < tabledata[GetDayOfweek()].Score.Length; i++)
        {
            //통과
            if (tabledata[GetDayOfweek()].Score[i] <= ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value)
            {
                multipleValue = tabledata[GetDayOfweek()].Rewardvalue[i];
            }
            //정지
            else
            {
                if (i == 0)
                {
                    multipleValue = 1f;
                }
                break;
            }
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score * multipleValue}개 획득 합니까?", () =>
        {
            if (ServerData.userInfoTable.GetTableData(UserInfoTable.getDayOfWeek).Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"요일 보상은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.GetTableData(UserInfoTable.getDayOfWeek).Value = 1;


            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getDayOfWeek, ServerData.userInfoTable.TableDatas[UserInfoTable.getDayOfWeek].Value);



            ServerData.goodsTable.GetTableData(tabledata[GetDayOfweek()].Rewardstring).Value += score * multipleValue;
            Param goodsParam = new Param();
            goodsParam.Add(tabledata[GetDayOfweek()].Rewardstring, ServerData.goodsTable.GetTableData(tabledata[GetDayOfweek()].Rewardstring).Value);


            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            DailyMissionManager.UpdateDailyMission(DailyMissionKey.ClearBonusDungeon, 10);

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearBandit, 1);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearBandit, 1);

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName((Item_Type)tabledata[GetDayOfweek()].Rewardtype)} {score * multipleValue}개 획득!", null);
            });
        }, null);
    }//★

    public void OnClickOniReward()
    {
        //난이도 고정
        int idx = 3;

        int currentEnterCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiEnterCount).Value;

        if (currentEnterCount >= GameBalance.dokebiEnterCount)
        {
            PopupManager.Instance.ShowAlarmMessage("오늘은 더이상 소탕할 수 없습니다.");
            return;
        }

        int defeatCount = 0;

        int clearCount = GameBalance.dokebiEnterCount - currentEnterCount;

        if (idx == 0)
        {
            defeatCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount0).Value;
        }
        else if (idx == 1)
        {
            defeatCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount1).Value;
        }
        else if (idx == 2)
        {
            defeatCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount2).Value;
        }
        else if (idx == 3)
        {
            defeatCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiKillCount3).Value;
        }

        if (defeatCount == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("플레이 데이터가 없습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.Dokebi)} <color=yellow>{defeatCount}</color>개로 <color=yellow>{clearCount}회</color> 소탕 합니까?", () =>
        {
            int currentEnterCount = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiEnterCount).Value;

            if (currentEnterCount >= GameBalance.dokebiEnterCount)
            {
                PopupManager.Instance.ShowAlarmMessage("오늘은 더이상 소탕할 수 없습니다.");
                return;
            }

            GuideMissionManager.UpdateGuideMissionClear(GuideMissionKey.ClearOni);

            int rewardNum = defeatCount;

            ServerData.goodsTable.GetTableData(GoodsTable.DokebiKey).Value += rewardNum * clearCount;

            ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiEnterCount).Value += clearCount;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param goodsParam = new Param();

            goodsParam.Add(GoodsTable.DokebiKey, ServerData.goodsTable.GetTableData(GoodsTable.DokebiKey).Value);

            Param userInfoParam = new Param();

            userInfoParam.Add(UserInfoTable.dokebiEnterCount, ServerData.userInfoTable.GetTableData(UserInfoTable.dokebiEnterCount).Value);

            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearOni, clearCount);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearOni, clearCount);
            ServerData.SendTransaction(transactions, successCallBack: () =>
            {

                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.Dokebi)} {rewardNum * clearCount}개 획득!");

                //사운드
                SoundManager.Instance.PlaySound("Reward");
                //LogManager.Instance.SendLog("DokClear", $"{rewardNum}개 획득 {ServerData.goodsTable.GetTableData(GoodsTable.DokebiKey).Value}");
            });
        }, null);
    }//★

    public void OnClickBaekguiReward()
    {

    }//★

    public void OnClickSonReward()
    {

    }

    public void OnClickGumgiReward()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.getGumGi].Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SP)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score}개 획득 합니까?\n<color=red>(하루 한번만 획득 가능)</color>\n{CommonString.GetItemName(Item_Type.DokebiTreasure)}로 추가획득 : {Utils.GetDokebiTreasureAddValue()}", () =>
        {
            if (ServerData.userInfoTable.TableDatas[UserInfoTable.getGumGi].Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SP)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.TableDatas[UserInfoTable.getGumGi].Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value += score + Utils.GetDokebiTreasureAddValue();

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getGumGi, ServerData.userInfoTable.TableDatas[UserInfoTable.getGumGi].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.SwordPartial, ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearSwordPartial, 1);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearSwordPartial, 1);

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                LogManager.Instance.SendLogType("GumGi", "_", score.ToString());
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.SP)} {score + Utils.GetDokebiTreasureAddValue()}개 획득!", null);
            });
        }, null);
    }

    public void OnClickHellFireReward()
    {

    }

    public void OnClickChunFlowerReward()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.getFlower].Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.Cw)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.flowerClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score}개 획득 합니까?\n<color=red>(하루 한번만 획득 가능)</color>\n{CommonString.GetItemName(Item_Type.DokebiTreasure)}로 추가획득 : {Utils.GetDokebiTreasureAddValue()}", () =>
        {
            if (ServerData.userInfoTable.TableDatas[UserInfoTable.getFlower].Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.Cw)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.TableDatas[UserInfoTable.getFlower].Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += score + Utils.GetDokebiTreasureAddValue();

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getFlower, ServerData.userInfoTable.TableDatas[UserInfoTable.getFlower].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Cw, ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearChunFlower, 1);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearChunFlower, 1);

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.Cw)} {score + Utils.GetDokebiTreasureAddValue()}개 획득!", null);
            });
        }, null);
    }

    public void OnClickSmithReward()
    {
        if (ServerData.userInfoTable.TableDatas[UserInfoTable.getSmith].Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SmithFire)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.smithClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score}개 획득 합니까?\n<color=red>(하루 한번만 획득 가능)</color>", () =>
        {
            if (ServerData.userInfoTable.TableDatas[UserInfoTable.getSmith].Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SmithFire)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.TableDatas[UserInfoTable.getSmith].Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value += score;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getSmith, ServerData.userInfoTable.TableDatas[UserInfoTable.getSmith].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.SmithFire, ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                LogManager.Instance.SendLogType("Smith", "_", score.ToString());
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.SmithFire)} {score}개 획득!", null);
            });
        }, null);
    }

    public void OnClickGuildGumihoReward()
    {

    }

    public void OnClickTrainingReward()
    {
        RewardPopupManager.Instance.OnclickButton();
    }

    public void OnClickDokebiReward()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getDokebiFire).Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.DokebiFire)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.DokebiFireClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score}개 획득 합니까?\n{CommonString.GetItemName(Item_Type.DokebiTreasure)}로 추가획득 : {Utils.GetDokebiTreasureAddValue()}", () =>
        {
            if (ServerData.userInfoTable.GetTableData(UserInfoTable.getDokebiFire).Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.DokebiFire)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.GetTableData(UserInfoTable.getDokebiFire).Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value += score + Utils.GetDokebiTreasureAddValue();

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getDokebiFire, ServerData.userInfoTable.TableDatas[UserInfoTable.getDokebiFire].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.DokebiFire, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearDokebiFire, 1);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearDokebiFire, 1);

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.DokebiFire)} {score + Utils.GetDokebiTreasureAddValue()}개 획득!", null);
            });
        }, null);
    }

    public void OnClickSumiReward()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getSumiFire).Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SumiFire)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        int score = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.sumiFireClear].Value;

        if (score == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{score}개 획득 합니까?\n{CommonString.GetItemName(Item_Type.DokebiTreasure)}로 추가획득 : {Utils.GetDokebiTreasureAddValue()}", () =>
        {
            if (ServerData.userInfoTable.GetTableData(UserInfoTable.getSumiFire).Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.SumiFire)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.GetTableData(UserInfoTable.getSumiFire).Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value += score + Utils.GetDokebiTreasureAddValue();

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getSumiFire, ServerData.userInfoTable.TableDatas[UserInfoTable.getSumiFire].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.SumiFire, ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.ClearSumiFire, 1);
            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearSumiFire, 1);

            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.SumiFire)} {score + Utils.GetDokebiTreasureAddValue()}개 획득!", null);
            });
        }, null);
    }

    private int GetDayOfweek()
    {
        var serverTime = ServerData.userInfoTable.currentServerTime;
        return (int)serverTime.DayOfWeek;
    }


    public void OnClickGetNewGachaButton()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getRingGoods).Value == 1)
        {
            PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.NewGachaEnergy)}은 하루에 한번만 획득 가능합니다!");
            return;
        }

        //int amount = GameBalance.getRingGoodsAmount;
        int amount = GameBalance.getRingGoodsAmount * (int)Mathf.Floor(Mathf.Max(1, (float)ServerData.userInfoTable.GetTableData(UserInfoTable.relicKillCount).Value));

        if (amount == 0)
        {
            PopupManager.Instance.ShowAlarmMessage("점수가 등록되지 않았습니다.");
            return;
        }

        PopupManager.Instance.ShowYesNoPopup(CommonString.Notice, $"{amount}개 획득 합니까?", () =>
        {
            if (ServerData.userInfoTable.GetTableData(UserInfoTable.getRingGoods).Value == 1)
            {
                PopupManager.Instance.ShowAlarmMessage($"{CommonString.GetItemName(Item_Type.NewGachaEnergy)}은 하루에 한번만 획득 가능합니다!");
                return;
            }

            ServerData.userInfoTable.GetTableData(UserInfoTable.getRingGoods).Value = 1;
            ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value += amount;

            List<TransactionValue> transactions = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.getRingGoods, ServerData.userInfoTable.TableDatas[UserInfoTable.getRingGoods].Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.NewGachaEnergy, ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value);

            transactions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            EventMissionManager.UpdateEventMissionClear(EventMissionKey.S_ClearSoulStone, 1);
            ServerData.SendTransaction(transactions, successCallBack: () =>
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"{CommonString.GetItemName(Item_Type.NewGachaEnergy)} {amount}개 획득!", null);
            });
        }, null);
    }
}
