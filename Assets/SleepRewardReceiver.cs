using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SleepRewardReceiver : SingletonMono<SleepRewardReceiver>
{
    public class SleepRewardInfo
    {
        public readonly float gold;

        public readonly float jade;

        public readonly float GrowthStone;

        public readonly float marble;

        public readonly float exp;

        public readonly float yoguiMarble;

        public readonly float eventItem;

        public readonly int elapsedSeconds;

        public readonly int killCount;

        public readonly float stageRelic;
        public readonly float sulItem;
        //눈사람,봄나물
        public readonly float springItem;
        public readonly float peachItem;
        public readonly float helItem;
        public readonly float chunItem;

        public SleepRewardInfo(float gold, float jade, float GrowthStone, float marble, float yoguiMarble, float eventItem, float exp, int elapsedSeconds, int killCount, float stageRelic, float sulItem, float springItem, float peachItem, float helItem,float chunItem)
        {
            this.gold = gold;

            this.jade = jade;

            this.GrowthStone = GrowthStone;

            this.marble = marble;

            this.yoguiMarble = yoguiMarble;

            this.eventItem = eventItem;

            this.exp = exp;

            this.elapsedSeconds = elapsedSeconds;

            this.killCount = killCount;

            this.stageRelic = stageRelic;

            this.sulItem = sulItem;

            this.springItem = springItem;

            this.peachItem = peachItem;
            
            this.helItem = helItem;
            
            this.chunItem = chunItem;
        }
    }

    public SleepRewardInfo sleepRewardInfo { get; private set; }

    public bool SetComplete = false;
    public void SetElapsedSecond(int elapsedSeconds)
    {
        if (SetComplete == true) return;

        elapsedSeconds = Mathf.Min(elapsedSeconds, GameBalance.sleepRewardMaxValue);

        SetComplete = true;

        //맨처음
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value == -1)
        {
            return;
        }

        sleepRewardInfo = null;

        //일정시간 이하는 안됨
        if (elapsedSeconds < GameBalance.sleepRewardMinValue)
        {
            return;
        }

        float elapsedMinutes = (float)elapsedSeconds / 60f;

        int currentStageIdx = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value;

        if (currentStageIdx == TableManager.Instance.GetLastStageIdx())
        {
            currentStageIdx = TableManager.Instance.GetLastStageIdx();
        }

        var stageTableData = TableManager.Instance.StageMapData[currentStageIdx];

        MapInfo mapInfo = BattleObjectManager.GetMapPrefabObject(stageTableData.Mappreset).GetComponent<MapInfo>();

        var spawnedEnemyData = TableManager.Instance.EnemyData[stageTableData.Monsterid1];

        var spawnInterval = stageTableData.Spawndelay + ((float)GameBalance.spawnIntervalTime * (float)GameBalance.spawnDivideNum);
        //
        int platformNum = mapInfo.spawnPlatforms.Count;


        int plusSpawnNum = GuildManager.Instance.GetGuildSpawnEnemyNum(GuildManager.Instance.guildLevelExp.Value);

        //지옥 추가소환
        plusSpawnNum += (int)ServerData.goodsTable.GetTableData(GoodsTable.du).Value;

        //천계 추가소환
        if (PlayerStats.IsChunMonsterSpawnAdd())
        {
            plusSpawnNum += 5;
        }
#if UNITY_EDITOR
        plusSpawnNum = 71;
#endif

            float spawnEnemyNumPerSec = (float)((platformNum * stageTableData.Spawnamountperplatform) + plusSpawnNum) / spawnInterval;

        float killedEnemyPerMin = spawnEnemyNumPerSec * 60f;

        float goldBuffRatio = PlayerStats.GetGoldPlusValueExclusiveBuff() * 1f;
        float expBuffRatio = PlayerStats.GetExpPlusValueExclusiveBuff() * 1f;

        float gold = killedEnemyPerMin * spawnedEnemyData.Gold * GameBalance.sleepRewardRatio * elapsedMinutes;
        gold += gold * goldBuffRatio;

        float enemyKilldailyMissionRequire = TableManager.Instance.DailyMissionDatas[0].Rewardrequire;
        float enemyKilldailyMissionReward = TableManager.Instance.DailyMissionDatas[0].Rewardvalue;

        float jade = killedEnemyPerMin / enemyKilldailyMissionRequire * enemyKilldailyMissionReward * GameBalance.sleepRewardRatio * elapsedMinutes * 1.8f;

        float GrowthStone = killedEnemyPerMin * (stageTableData.Magicstoneamount + PlayerStats.GetSmithValue(StatusType.growthStoneUp)) * GameBalance.sleepRewardRatio * elapsedMinutes;

        float marble = killedEnemyPerMin * stageTableData.Marbleamount * GameBalance.sleepRewardRatio * elapsedMinutes;

        float yoguimarble = killedEnemyPerMin * stageTableData.Marbleamount * GameBalance.sleepRewardRatio * elapsedMinutes;

        float eventItem = 0;

        float stageRelic = killedEnemyPerMin * stageTableData.Relicspawnamount * GameBalance.sleepRewardRatio * elapsedMinutes;

        float sulItem = killedEnemyPerMin * stageTableData.Marbleamount * GameBalance.sleepRewardRatio * elapsedMinutes;

        //눈사람, 봄나물
        float springItem = killedEnemyPerMin * stageTableData.Marbleamount * GameBalance.sleepRewardRatio * elapsedMinutes;

        //복숭아
        float peachItem = 0;
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateSon).Value >0)
        {
            peachItem = killedEnemyPerMin * stageTableData.Peachamount * GameBalance.sleepRewardRatio * elapsedMinutes;
        }
        //복숭아
        float helItem = 0;
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateHel).Value >0)
        {
            helItem = killedEnemyPerMin * stageTableData.Helamount * GameBalance.sleepRewardRatio * elapsedMinutes;
        }
        float chunItem = 0;
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateChun).Value >0)
        {
            chunItem = killedEnemyPerMin * stageTableData.Chunfloweramount * GameBalance.sleepRewardRatio * elapsedMinutes;
        }
        eventItem = springItem;

        float exp = killedEnemyPerMin * spawnedEnemyData.Exp * GameBalance.sleepRewardRatio * elapsedMinutes;
        exp += exp * expBuffRatio;

        this.sleepRewardInfo = new SleepRewardInfo(gold: gold, jade: jade, GrowthStone: GrowthStone, marble: marble,
            yoguiMarble: yoguimarble, eventItem: eventItem, exp: exp, elapsedSeconds: elapsedSeconds,
            killCount: (int)(elapsedMinutes * killedEnemyPerMin * stageTableData.Marbleamount *
                             GameBalance.sleepRewardRatio), stageRelic: stageRelic, sulItem: sulItem,
            springItem: springItem, peachItem: peachItem, helItem: helItem, chunItem: chunItem);

        UiSleepRewardView.Instance.CheckReward();
    }

    public IEnumerator GetSleepReward(Action successCallBack)
    {
        if (sleepRewardInfo == null) yield break;

        Debug.LogError($"before {ServerData.statusTable.GetTableData(StatusTable.Level).Value}");

        UiSleepRewardMask.Instance.ShowMaskObject(true);

        int elapsedSeconds = sleepRewardInfo.elapsedSeconds;

        LogManager.Instance.SendLogType("SleepReward", "Req", $"seconds {sleepRewardInfo.elapsedSeconds} gold {sleepRewardInfo.gold} jade {sleepRewardInfo.jade} marble {sleepRewardInfo.marble} growthStone {sleepRewardInfo.GrowthStone} exp {sleepRewardInfo.exp}");

        GrowthManager.Instance.GetExpBySleep(sleepRewardInfo.exp);

        ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value += sleepRewardInfo.gold;
        ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += sleepRewardInfo.jade;
        ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += sleepRewardInfo.marble;
        ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value += sleepRewardInfo.GrowthStone;
        //
        ServerData.goodsTable.GetTableData(GoodsTable.PetUpgradeSoul).Value += sleepRewardInfo.yoguiMarble;

        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateSon).Value >0)
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += (int)sleepRewardInfo.peachItem;
        }
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateHel).Value >0)
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += (int)sleepRewardInfo.helItem;
        }
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateChun).Value >0)
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += (int)sleepRewardInfo.chunItem;
        }
        //봄나물
        if (ServerData.userInfoTable.CanSpawnSpringEventItem())
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value += sleepRewardInfo.springItem;
            if (ServerData.iapServerTable.TableDatas[UiCollectionPass0BuyButton.PassKey].buyCount.Value == 0)
            {
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value += sleepRewardInfo.springItem;
            }
            else
            {
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value += sleepRewardInfo.springItem;
            }
        }
        //눈사람
        if (ServerData.userInfoTable.CanSpawnSnowManItem())
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += sleepRewardInfo.springItem;
        }
        
        ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value += sleepRewardInfo.sulItem;
        ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value += sleepRewardInfo.stageRelic;

        ServerData.userInfoTable.TableDatas[UserInfoTable.dailyEnemyKillCount].Value += sleepRewardInfo.killCount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value = 0;

        if (ServerData.userInfoTable.IsMonthlyPass2() == false)
        {
            ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotal].Value += sleepRewardInfo.killCount;
        }
        else
        {
            ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotal2].Value += sleepRewardInfo.killCount;
        }

        //ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalChild].Value += sleepRewardInfo.killCount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalWinterPass].Value += sleepRewardInfo.killCount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalSeason].Value += sleepRewardInfo.killCount;
        ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalSeason2].Value += sleepRewardInfo.killCount;
        //ServerData.userInfoTable.TableDatas[UserInfoTable.attenCountOne].Value += sleepRewardInfo.killCount;

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.Gold, ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value);
        goodsParam.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
        goodsParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
        goodsParam.Add(GoodsTable.GrowthStone, ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value);
        goodsParam.Add(GoodsTable.PetUpgradeSoul, ServerData.goodsTable.GetTableData(GoodsTable.PetUpgradeSoul).Value);



        //   goodsParam.Add(GoodsTable.Event_Item_1, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value);
        //goodsParam.Add(GoodsTable.Event_Mission, ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value);
        if (ServerData.userInfoTable.CanSpawnSpringEventItem())
        {
            goodsParam.Add(GoodsTable.Event_Collection, ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value);
            if (ServerData.iapServerTable.TableDatas[UiCollectionPass0BuyButton.PassKey].buyCount.Value == 0)
            { 
                goodsParam.Add(GoodsTable.Event_Collection_All, ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value);
            }
        }
        if(ServerData.userInfoTable.CanSpawnSnowManItem())
        {
            goodsParam.Add(GoodsTable.Event_Item_SnowMan, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value);
        }
        
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateSon).Value >0)
        {
            goodsParam.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value); 
        }

        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateHel).Value >0)
        {
            goodsParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value); 
        }
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateChun).Value >0)
        {
            goodsParam.Add(GoodsTable.Cw, ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value); 
        }

        goodsParam.Add(GoodsTable.StageRelic, ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value);
        goodsParam.Add(GoodsTable.SulItem, ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value);

        Param userInfoParam = new Param();
        userInfoParam.Add(UserInfoTable.dailyEnemyKillCount, ServerData.userInfoTable.TableDatas[UserInfoTable.dailyEnemyKillCount].Value);
        userInfoParam.Add(UserInfoTable.sleepRewardSavedTime, ServerData.userInfoTable.TableDatas[UserInfoTable.sleepRewardSavedTime].Value);

        if (ServerData.userInfoTable.IsMonthlyPass2() == false)
        {
            userInfoParam.Add(UserInfoTable.killCountTotal, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotal].Value);
        }
        else
        {
            userInfoParam.Add(UserInfoTable.killCountTotal2, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotal2].Value);
        }

        //userInfoParam.Add(UserInfoTable.killCountTotalChild, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalChild].Value);
        userInfoParam.Add(UserInfoTable.killCountTotalWinterPass, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalWinterPass].Value);
        userInfoParam.Add(UserInfoTable.killCountTotalSeason, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalSeason].Value);
        userInfoParam.Add(UserInfoTable.killCountTotalSeason2, ServerData.userInfoTable.TableDatas[UserInfoTable.killCountTotalSeason2].Value);
        // userInfoParam.Add(UserInfoTable.attenCountOne, ServerData.userInfoTable.TableDatas[UserInfoTable.attenCountOne].Value);


        yield return new WaitForSeconds(0.5f);

        List<TransactionValue> transantions = new List<TransactionValue>();

        Debug.LogError($"after {ServerData.statusTable.GetTableData(StatusTable.Level).Value}");

        //경험치
        Param statusParam = new Param();
        //레벨
        statusParam.Add(StatusTable.Level, ServerData.statusTable.GetTableData(StatusTable.Level).Value);

        //스킬포인트
        statusParam.Add(StatusTable.SkillPoint, ServerData.statusTable.GetTableData(StatusTable.SkillPoint).Value);

        //스탯포인트
        statusParam.Add(StatusTable.StatPoint, ServerData.statusTable.GetTableData(StatusTable.StatPoint).Value);

        Param growthParam = new Param();
        growthParam.Add(GrowthTable.Exp, ServerData.growthTable.GetTableData(GrowthTable.Exp).Value);
        goodsParam.Add(GoodsTable.BonusSpinKey, ServerData.goodsTable.GetTableData(GoodsTable.BonusSpinKey).Value);

        transantions.Add(TransactionValue.SetUpdate(StatusTable.tableName, StatusTable.Indate, statusParam));
        transantions.Add(TransactionValue.SetUpdate(GrowthTable.tableName, GrowthTable.Indate, growthParam));
        //
        transantions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        transantions.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

        ServerData.SendTransaction(transantions, successCallBack: () =>
          {
              successCallBack?.Invoke();
              UiSleepRewardMask.Instance.ShowMaskObject(false);
              LogManager.Instance.SendLogType("SleepReward", "Get", elapsedSeconds.ToString());
              UiExpGauge.Instance.WhenGrowthValueChanged();
              DailyMissionManager.SyncAllMissions();
             // UiTutorialManager.Instance.SetClear(TutorialStep.GetSleepReward);
          });
    }

    public float GetKilledEnemyPerMin(Item_Type type)
    {
        int currentStageIdx = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).Value;

        if (currentStageIdx == TableManager.Instance.GetLastStageIdx())
        {
            currentStageIdx = TableManager.Instance.GetLastStageIdx();
        }
        var stageTableData = TableManager.Instance.StageMapData[currentStageIdx];

        MapInfo mapInfo = BattleObjectManager.GetMapPrefabObject(stageTableData.Mappreset).GetComponent<MapInfo>();
        var spawnInterval = stageTableData.Spawndelay + ((float)GameBalance.spawnIntervalTime * (float)GameBalance.spawnDivideNum);
        int platformNum = mapInfo.spawnPlatforms.Count;
        int plusSpawnNum = GuildManager.Instance.GetGuildSpawnEnemyNum(GuildManager.Instance.guildLevelExp.Value);

        //지옥 추가소환
        plusSpawnNum += (int)ServerData.goodsTable.GetTableData(GoodsTable.du).Value;

        //천계 추가소환
        if (PlayerStats.IsChunMonsterSpawnAdd())
        {
            plusSpawnNum += 5;
        }
#if UNITY_EDITOR
        plusSpawnNum = 71;
#endif

        float spawnEnemyNumPerSec = (float)((platformNum * stageTableData.Spawnamountperplatform) + plusSpawnNum) / spawnInterval;

        float killedEnemyPerMin = spawnEnemyNumPerSec * 60f;
        
        
        switch (type)
        {
            case Item_Type.GrowthStone:
                return killedEnemyPerMin * (stageTableData.Magicstoneamount + PlayerStats.GetSmithValue(StatusType.growthStoneUp)) * GameBalance.sleepRewardRatio;
            case Item_Type.Marble:
                return killedEnemyPerMin * stageTableData.Marbleamount * GameBalance.sleepRewardRatio;
            case Item_Type.StageRelic:
                return killedEnemyPerMin * stageTableData.Relicspawnamount * GameBalance.sleepRewardRatio;
            case Item_Type.PeachReal:
                return killedEnemyPerMin * stageTableData.Peachamount * GameBalance.sleepRewardRatio;
            default:
                return 0f;
        }
    }
    private IEnumerator SyncLevelUpDataLate()
    {
        yield return new WaitForSeconds(5.0f);
    }
    public void GetRewardSuccess()
    {
        sleepRewardInfo = null;
    }
}
