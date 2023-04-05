using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;
using static UiRewardView;

public static class ServerData
{
    public static UserInfoTable userInfoTable { get; private set; } = new UserInfoTable();
    public static StatusTable statusTable { get; private set; } = new StatusTable();
    public static GrowthTable growthTable { get; private set; } = new GrowthTable();
    public static GoodsTable goodsTable { get; private set; } = new GoodsTable();
    public static WeaponTable weaponTable { get; private set; } = new WeaponTable();
    public static SkillServerTable skillServerTable { get; private set; } = new SkillServerTable();
    public static NewGachaServerTable newGachaServerTable { get; private set; } = new NewGachaServerTable();
    public static DailyMissionTable dailyMissionTable { get; private set; } = new DailyMissionTable();
    public static EventMissionTable eventMissionTable { get; private set; } = new EventMissionTable();
    public static CollectionTable collectionTable { get; private set; } = new CollectionTable();
    public static EquipmentTable equipmentTable { get; private set; } = new EquipmentTable();
    public static MagicBookTable magicBookTable { get; private set; } = new MagicBookTable();
    public static PetServerTable petTable { get; private set; } = new PetServerTable();
    public static RankTable_Level rankTables_level { get; private set; } = new RankTable_Level();
    public static RankTable_Stage rankTables_Stage { get; private set; } = new RankTable_Stage();
    public static RankTable_Boss rankTables_Boss { get; private set; } = new RankTable_Boss();
    public static RankTable_Real_Boss rankTables_Real_Boss { get; private set; } = new RankTable_Real_Boss();

    public static RankTable_Real_Boss_GangChul rankTables_Real_Boss_gangChul { get; private set; } =
        new RankTable_Real_Boss_GangChul();

    public static PassServerTable passServerTable { get; private set; } = new PassServerTable();

    public static CostumeServerTable costumeServerTable { get; private set; } = new CostumeServerTable();

    public static DailyPassServerTable dailyPassServerTable { get; private set; } = new DailyPassServerTable();
    public static IAPServerTable iapServerTable { get; private set; } = new IAPServerTable();

    public static BossServerTable bossServerTable { get; private set; } = new BossServerTable();
    public static SasinsuTable sasinsuServerTable { get; private set; } = new SasinsuTable();
    public static AttendanceServerTable attendanceServerTable { get; private set; } = new AttendanceServerTable();

    // public static FieldBossServerTable fieldBossTable { get; private set; } = new FieldBossServerTable();

    public static BuffServerTable buffServerTable { get; private set; } = new BuffServerTable();
    public static PassiveServerTable passiveServerTable { get; private set; } = new PassiveServerTable();
    public static Passive2ServerTable passive2ServerTable { get; private set; } = new Passive2ServerTable();

    public static MarbleServerTable marbleServerTable { get; private set; } = new MarbleServerTable();
    public static EtcServerTable etcServerTable { get; private set; } = new EtcServerTable();
    public static NewLevelPass newLevelPass { get; private set; } = new NewLevelPass();
    public static IAPServerTableTotal iAPServerTableTotal { get; private set; } = new IAPServerTableTotal();
    public static TitleServerTable titleServerTable { get; private set; } = new TitleServerTable();
    public static PensionServerTable pensionServerTable { get; private set; } = new PensionServerTable();
    public static YomulServerTable yomulServerTable { get; private set; } = new YomulServerTable();
    public static CostumePreset costumePreset { get; private set; } = new CostumePreset();
    public static RankTable_YoguiSogul yoguisogul_Rank { get; private set; } = new RankTable_YoguiSogul();

    public static PetEquipmentServerTable petEquipmentServerTable { get; private set; } = new PetEquipmentServerTable();
    public static MonthlyPassServerTable monthlyPassServerTable { get; private set; } = new MonthlyPassServerTable();
    public static ChildPassServerTable childPassServerTable { get; private set; } = new ChildPassServerTable();

    public static RelicServerTable relicServerTable { get; private set; } = new RelicServerTable();
    public static StageRelicServerTable stageRelicServerTable { get; private set; } = new StageRelicServerTable();

    public static MonthlyPassServerTable2 monthlyPassServerTable2 { get; private set; } = new MonthlyPassServerTable2();
    public static RankTable_MiniGame rankTable_MiniGame { get; private set; } = new RankTable_MiniGame();
    public static SeolPassServerTable seolPassServerTable { get; private set; } = new SeolPassServerTable();
    public static SulPassServerTable sulPassServerTable { get; private set; } = new SulPassServerTable();
    public static BokPassServerTable bokPassServerTable { get; private set; } = new BokPassServerTable();
    public static OneYearPassServerTable oneYearPassServerTable { get; private set; } = new OneYearPassServerTable();
    public static RankTable_ChunmaTop rankTable_ChunmaTop { get; private set; } = new RankTable_ChunmaTop();

    public static ColdSeasonPassServerTable coldSeasonPassServerTable { get; private set; } =
        new ColdSeasonPassServerTable();

    public static SuhoAnimalServerTable suhoAnimalServerTable { get; private set; } = new SuhoAnimalServerTable();

    #region string

    public static string inDate_str = "inDate";
    public static string format_string = "S";
    public static string format_Number = "N";
    public static string format_bool = "BOOL";
    public static string format_dic = "M";
    public static string format_list = "L";

    //  BOOL bool boolean 형태의 데이터가 이에 해당됩니다.
    //  N   numbers int, float, double 등 모든 숫자형 데이터는 이에 해당됩니다.
    //  S   string  string 형태의 데이터가 이에 해당됩니다.
    //  L list    list 형태의 데이터가 이에 해당됩니다.
    //  M map map, dictionary 형태의 데이터가 이에 해당됩니다.
    //  NULL    null	값이 존재하지 않는 경우 이에 해당됩니다.

    #endregion

    //트렌젝션으로 수정
    public static void LoadTables()
    {
        statusTable.Initialize();
        growthTable.Initialize();
        goodsTable.Initialize();
        weaponTable.Initialize();
        skillServerTable.Initialize();
        //dailyMissionTable.Initialize();
        eventMissionTable.Initialize();
        //collectionTable.Initialize();
        equipmentTable.Initialize();
        magicBookTable.Initialize();
        newGachaServerTable.Initialize();
        petTable.Initialize();
        userInfoTable.Initialize();
        rankTables_level.Initialize();
        rankTables_Stage.Initialize();
        rankTables_Boss.Initialize();
        rankTables_Real_Boss.Initialize();
        rankTables_Real_Boss_gangChul.Initialize();
        passServerTable.Initialize();
        costumeServerTable.Initialize();
        dailyPassServerTable.Initialize();
        iapServerTable.Initialize();
        //rankTable_InfinityTower.Initialize();
        bossServerTable.Initialize();
        sasinsuServerTable.Initialize();
        attendanceServerTable.Initialize();
        //fieldBossTable.Initialize();
        buffServerTable.Initialize();
        passiveServerTable.Initialize();
        passive2ServerTable.Initialize();
        //rankTables_Boss1.Initialize();
        marbleServerTable.Initialize();
        etcServerTable.Initialize();

        newLevelPass.Initialize();

        iAPServerTableTotal.Initialize();

        titleServerTable.Initialize();

        pensionServerTable.Initialize();

        yomulServerTable.Initialize();

        costumePreset.Initialize();

        yoguisogul_Rank.Initialize();

        petEquipmentServerTable.Initialize();

        monthlyPassServerTable.Initialize();

        childPassServerTable.Initialize();

        relicServerTable.Initialize();

        stageRelicServerTable.Initialize();

        monthlyPassServerTable2.Initialize();

        rankTable_MiniGame.Initialize();

        seolPassServerTable.Initialize();

        sulPassServerTable.Initialize();

        bokPassServerTable.Initialize();

        oneYearPassServerTable.Initialize();

        rankTable_ChunmaTop.Initialize();

        coldSeasonPassServerTable.Initialize();

        suhoAnimalServerTable.Initialize();
    }

    public static void GetUserInfo()
    {
        BackendReturnObject BRO = Backend.BMember.GetUserInfo();

        if (BRO.IsSuccess())
        {
            Debug.Log(BRO.GetReturnValue());
        }
        else
        {
            Debug.Log("서버 공통 에러 발생: " + BRO.GetErrorCode());
        }
    }

    public static void ShowCommonErrorPopup(BackendReturnObject bro, Action retryCallBack)
    {
        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice,
            $"{CommonString.DataLoadFailedRetry}\n{bro.GetStatusCode()}", retryCallBack);
    }


    public static void SendTransaction(List<TransactionValue> transactionList, bool retry = true,
        Action completeCallBack = null, Action successCallBack = null)
    {
        SendQueue.Enqueue(Backend.GameData.TransactionWrite, transactionList, (bro) =>
        {
            if (bro.IsSuccess())
            {
                successCallBack?.Invoke();
            }
            else
            {
                Debug.LogError($"SendTransaction error!!! {bro.GetMessage()}");

                if (retry)
                {
                    CoroutineExecuter.Instance.StartCoroutine(TransactionRetryRoutine(transactionList));
                }
                else
                {
                    PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, "네트워크가 불안정 합니다.\n앱을 재실행 합니다.",
                        () => { Utils.RestartApplication(); });
                }
            }

            completeCallBack?.Invoke();
        });
    }

    private static WaitForSeconds retryWs = new WaitForSeconds(3.0f);

    private static IEnumerator TransactionRetryRoutine(List<TransactionValue> transactionList)
    {
        yield return retryWs;
        SendTransaction(transactionList, retry: false);
    }

    public static void AddParam(Param param, string key, float value)
    {
        param.Add(key, value);
    }

    public static void AddParam(Param param, Item_Type key)
    {
        var stringKey = Utils.GetGoodsNameByType(key);

        if (string.IsNullOrEmpty(stringKey))
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"stringKey is null", null);
            return;
        }

        if (!ServerData.goodsTable.TableDatas.ContainsKey(stringKey))
        {
            PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"stringKey is not exist", null);
            return;
        }

        param.Add(stringKey, ServerData.goodsTable.GetTableData(stringKey).Value);
    }

    public static void AddLocalValue(Item_Type type, float rewardValue)
    {
        switch (type)
        {
            case Item_Type.Gold:
                //로컬
                ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value += rewardValue;
                break;
            case Item_Type.Jade:
                ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += rewardValue;
                break;
            case Item_Type.Marble:
                ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += rewardValue;
                break;
            case Item_Type.GrowthStone:
                ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value += rewardValue;
                break;
            case Item_Type.Memory:
                ServerData.statusTable.GetTableData(StatusTable.Memory).Value += (int)rewardValue;
                break;
            case Item_Type.Ticket:
                ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value += rewardValue;
                break;
            case Item_Type.Songpyeon:
                ServerData.goodsTable.GetTableData(GoodsTable.Songpyeon).Value += rewardValue;
                break;
            case Item_Type.Event_Item_0:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value += rewardValue;
                break;

            case Item_Type.Event_Item_1:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value += rewardValue;
                break;
            case Item_Type.Event_Item_SnowMan:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += rewardValue;
                break;

            case Item_Type.SulItem:
                ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value += rewardValue;
                break;
            case Item_Type.FeelMulStone:
                ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value += rewardValue;
                break;
            case Item_Type.SmithFire:
                ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value += rewardValue;
                break;

            case Item_Type.Asura0:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value += rewardValue;
                break;
            case Item_Type.Asura1:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value += rewardValue;
                break;
            case Item_Type.Asura2:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value += rewardValue;
                break;
            case Item_Type.Asura3:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value += rewardValue;
                break;

            case Item_Type.Asura4:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value += rewardValue;
                break;

            case Item_Type.Asura5:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value += rewardValue;
                break;
            case Item_Type.Aduk:
                ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value += rewardValue;
                break;
            //
            case Item_Type.SinSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value += rewardValue;
                break;

            case Item_Type.SinSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value += rewardValue;
                break;

            case Item_Type.SinSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value += rewardValue;
                break;
            case Item_Type.NataSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value += rewardValue;
                break;
            case Item_Type.OrochiSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.OrochiSkill).Value += rewardValue;
                break;
            //
            case Item_Type.Sun0:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun0).Value += rewardValue;
                break;
            case Item_Type.Sun1:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun1).Value += rewardValue;
                break;
            case Item_Type.Sun2:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun2).Value += rewardValue;
                break;
            case Item_Type.Sun3:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun3).Value += rewardValue;
                break;
            case Item_Type.Sun4:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun4).Value += rewardValue;
                break;
            //
            //
            case Item_Type.Chun0:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun0).Value += rewardValue;
                break;
            case Item_Type.Chun1:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun1).Value += rewardValue;
                break;
            case Item_Type.Chun2:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun2).Value += rewardValue;
                break;
            case Item_Type.Chun3:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun3).Value += rewardValue;
                break;
            case Item_Type.Chun4:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun4).Value += rewardValue;
                break;
            //
            //
            case Item_Type.DokebiSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill0).Value += rewardValue;
                break;
            case Item_Type.DokebiSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill1).Value += rewardValue;
                break;
            case Item_Type.DokebiSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill2).Value += rewardValue;
                break;
            case Item_Type.DokebiSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill3).Value += rewardValue;
                break;
            case Item_Type.DokebiSkill4:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill4).Value += rewardValue;
                break;
            //            
            case Item_Type.FourSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill0).Value += rewardValue;
                break;
            case Item_Type.FourSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill1).Value += rewardValue;
                break;
            case Item_Type.FourSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill2).Value += rewardValue;
                break;
            case Item_Type.FourSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill3).Value += rewardValue;
                break;

            //
            //            
            case Item_Type.FourSkill4:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill4).Value += rewardValue;
                break;
            case Item_Type.FourSkill5:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill5).Value += rewardValue;
                break;
            case Item_Type.FourSkill6:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill6).Value += rewardValue;
                break;
            case Item_Type.FourSkill7:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill7).Value += rewardValue;
                break;
            case Item_Type.FourSkill8:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill8).Value += rewardValue;
                break;

            //            
            case Item_Type.VisionSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill0).Value += rewardValue;
                break;
            case Item_Type.VisionSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill1).Value += rewardValue;
                break;
            case Item_Type.VisionSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill2).Value += rewardValue;
                break;
            case Item_Type.VisionSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill3).Value += rewardValue;
                break;

            //
            //
            case Item_Type.GangrimSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.GangrimSkill).Value += rewardValue;
                break;

            case Item_Type.SinSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value += rewardValue;
                break;

            case Item_Type.LeeMuGiStone:
                ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value += rewardValue;
                break;
            case Item_Type.SP:
                ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value += rewardValue;
                break;
            case Item_Type.HellPower:
                ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value += rewardValue;
                break;

            case Item_Type.DokebiTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiTreasure).Value += rewardValue;
                break;
            case Item_Type.DokebiFireEnhance:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value += rewardValue;
                break;
            case Item_Type.SusanoTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.SusanoTreasure).Value += rewardValue;
                break;
            case Item_Type.SahyungTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value += rewardValue;
                break;   
            case Item_Type.VisionTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionTreasure).Value += rewardValue;
                break;  
            case Item_Type.GuildTowerClearTicket:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerClearTicket).Value += rewardValue;
                break;  
            case Item_Type.GuildTowerHorn:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerHorn).Value += rewardValue;
                break;   
            case Item_Type.SinsuMarble:
                ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value += rewardValue;
                break;

            case Item_Type.Hel:
                ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += rewardValue;
                break;
            case Item_Type.Ym:
                ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value += rewardValue;
                break;
            case Item_Type.Fw:
                ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value += rewardValue;
                break;
            case Item_Type.Cw:
                ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += rewardValue;
                break;
            case Item_Type.FoxMaskPartial:
                ServerData.goodsTable.GetTableData(GoodsTable.FoxMaskPartial).Value += rewardValue;
                break;
            case Item_Type.DokebiFire:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value += rewardValue;
                break;
            case Item_Type.SuhoPetFeed:
                ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value += rewardValue;
                break;   
       
            case Item_Type.SuhoPetFeedClear:
                ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeedClear).Value += rewardValue;
                break;
            case Item_Type.SumiFire:
                ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value += rewardValue;
                break;
            case Item_Type.Tresure:
                ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value += rewardValue;
                break;

            case Item_Type.SinsuRelic:
                ServerData.goodsTable.GetTableData(GoodsTable.SinsuRelic).Value += rewardValue;
                break;

            case Item_Type.EventDice:
                ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value += rewardValue;
                break;

            case Item_Type.DokebiFireKey:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireKey).Value += rewardValue;
                break;

            case Item_Type.SumiFireKey:
                ServerData.goodsTable.GetTableData(GoodsTable.SumiFireKey).Value += rewardValue;
                break;
            case Item_Type.NewGachaEnergy:
                ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value += rewardValue;
                break;
            case Item_Type.DokebiBundle:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiBundle).Value += rewardValue;
                break;


            case Item_Type.Mileage:
                ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value += rewardValue;
                break;

            case Item_Type.Event_Collection:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value += rewardValue;
                break;
            case Item_Type.Event_Collection_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value += rewardValue;
                break;
            case Item_Type.Event_Fall_Gold:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Fall_Gold).Value += rewardValue;
                break;
            case Item_Type.Event_NewYear:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear).Value += rewardValue;
                break;
            case Item_Type.Event_NewYear_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear_All).Value += rewardValue;
                break;
            case Item_Type.Event_Mission:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value += rewardValue;
                break;
            case Item_Type.Event_Mission_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value += rewardValue;
                break;

            case Item_Type.du:
                ServerData.goodsTable.GetTableData(GoodsTable.du).Value += rewardValue;
                break;

            case Item_Type.Hae_Norigae:
                ServerData.goodsTable.GetTableData(GoodsTable.Hae_Norigae).Value += rewardValue;
                break;

            case Item_Type.Hae_Pet:
                ServerData.goodsTable.GetTableData(GoodsTable.Hae_Pet).Value += rewardValue;
                break;

            case Item_Type.costume1:
            case Item_Type.costume6:
            case Item_Type.costume7:
            case Item_Type.costume8:
            case Item_Type.costume9:
            case Item_Type.costume10:
            case Item_Type.costume11:
            case Item_Type.costume12:
            case Item_Type.costume13:
            case Item_Type.costume14:
            case Item_Type.costume15:
            case Item_Type.costume16:
            case Item_Type.costume17:
            case Item_Type.costume18:
            case Item_Type.costume19:
            case Item_Type.costume20:
            case Item_Type.costume21:
            case Item_Type.costume22:
            case Item_Type.costume23:
            case Item_Type.costume24:
            case Item_Type.costume25:
            case Item_Type.costume26:
            case Item_Type.costume27:
            case Item_Type.costume28:
            case Item_Type.costume29:
            case Item_Type.costume30:
            case Item_Type.costume31:
            case Item_Type.costume32:
            case Item_Type.costume33:
            case Item_Type.costume34:
            case Item_Type.costume35:
            case Item_Type.costume36:
            case Item_Type.costume37:
            case Item_Type.costume38:
            case Item_Type.costume39:
            case Item_Type.costume40:
            case Item_Type.costume41:
            case Item_Type.costume42:
            case Item_Type.costume43:
            case Item_Type.costume44:
            case Item_Type.costume45:
            case Item_Type.costume46:
            case Item_Type.costume47:
            case Item_Type.costume48:
            case Item_Type.costume49:
            case Item_Type.costume50:
            case Item_Type.costume51:
            case Item_Type.costume52:
            case Item_Type.costume53:
            case Item_Type.costume54:
            case Item_Type.costume55:
            case Item_Type.costume56:
            case Item_Type.costume57:
            case Item_Type.costume58:
            case Item_Type.costume59:
            case Item_Type.costume60:
            case Item_Type.costume61:

            case Item_Type.costume62:
            case Item_Type.costume63:
            case Item_Type.costume64:
            case Item_Type.costume65:
            case Item_Type.costume66:
            case Item_Type.costume67:
            case Item_Type.costume68:
            case Item_Type.costume69:
            case Item_Type.costume70:
            case Item_Type.costume71:
            case Item_Type.costume72:
            case Item_Type.costume73:
            case Item_Type.costume74:
            case Item_Type.costume75:
            case Item_Type.costume76:
            case Item_Type.costume77:
            case Item_Type.costume78:
            case Item_Type.costume79:
            case Item_Type.costume80:
            case Item_Type.costume81:
            case Item_Type.costume82:
            case Item_Type.costume83:
            case Item_Type.costume84:
            case Item_Type.costume85:
            case Item_Type.costume86:
            case Item_Type.costume87:
            case Item_Type.costume88:
            case Item_Type.costume89:
            case Item_Type.costume90:
            case Item_Type.costume91:
            case Item_Type.costume92:
            case Item_Type.costume93:
            case Item_Type.costume94:
            case Item_Type.costume95:
            case Item_Type.costume96:
            case Item_Type.costume97:
            case Item_Type.costume98:
            case Item_Type.costume99:
            case Item_Type.costume100:
            case Item_Type.costume101:
            case Item_Type.costume102:
            case Item_Type.costume103:
            case Item_Type.costume104:
                ServerData.costumeServerTable.TableDatas[type.ToString()].hasCostume.Value = true;
                break;
            case Item_Type.weapon81:
                ServerData.weaponTable.TableDatas[type.ToString()].hasItem.Value += (int)rewardValue;
                break;
            case Item_Type.weapon90:
                ServerData.weaponTable.TableDatas[type.ToString()].hasItem.Value += (int)rewardValue;
                break;
            case Item_Type.RelicTicket:
                ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value += rewardValue;
                break;
            case Item_Type.StageRelic:
                ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value += rewardValue;
                break;
            case Item_Type.PeachReal:
                ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += rewardValue;
                break;


            case Item_Type.GuildReward:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += rewardValue;
                break;
            default:
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"Item_Type {type} is not defined", null);
            }
                break;
        }
    }

    public static TransactionValue GetItemTypeTransactionValue(Item_Type type)
    {
        Param passParam = new Param();
        //패스 보상

        switch (type)
        {
            case Item_Type.Gold:
                passParam.Add(GoodsTable.Gold, ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
                break;
            case Item_Type.Jade:
                passParam.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
                break;
            case Item_Type.GrowthStone:
                passParam.Add(GoodsTable.GrowthStone, ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
                break;
            case Item_Type.Memory:
                passParam.Add(StatusTable.Memory, ServerData.statusTable.GetTableData(StatusTable.Memory).Value);
                return TransactionValue.SetUpdate(StatusTable.tableName, StatusTable.Indate, passParam);
                break;
            case Item_Type.Ticket:
                passParam.Add(GoodsTable.Ticket, ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Marble:
                passParam.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Dokebi:
                passParam.Add(GoodsTable.DokebiKey, ServerData.goodsTable.GetTableData(GoodsTable.DokebiKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.costume1:
            case Item_Type.costume8:
            case Item_Type.costume11:
            case Item_Type.costume12:
            case Item_Type.costume13:
            case Item_Type.costume14:
            case Item_Type.costume15:
            case Item_Type.costume16:
            case Item_Type.costume17:
            case Item_Type.costume18:
            case Item_Type.costume19:
            case Item_Type.costume20:
            case Item_Type.costume21:
            case Item_Type.costume22:
            case Item_Type.costume23:
            case Item_Type.costume24:
            case Item_Type.costume25:
            case Item_Type.costume26:
            case Item_Type.costume27:
            case Item_Type.costume28:
            case Item_Type.costume29:
            case Item_Type.costume30:
            case Item_Type.costume31:
            case Item_Type.costume32:
            case Item_Type.costume33:
            case Item_Type.costume34:
            case Item_Type.costume35:
            case Item_Type.costume36:
            case Item_Type.costume37:
            case Item_Type.costume38:
            case Item_Type.costume39:
            case Item_Type.costume40:
            case Item_Type.costume41:
            case Item_Type.costume42:
            case Item_Type.costume43:
            case Item_Type.costume44:
            case Item_Type.costume45:
            case Item_Type.costume46:
            case Item_Type.costume47:
            case Item_Type.costume48:
            case Item_Type.costume49:
            case Item_Type.costume50:
            case Item_Type.costume51:
            case Item_Type.costume52:
            case Item_Type.costume53:
            case Item_Type.costume54:
            case Item_Type.costume55:

            case Item_Type.costume56:
            case Item_Type.costume57:
            case Item_Type.costume58:
            case Item_Type.costume59:
            case Item_Type.costume60:
            case Item_Type.costume61:
            case Item_Type.costume62:
            case Item_Type.costume63:
            case Item_Type.costume64:
            case Item_Type.costume65:
            case Item_Type.costume66:
            case Item_Type.costume67:
            case Item_Type.costume68:
            case Item_Type.costume69:
            case Item_Type.costume70:
            case Item_Type.costume71:
            case Item_Type.costume72:
            case Item_Type.costume73:
            case Item_Type.costume74:
            case Item_Type.costume75:
            case Item_Type.costume76:
            case Item_Type.costume77:
            case Item_Type.costume78:
            case Item_Type.costume79:
            case Item_Type.costume80:
            case Item_Type.costume81:
            case Item_Type.costume82:
            case Item_Type.costume83:
            case Item_Type.costume84:
            case Item_Type.costume85:
            case Item_Type.costume86:
            case Item_Type.costume87:
            case Item_Type.costume88:
            case Item_Type.costume89:
            case Item_Type.costume90:
            case Item_Type.costume91:
            case Item_Type.costume92:
            case Item_Type.costume93:
            case Item_Type.costume94:
            case Item_Type.costume95:
            case Item_Type.costume96:
            case Item_Type.costume97:
            case Item_Type.costume98:
            case Item_Type.costume99:
            case Item_Type.costume100:
            case Item_Type.costume101:
            case Item_Type.costume102:
            case Item_Type.costume103:
            case Item_Type.costume104:
                string costumeKey = type.ToString();
                passParam.Add(costumeKey, ServerData.costumeServerTable.TableDatas[costumeKey].ConvertToString());
                return TransactionValue.SetUpdate(CostumeServerTable.tableName, CostumeServerTable.Indate, passParam);
                break;
            case Item_Type.Songpyeon:
                passParam.Add(GoodsTable.Songpyeon, ServerData.goodsTable.GetTableData(GoodsTable.Songpyeon).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Relic:
                passParam.Add(GoodsTable.Relic, ServerData.goodsTable.GetTableData(GoodsTable.Relic).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.RelicTicket:
                passParam.Add(GoodsTable.RelicTicket, ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Event_Item_0:
                passParam.Add(GoodsTable.Event_Item_0,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Event_Item_1:
                passParam.Add(GoodsTable.Event_Item_1,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_Item_SnowMan:
                passParam.Add(GoodsTable.Event_Item_SnowMan,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SulItem:
                passParam.Add(GoodsTable.SulItem, ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.StageRelic:
                passParam.Add(GoodsTable.StageRelic, ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.PeachReal:
                passParam.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiTreasure:
                passParam.Add(GoodsTable.DokebiTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiFireEnhance:
                passParam.Add(GoodsTable.DokebiFireEnhance,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.HellPower:
                passParam.Add(GoodsTable.HellPowerUp, ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SP:
                passParam.Add(GoodsTable.SwordPartial,
                    ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Hel:
                passParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Ym:
                passParam.Add(GoodsTable.Ym, ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Cw:
                passParam.Add(GoodsTable.Cw, ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SinsuRelic:
                passParam.Add(GoodsTable.SinsuRelic, ServerData.goodsTable.GetTableData(GoodsTable.SinsuRelic).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.EventDice:
                passParam.Add(GoodsTable.EventDice, ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Tresure:
                passParam.Add(GoodsTable.Tresure, ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SumiFire:
                passParam.Add(GoodsTable.SumiFire, ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.NewGachaEnergy:
                passParam.Add(GoodsTable.NewGachaEnergy,
                    ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiBundle:
                passParam.Add(GoodsTable.DokebiBundle,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiBundle).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SuhoPetFeed:
                passParam.Add(GoodsTable.SuhoPetFeed, ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SuhoPetFeedClear:
                passParam.Add(GoodsTable.SuhoPetFeedClear,
                    ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeedClear).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiFire:
                passParam.Add(GoodsTable.DokebiFire, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Mileage:
                passParam.Add(GoodsTable.Mileage, ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
          

            case Item_Type.SumiFireKey:
                passParam.Add(GoodsTable.SumiFireKey, ServerData.goodsTable.GetTableData(GoodsTable.SumiFireKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiFireKey:
                passParam.Add(GoodsTable.DokebiFireKey,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Event_Collection:
                passParam.Add(GoodsTable.Event_Collection,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_Collection_All:
                passParam.Add(GoodsTable.Event_Collection_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_Fall_Gold:
                passParam.Add(GoodsTable.Event_Fall_Gold,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Fall_Gold).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_NewYear:
                passParam.Add(GoodsTable.Event_NewYear,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_NewYear_All:
                passParam.Add(GoodsTable.Event_NewYear_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_Mission:
                passParam.Add(GoodsTable.Event_Mission,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Event_Mission_All:
                passParam.Add(GoodsTable.Event_Mission_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Fw:
                passParam.Add(GoodsTable.Fw, ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.du:
                passParam.Add(GoodsTable.du, ServerData.goodsTable.GetTableData(GoodsTable.du).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Hae_Norigae:
                passParam.Add(GoodsTable.Hae_Norigae, ServerData.goodsTable.GetTableData(GoodsTable.Hae_Norigae).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Hae_Pet:
                passParam.Add(GoodsTable.Hae_Pet, ServerData.goodsTable.GetTableData(GoodsTable.Hae_Pet).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.GuildReward:
                passParam.Add(GoodsTable.GuildReward, ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FeelMulStone:
                passParam.Add(GoodsTable.FeelMulStone,
                    ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.SmithFire:
                passParam.Add(GoodsTable.SmithFire, ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura0:
                passParam.Add(GoodsTable.Asura0, ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura1:
                passParam.Add(GoodsTable.Asura1, ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura2:
                passParam.Add(GoodsTable.Asura2, ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura3:
                passParam.Add(GoodsTable.Asura3, ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura4:
                passParam.Add(GoodsTable.Asura4, ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Asura5:
                passParam.Add(GoodsTable.Asura5, ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            case Item_Type.Indra0:
                passParam.Add(GoodsTable.Indra0, ServerData.goodsTable.GetTableData(GoodsTable.Indra0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Indra1:
                passParam.Add(GoodsTable.Indra1, ServerData.goodsTable.GetTableData(GoodsTable.Indra1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.Indra2:
                passParam.Add(GoodsTable.Indra2, ServerData.goodsTable.GetTableData(GoodsTable.Indra2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.IndraPower:
                passParam.Add(GoodsTable.IndraPower, ServerData.goodsTable.GetTableData(GoodsTable.IndraPower).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.OrochiTooth0:
                passParam.Add(GoodsTable.OrochiTooth0,
                    ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.OrochiTooth1:
                passParam.Add(GoodsTable.OrochiTooth1,
                    ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            //

            case Item_Type.Aduk:
                passParam.Add(GoodsTable.Aduk, ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            //
            case Item_Type.SinSkill0:
                passParam.Add(GoodsTable.SinSkill0, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.SinSkill1:
                passParam.Add(GoodsTable.SinSkill1, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.SinSkill2:
                passParam.Add(GoodsTable.SinSkill2, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.SinSkill3:
                passParam.Add(GoodsTable.SinSkill3, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.NataSkill:
                passParam.Add(GoodsTable.NataSkill, ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.GangrimSkill:
                passParam.Add(GoodsTable.GangrimSkill,
                    ServerData.goodsTable.GetTableData(GoodsTable.GangrimSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.OrochiSkill:
                passParam.Add(GoodsTable.OrochiSkill, ServerData.goodsTable.GetTableData(GoodsTable.OrochiSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            case Item_Type.Sun0:
                passParam.Add(GoodsTable.Sun0, ServerData.goodsTable.GetTableData(GoodsTable.Sun0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Sun1:
                passParam.Add(GoodsTable.Sun1, ServerData.goodsTable.GetTableData(GoodsTable.Sun1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Sun2:
                passParam.Add(GoodsTable.Sun2, ServerData.goodsTable.GetTableData(GoodsTable.Sun2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Sun3:
                passParam.Add(GoodsTable.Sun3, ServerData.goodsTable.GetTableData(GoodsTable.Sun3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Sun4:
                passParam.Add(GoodsTable.Sun4, ServerData.goodsTable.GetTableData(GoodsTable.Sun4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            //
            case Item_Type.Chun0:
                passParam.Add(GoodsTable.Chun0, ServerData.goodsTable.GetTableData(GoodsTable.Chun0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Chun1:
                passParam.Add(GoodsTable.Chun1, ServerData.goodsTable.GetTableData(GoodsTable.Chun1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Chun2:
                passParam.Add(GoodsTable.Chun2, ServerData.goodsTable.GetTableData(GoodsTable.Chun2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Chun3:
                passParam.Add(GoodsTable.Chun3, ServerData.goodsTable.GetTableData(GoodsTable.Chun3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.Chun4:
                passParam.Add(GoodsTable.Chun4, ServerData.goodsTable.GetTableData(GoodsTable.Chun4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            //
            case Item_Type.DokebiSkill0:
                passParam.Add(GoodsTable.DokebiSkill0,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiSkill1:
                passParam.Add(GoodsTable.DokebiSkill1,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiSkill2:
                passParam.Add(GoodsTable.DokebiSkill2,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiSkill3:
                passParam.Add(GoodsTable.DokebiSkill3,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.DokebiSkill4:
                passParam.Add(GoodsTable.DokebiSkill4,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //           //
            case Item_Type.FourSkill0:
                passParam.Add(GoodsTable.FourSkill0, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill1:
                passParam.Add(GoodsTable.FourSkill1, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill2:
                passParam.Add(GoodsTable.FourSkill2, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill3:
                passParam.Add(GoodsTable.FourSkill3, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            case Item_Type.FourSkill4:
                passParam.Add(GoodsTable.FourSkill4, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill5:
                passParam.Add(GoodsTable.FourSkill5, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill5).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill6:
                passParam.Add(GoodsTable.FourSkill6, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill6).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill7:
                passParam.Add(GoodsTable.FourSkill7, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill7).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.FourSkill8:
                passParam.Add(GoodsTable.FourSkill8, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill8).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            //
            //
            case Item_Type.VisionSkill0:
                passParam.Add(GoodsTable.VisionSkill0, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.VisionSkill1:
                passParam.Add(GoodsTable.VisionSkill1, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.VisionSkill2:
                passParam.Add(GoodsTable.VisionSkill2, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);

            case Item_Type.VisionSkill3:
                passParam.Add(GoodsTable.VisionSkill3, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            //
            case Item_Type.LeeMuGiStone:
                passParam.Add(GoodsTable.LeeMuGiStone,
                    ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            
            case Item_Type.SinsuMarble:
                passParam.Add(GoodsTable.SinsuMarble,
                    ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            
            case Item_Type.SahyungTreasure:
                passParam.Add(GoodsTable.SahyungTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            case Item_Type.VisionTreasure:
                passParam.Add(GoodsTable.VisionTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.VisionTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);    
            case Item_Type.GuildTowerClearTicket:
                passParam.Add(GoodsTable.GuildTowerClearTicket,
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerClearTicket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            
            case Item_Type.GuildTowerHorn:
                passParam.Add(GoodsTable.GuildTowerHorn,
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerHorn).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, passParam);
            
        }

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 트랜젝션 타입 {type}", null);

        return null;
    }

    public static TransactionValue GetItemTypeTransactionValueForAttendance(Item_Type type, float amount)
    {
        Param param = new Param();
        //패스 보상

        switch (type)
        {
            case Item_Type.Jade:
                ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += amount;
                param.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
                break;
            case Item_Type.GrowthStone:
                ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value += amount;
                param.Add(GoodsTable.GrowthStone, ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
                break;
            case Item_Type.Memory:
                ServerData.statusTable.GetTableData(StatusTable.Memory).Value += (int)amount;
                param.Add(StatusTable.Memory, ServerData.statusTable.GetTableData(StatusTable.Memory).Value);
                return TransactionValue.SetUpdate(StatusTable.tableName, StatusTable.Indate, param);
                break;
            case Item_Type.Ticket:
                ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value += amount;
                param.Add(GoodsTable.Ticket, ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Marble:
                ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += amount;
                param.Add(GoodsTable.MarbleKey, ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.WeaponUpgradeStone:
                ServerData.goodsTable.GetTableData(GoodsTable.WeaponUpgradeStone).Value += amount;
                param.Add(GoodsTable.WeaponUpgradeStone,
                    ServerData.goodsTable.GetTableData(GoodsTable.WeaponUpgradeStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.YomulExchangeStone:
                ServerData.goodsTable.GetTableData(GoodsTable.YomulExchangeStone).Value += amount;
                param.Add(GoodsTable.YomulExchangeStone,
                    ServerData.goodsTable.GetTableData(GoodsTable.YomulExchangeStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.TigerBossStone:
                ServerData.goodsTable.GetTableData(GoodsTable.TigerStone).Value += amount;
                param.Add(GoodsTable.TigerStone, ServerData.goodsTable.GetTableData(GoodsTable.TigerStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.RabitBossStone:
                ServerData.goodsTable.GetTableData(GoodsTable.RabitStone).Value += amount;
                param.Add(GoodsTable.RabitStone, ServerData.goodsTable.GetTableData(GoodsTable.RabitStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.DragonBossStone:
                ServerData.goodsTable.GetTableData(GoodsTable.DragonStone).Value += amount;
                param.Add(GoodsTable.DragonStone, ServerData.goodsTable.GetTableData(GoodsTable.DragonStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SnakeStone:
                ServerData.goodsTable.GetTableData(GoodsTable.SnakeStone).Value += amount;
                param.Add(GoodsTable.SnakeStone, ServerData.goodsTable.GetTableData(GoodsTable.SnakeStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.HorseStone:
                ServerData.goodsTable.GetTableData(GoodsTable.HorseStone).Value += amount;
                param.Add(GoodsTable.HorseStone, ServerData.goodsTable.GetTableData(GoodsTable.HorseStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SheepStone:
                ServerData.goodsTable.GetTableData(GoodsTable.SheepStone).Value += amount;
                param.Add(GoodsTable.SheepStone, ServerData.goodsTable.GetTableData(GoodsTable.SheepStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.CockStone:
                ServerData.goodsTable.GetTableData(GoodsTable.CockStone).Value += amount;
                param.Add(GoodsTable.CockStone, ServerData.goodsTable.GetTableData(GoodsTable.CockStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DogStone:
                ServerData.goodsTable.GetTableData(GoodsTable.DogStone).Value += amount;
                param.Add(GoodsTable.DogStone, ServerData.goodsTable.GetTableData(GoodsTable.DogStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.PigStone:
                ServerData.goodsTable.GetTableData(GoodsTable.PigStone).Value += amount;
                param.Add(GoodsTable.PigStone, ServerData.goodsTable.GetTableData(GoodsTable.PigStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.MonkeyStone:
                ServerData.goodsTable.GetTableData(GoodsTable.MonkeyStone).Value += amount;
                param.Add(GoodsTable.MonkeyStone, ServerData.goodsTable.GetTableData(GoodsTable.MonkeyStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.MiniGameReward:
                ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value += amount;
                param.Add(GoodsTable.MiniGameReward,
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.MiniGameReward2:
                ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value += amount;
                param.Add(GoodsTable.MiniGameReward2,
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.RelicTicket:
                ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value += amount;
                param.Add(GoodsTable.RelicTicket, ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.StageRelic:
                ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value += amount;
                param.Add(GoodsTable.StageRelic, ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Event_Item_0:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value += amount;
                param.Add(GoodsTable.Event_Item_0, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Event_Item_1:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value += amount;
                param.Add(GoodsTable.Event_Item_1, ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_Item_SnowMan:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += amount;
                param.Add(GoodsTable.Event_Item_SnowMan,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SulItem:
                ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value += amount;
                param.Add(GoodsTable.SulItem, ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.PeachReal:
                ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += amount;
                param.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DokebiTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiTreasure).Value += amount;
                param.Add(GoodsTable.DokebiTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DokebiFireEnhance:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value += amount;
                param.Add(GoodsTable.DokebiFireEnhance,
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.HellPower:
                ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value += amount;
                param.Add(GoodsTable.HellPowerUp, ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SP:
                ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value += amount;
                param.Add(GoodsTable.SwordPartial, ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Event_Collection:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value += amount;
                param.Add(GoodsTable.Event_Collection,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_Collection_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value += amount;
                param.Add(GoodsTable.Event_Collection_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_Fall_Gold:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Fall_Gold).Value += amount;
                param.Add(GoodsTable.Event_Fall_Gold,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Fall_Gold).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_NewYear:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear).Value += amount;
                param.Add(GoodsTable.Event_NewYear, ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_NewYear_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear_All).Value += amount;
                param.Add(GoodsTable.Event_NewYear_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_Mission:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value += amount;
                param.Add(GoodsTable.Event_Mission, ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Event_Mission_All:
                ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value += amount;
                param.Add(GoodsTable.Event_Mission_All,
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Hel:
                ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += amount;
                param.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.FoxMaskPartial:
                ServerData.goodsTable.GetTableData(GoodsTable.FoxMaskPartial).Value += amount;
                param.Add(GoodsTable.FoxMaskPartial,
                    ServerData.goodsTable.GetTableData(GoodsTable.FoxMaskPartial).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SusanoTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.SusanoTreasure).Value += amount;
                param.Add(GoodsTable.SusanoTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.SusanoTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param); 
            
            case Item_Type.SinsuMarble:
                ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value += amount;
                param.Add(GoodsTable.SinsuMarble,
                    ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);   
                
            case Item_Type.GuildTowerHorn:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerHorn).Value += amount;
                param.Add(GoodsTable.GuildTowerHorn,
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerHorn).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);   
            
            case Item_Type.GuildTowerClearTicket:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerClearTicket).Value += amount;
                param.Add(GoodsTable.GuildTowerClearTicket,
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerClearTicket).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SahyungTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value += amount;
                param.Add(GoodsTable.SahyungTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.VisionTreasure:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionTreasure).Value += amount;
                param.Add(GoodsTable.VisionTreasure,
                    ServerData.goodsTable.GetTableData(GoodsTable.VisionTreasure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Mileage:
                ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value += amount;
                param.Add(GoodsTable.Mileage, ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SinsuRelic:
                ServerData.goodsTable.GetTableData(GoodsTable.SinsuRelic).Value += amount;
                param.Add(GoodsTable.SinsuRelic, ServerData.goodsTable.GetTableData(GoodsTable.SinsuRelic).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.EventDice:
                ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value += amount;
                param.Add(GoodsTable.EventDice, ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Tresure:
                ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value += amount;
                param.Add(GoodsTable.Tresure, ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SumiFire:
                ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value += amount;
                param.Add(GoodsTable.SumiFire, ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.NewGachaEnergy:
                ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value += amount;
                param.Add(GoodsTable.NewGachaEnergy,
                    ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DokebiBundle:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiBundle).Value += amount;
                param.Add(GoodsTable.DokebiBundle, ServerData.goodsTable.GetTableData(GoodsTable.DokebiBundle).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Cw:
                ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += amount;
                param.Add(GoodsTable.Cw, ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SumiFireKey:
                ServerData.goodsTable.GetTableData(GoodsTable.SumiFireKey).Value += amount;
                param.Add(GoodsTable.SumiFireKey, ServerData.goodsTable.GetTableData(GoodsTable.SumiFireKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SuhoPetFeedClear:
                ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeedClear).Value += amount;
                param.Add(GoodsTable.SuhoPetFeedClear,
                    ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeedClear).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SuhoPetFeed:
                ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value += amount;
                param.Add(GoodsTable.SuhoPetFeed, ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DokebiFire:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value += amount;
                param.Add(GoodsTable.DokebiFire, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.DokebiFireKey:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireKey).Value += amount;
                param.Add(GoodsTable.DokebiFireKey, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireKey).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Fw:
                ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value += amount;
                param.Add(GoodsTable.Fw, ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Ym:
                ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value += amount;
                param.Add(GoodsTable.Ym, ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.du:
                ServerData.goodsTable.GetTableData(GoodsTable.du).Value += amount;
                param.Add(GoodsTable.du, ServerData.goodsTable.GetTableData(GoodsTable.du).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            case Item_Type.Hae_Norigae:
                ServerData.goodsTable.GetTableData(GoodsTable.Hae_Norigae).Value += amount;
                param.Add(GoodsTable.Hae_Norigae, ServerData.goodsTable.GetTableData(GoodsTable.Hae_Norigae).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Hae_Pet:
                ServerData.goodsTable.GetTableData(GoodsTable.Hae_Pet).Value += amount;
                param.Add(GoodsTable.Hae_Pet, ServerData.goodsTable.GetTableData(GoodsTable.Hae_Pet).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            case Item_Type.Indra0:
                ServerData.goodsTable.GetTableData(GoodsTable.Indra0).Value += amount;
                param.Add(GoodsTable.Indra0, ServerData.goodsTable.GetTableData(GoodsTable.Indra0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Indra1:
                ServerData.goodsTable.GetTableData(GoodsTable.Indra1).Value += amount;
                param.Add(GoodsTable.Indra1, ServerData.goodsTable.GetTableData(GoodsTable.Indra1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Indra2:
                ServerData.goodsTable.GetTableData(GoodsTable.Indra2).Value += amount;
                param.Add(GoodsTable.Indra2, ServerData.goodsTable.GetTableData(GoodsTable.Indra2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.OrochiTooth0:
                ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth0).Value += amount;
                param.Add(GoodsTable.OrochiTooth0, ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.OrochiTooth1:
                ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth1).Value += amount;
                param.Add(GoodsTable.OrochiTooth1, ServerData.goodsTable.GetTableData(GoodsTable.OrochiTooth1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.IndraPower:
                ServerData.goodsTable.GetTableData(GoodsTable.IndraPower).Value += amount;
                param.Add(GoodsTable.IndraPower, ServerData.goodsTable.GetTableData(GoodsTable.IndraPower).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            //

            case Item_Type.GuildReward:
                ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += amount;
                param.Add(GoodsTable.GuildReward, ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.FeelMulStone:
                ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value += amount;
                param.Add(GoodsTable.FeelMulStone, ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SmithFire:
                ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value += amount;
                param.Add(GoodsTable.SmithFire, ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura0:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value += amount;
                param.Add(GoodsTable.Asura0, ServerData.goodsTable.GetTableData(GoodsTable.Asura0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura1:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value += amount;
                param.Add(GoodsTable.Asura1, ServerData.goodsTable.GetTableData(GoodsTable.Asura1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura2:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value += amount;
                param.Add(GoodsTable.Asura2, ServerData.goodsTable.GetTableData(GoodsTable.Asura2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura3:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value += amount;
                param.Add(GoodsTable.Asura3, ServerData.goodsTable.GetTableData(GoodsTable.Asura3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura4:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value += amount;
                param.Add(GoodsTable.Asura4, ServerData.goodsTable.GetTableData(GoodsTable.Asura4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.Asura5:
                ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value += amount;
                param.Add(GoodsTable.Asura5, ServerData.goodsTable.GetTableData(GoodsTable.Asura5).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Aduk:
                ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value += amount;
                param.Add(GoodsTable.Aduk, ServerData.goodsTable.GetTableData(GoodsTable.Aduk).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            case Item_Type.Sun0:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun0).Value += amount;
                param.Add(GoodsTable.Sun0, ServerData.goodsTable.GetTableData(GoodsTable.Sun0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Sun1:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun1).Value += amount;
                param.Add(GoodsTable.Sun1, ServerData.goodsTable.GetTableData(GoodsTable.Sun1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Sun2:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun2).Value += amount;
                param.Add(GoodsTable.Sun2, ServerData.goodsTable.GetTableData(GoodsTable.Sun2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Sun3:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun3).Value += amount;
                param.Add(GoodsTable.Sun3, ServerData.goodsTable.GetTableData(GoodsTable.Sun3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Sun4:
                ServerData.goodsTable.GetTableData(GoodsTable.Sun4).Value += amount;
                param.Add(GoodsTable.Sun4, ServerData.goodsTable.GetTableData(GoodsTable.Sun4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            //
            case Item_Type.Chun0:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun0).Value += amount;
                param.Add(GoodsTable.Chun0, ServerData.goodsTable.GetTableData(GoodsTable.Chun0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Chun1:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun1).Value += amount;
                param.Add(GoodsTable.Chun1, ServerData.goodsTable.GetTableData(GoodsTable.Chun1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Chun2:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun2).Value += amount;
                param.Add(GoodsTable.Chun2, ServerData.goodsTable.GetTableData(GoodsTable.Chun2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Chun3:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun3).Value += amount;
                param.Add(GoodsTable.Chun3, ServerData.goodsTable.GetTableData(GoodsTable.Chun3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.Chun4:
                ServerData.goodsTable.GetTableData(GoodsTable.Chun4).Value += amount;
                param.Add(GoodsTable.Chun4, ServerData.goodsTable.GetTableData(GoodsTable.Chun4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            //
            case Item_Type.DokebiSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill0).Value += amount;
                param.Add(GoodsTable.DokebiSkill0, ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.DokebiSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill1).Value += amount;
                param.Add(GoodsTable.DokebiSkill1, ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.DokebiSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill2).Value += amount;
                param.Add(GoodsTable.DokebiSkill2, ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.DokebiSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill3).Value += amount;
                param.Add(GoodsTable.DokebiSkill3, ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.DokebiSkill4:
                ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill4).Value += amount;
                param.Add(GoodsTable.DokebiSkill4, ServerData.goodsTable.GetTableData(GoodsTable.DokebiSkill4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //            //
            case Item_Type.FourSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill0).Value += amount;
                param.Add(GoodsTable.FourSkill0, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill1).Value += amount;
                param.Add(GoodsTable.FourSkill1, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill2).Value += amount;
                param.Add(GoodsTable.FourSkill2, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill3).Value += amount;
                param.Add(GoodsTable.FourSkill3, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //   //
            case Item_Type.FourSkill4:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill4).Value += amount;
                param.Add(GoodsTable.FourSkill4, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill4).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill5:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill5).Value += amount;
                param.Add(GoodsTable.FourSkill5, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill5).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill6:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill6).Value += amount;
                param.Add(GoodsTable.FourSkill6, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill6).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill7:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill7).Value += amount;
                param.Add(GoodsTable.FourSkill7, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill7).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.FourSkill8:
                ServerData.goodsTable.GetTableData(GoodsTable.FourSkill8).Value += amount;
                param.Add(GoodsTable.FourSkill8, ServerData.goodsTable.GetTableData(GoodsTable.FourSkill8).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //
            //            //
            case Item_Type.VisionSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill0).Value += amount;
                param.Add(GoodsTable.VisionSkill0, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.VisionSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill1).Value += amount;
                param.Add(GoodsTable.VisionSkill1, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.VisionSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill2).Value += amount;
                param.Add(GoodsTable.VisionSkill2, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.VisionSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill3).Value += amount;
                param.Add(GoodsTable.VisionSkill3, ServerData.goodsTable.GetTableData(GoodsTable.VisionSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            //   //
            case Item_Type.SinSkill0:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value += amount;
                param.Add(GoodsTable.SinSkill0, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill0).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SinSkill1:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value += amount;
                param.Add(GoodsTable.SinSkill1, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill1).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SinSkill2:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value += amount;
                param.Add(GoodsTable.SinSkill2, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill2).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.SinSkill3:
                ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value += amount;
                param.Add(GoodsTable.SinSkill3, ServerData.goodsTable.GetTableData(GoodsTable.SinSkill3).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.NataSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value += amount;
                param.Add(GoodsTable.NataSkill, ServerData.goodsTable.GetTableData(GoodsTable.NataSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            case Item_Type.OrochiSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.OrochiSkill).Value += amount;
                param.Add(GoodsTable.OrochiSkill, ServerData.goodsTable.GetTableData(GoodsTable.OrochiSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.GangrimSkill:
                ServerData.goodsTable.GetTableData(GoodsTable.GangrimSkill).Value += amount;
                param.Add(GoodsTable.GangrimSkill, ServerData.goodsTable.GetTableData(GoodsTable.GangrimSkill).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);

            case Item_Type.LeeMuGiStone:
                ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value += amount;
                param.Add(GoodsTable.LeeMuGiStone, ServerData.goodsTable.GetTableData(GoodsTable.LeeMuGiStone).Value);
                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);


            case Item_Type.costume1:
            case Item_Type.costume4:
            case Item_Type.costume12:
            {
                string costumeKey = type.ToString();
                ServerData.costumeServerTable.TableDatas[costumeKey].hasCostume.Value = true;
                param.Add(costumeKey, ServerData.costumeServerTable.TableDatas[costumeKey].ConvertToString());
                return TransactionValue.SetUpdate(CostumeServerTable.tableName, CostumeServerTable.Indate, param);
            }
                break;
            case Item_Type.weapon0:
            case Item_Type.weapon1:
            case Item_Type.weapon2:
            case Item_Type.weapon3:
            case Item_Type.weapon4:
            case Item_Type.weapon5:
            case Item_Type.weapon6:
            case Item_Type.weapon7:
            case Item_Type.weapon8:
            case Item_Type.weapon9:
            case Item_Type.weapon10:
            case Item_Type.weapon11:
            case Item_Type.weapon12:
            case Item_Type.weapon13:
            case Item_Type.weapon14:
            case Item_Type.weapon15:
            case Item_Type.weapon16:
            case Item_Type.weapon37:
            case Item_Type.weapon38:
            case Item_Type.weapon39:
            case Item_Type.weapon40:
            case Item_Type.weapon41:
            case Item_Type.weapon42:
            {
                string key = type.ToString();
                ServerData.weaponTable.TableDatas[key].hasItem.Value = 1;
                ServerData.weaponTable.TableDatas[key].amount.Value += (int)amount;

                param.Add(key, ServerData.weaponTable.TableDatas[key].ConvertToString());

                return TransactionValue.SetUpdate(WeaponTable.tableName, WeaponTable.Indate, param);
            }
                break;
            case Item_Type.magicBook0:
            case Item_Type.magicBook1:
            case Item_Type.magicBook2:
            case Item_Type.magicBook3:
            case Item_Type.magicBook4:
            case Item_Type.magicBook5:
            case Item_Type.magicBook6:
            case Item_Type.magicBook7:
            case Item_Type.magicBook8:
            case Item_Type.magicBook9:
            case Item_Type.magicBook10:
            case Item_Type.magicBook11:
            {
                string key = type.ToString();
                ServerData.magicBookTable.TableDatas[key].hasItem.Value = 1;
                ServerData.magicBookTable.TableDatas[key].amount.Value += (int)amount;

                param.Add(key, ServerData.magicBookTable.TableDatas[key].ConvertToString());

                return TransactionValue.SetUpdate(MagicBookTable.tableName, MagicBookTable.Indate, param);
            }
                break;


            case Item_Type.gumiho0:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho0).Value += amount;
                param.Add(GoodsTable.gumiho0, ServerData.goodsTable.GetTableData(GoodsTable.gumiho0).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;

            //
            case Item_Type.gumiho1:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho1).Value += amount;
                param.Add(GoodsTable.gumiho1, ServerData.goodsTable.GetTableData(GoodsTable.gumiho1).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho2:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho2).Value += amount;
                param.Add(GoodsTable.gumiho2, ServerData.goodsTable.GetTableData(GoodsTable.gumiho2).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho3:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho3).Value += amount;
                param.Add(GoodsTable.gumiho3, ServerData.goodsTable.GetTableData(GoodsTable.gumiho3).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho4:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho4).Value += amount;
                param.Add(GoodsTable.gumiho4, ServerData.goodsTable.GetTableData(GoodsTable.gumiho4).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho5:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho5).Value += amount;
                param.Add(GoodsTable.gumiho5, ServerData.goodsTable.GetTableData(GoodsTable.gumiho5).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho6:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho6).Value += amount;
                param.Add(GoodsTable.gumiho6, ServerData.goodsTable.GetTableData(GoodsTable.gumiho6).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho7:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho7).Value += amount;
                param.Add(GoodsTable.gumiho7, ServerData.goodsTable.GetTableData(GoodsTable.gumiho7).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.gumiho8:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.gumiho8).Value += amount;
                param.Add(GoodsTable.gumiho8, ServerData.goodsTable.GetTableData(GoodsTable.gumiho8).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            //
            case Item_Type.h0:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h0).Value += amount;
                param.Add(GoodsTable.h0, ServerData.goodsTable.GetTableData(GoodsTable.h0).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h1:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h1).Value += amount;
                param.Add(GoodsTable.h1, ServerData.goodsTable.GetTableData(GoodsTable.h1).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h2:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h2).Value += amount;
                param.Add(GoodsTable.h2, ServerData.goodsTable.GetTableData(GoodsTable.h2).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h3:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h3).Value += amount;
                param.Add(GoodsTable.h3, ServerData.goodsTable.GetTableData(GoodsTable.h3).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h4:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h4).Value += amount;
                param.Add(GoodsTable.h4, ServerData.goodsTable.GetTableData(GoodsTable.h4).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h5:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h5).Value += amount;
                param.Add(GoodsTable.h5, ServerData.goodsTable.GetTableData(GoodsTable.h5).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h6:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h6).Value += amount;
                param.Add(GoodsTable.h6, ServerData.goodsTable.GetTableData(GoodsTable.h6).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h7:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h7).Value += amount;
                param.Add(GoodsTable.h7, ServerData.goodsTable.GetTableData(GoodsTable.h7).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h8:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h8).Value += amount;
                param.Add(GoodsTable.h8, ServerData.goodsTable.GetTableData(GoodsTable.h8).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.h9:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.h9).Value += amount;
                param.Add(GoodsTable.h9, ServerData.goodsTable.GetTableData(GoodsTable.h9).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            //
            case Item_Type.c0:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c0).Value += amount;
                param.Add(GoodsTable.c0, ServerData.goodsTable.GetTableData(GoodsTable.c0).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c1:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c1).Value += amount;
                param.Add(GoodsTable.c1, ServerData.goodsTable.GetTableData(GoodsTable.c1).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c2:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c2).Value += amount;
                param.Add(GoodsTable.c2, ServerData.goodsTable.GetTableData(GoodsTable.c2).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c3:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c3).Value += amount;
                param.Add(GoodsTable.c3, ServerData.goodsTable.GetTableData(GoodsTable.c3).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c4:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c4).Value += amount;
                param.Add(GoodsTable.c4, ServerData.goodsTable.GetTableData(GoodsTable.c4).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c5:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c5).Value += amount;
                param.Add(GoodsTable.c5, ServerData.goodsTable.GetTableData(GoodsTable.c5).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
            case Item_Type.c6:
            {
                ServerData.goodsTable.GetTableData(GoodsTable.c6).Value += amount;
                param.Add(GoodsTable.c6, ServerData.goodsTable.GetTableData(GoodsTable.c6).Value);

                return TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, param);
            }
                break;
        }

        PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"등록되지 않은 트랜젝션 타입 {type}", null);

        return null;
    }

    //addValue에는 반드시 goods나 status는 들어가면 안됨. 업적같은것만.
    public static void SendTransaction(List<RewardData> rewardList, TransactionValue addValue = null,
        Action successCallBack = null, Action completeCallBack = null)
    {
        List<TransactionValue> transactionList = new List<TransactionValue>();

        if (addValue != null)
        {
            transactionList.Add(addValue);
        }

        Param goodsParam = new Param();
        Param statusParam = new Param();

        for (int i = 0; i < rewardList.Count; i++)
        {
            switch ((Item_Type)rewardList[i].itemType)
            {
                case Item_Type.Gold:
                    if (goodsParam.ContainsKey(GoodsTable.Gold) == false)
                        goodsParam.Add(GoodsTable.Gold, ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value);
                    break;
                case Item_Type.Jade:
                    if (goodsParam.ContainsKey(GoodsTable.Jade) == false)
                        goodsParam.Add(GoodsTable.Jade, ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value);
                    break;
                case Item_Type.GrowthStone:
                    if (goodsParam.ContainsKey(GoodsTable.GrowthStone) == false)
                        goodsParam.Add(GoodsTable.GrowthStone,
                            ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value);
                    break;
                case Item_Type.Memory:
                    if (statusParam.ContainsKey(StatusTable.Memory) == false)
                        statusParam.Add(StatusTable.Memory,
                            ServerData.statusTable.GetTableData(StatusTable.Memory).Value);
                    break;
                case Item_Type.Ticket:
                    if (goodsParam.ContainsKey(GoodsTable.Ticket) == false)
                        goodsParam.Add(GoodsTable.Ticket, ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value);
                    break;
            }
        }

        if (goodsParam.Count != 0)
        {
            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        }

        if (statusParam.Count != 0)
        {
            transactionList.Add(TransactionValue.SetUpdate(StatusTable.tableName, StatusTable.Indate, statusParam));
        }

        SendTransaction(transactionList, completeCallBack: completeCallBack, successCallBack: successCallBack);
    }

    public static void GetPostItem(Item_Type type, float amount)
    {
        if (type.IsRankFrameItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_1;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 8;
                    break;
                case Item_Type.RankFrame2:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_2;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 7;
                    break;
                case Item_Type.RankFrame3:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_3;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 6;
                    break;
                case Item_Type.RankFrame4:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_4;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 5;
                    break;
                case Item_Type.RankFrame5:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_5;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 4;
                    break;
                case Item_Type.RankFrame6_20:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_6_20;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 3;
                    break;
                case Item_Type.RankFrame21_100:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_21_100;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 2;
                    break;
                case Item_Type.RankFrame101_1000:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_101_1000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 1;
                    break;
                case Item_Type.RankFrame1001_10000:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += GameBalance.rankRewardTicket_1001_10000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 9;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.chatFrame,
                ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);

            transactionList.Add(
                TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //LogManager.Instance.SendLogType("RankReward", type.ToString(), "");
            });
        }
        else if (type.IsPartyRaidRankFrameItem())
        {
            switch (type)
            {
                case Item_Type.PartyRaidRankFrame1:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_1;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 8;
                    break;
                case Item_Type.PartyRaidRankFrame2:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_2;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 7;
                    break;
                case Item_Type.PartyRaidRankFrame3:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_3;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 6;
                    break;
                case Item_Type.PartyRaidRankFrame4:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_4;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 5;
                    break;
                case Item_Type.PartyRaidRankFrame5:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_5;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 4;
                    break;
                case Item_Type.PartyRaidRankFrame6_20:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_6_20;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 3;
                    break;
                case Item_Type.PartyRaidRankFrame21_100:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_21_100;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 2;
                    break;
                case Item_Type.PartyRaidRankFrame101_1000:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_101_1000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 1;
                    break;
                case Item_Type.PartyRaidRankFrame1001_10000:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value +=
                        GameBalance.partyRaidRankRewardTicket_1001_10000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 9;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.chatFrame,
                ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.DokebiFire, ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value);

            transactionList.Add(
                TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //LogManager.Instance.SendLogType("RankReward", type.ToString(), "");
            });
        }
        else if (type.IsMergePartyRaidRankFrameItem())
        {
            switch (type)
            {
                case Item_Type.MergePartyRaidRankFrame1:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_1;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 8;
                    break;
                case Item_Type.MergePartyRaidRankFrame2:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_2;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 7;
                    break;
                case Item_Type.MergePartyRaidRankFrame3:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_3;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 6;
                    break;
                case Item_Type.MergePartyRaidRankFrame4:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_4;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 5;
                    break;
                case Item_Type.MergePartyRaidRankFrame5:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_5;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 4;
                    break;
                case Item_Type.MergePartyRaidRankFrame6_10:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_6_10;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 6;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 3;
                    break;
                case Item_Type.MergePartyRaidRankFrame11_20:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_11_20;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 5;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 3;
                    break;
                case Item_Type.MergePartyRaidRankFrame21_50:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_21_50;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 4;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 2;
                    break;
                case Item_Type.MergePartyRaidRankFrame51_100:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_51_100;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 3;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 2;
                    break;
                case Item_Type.MergePartyRaidRankFrame101_500:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_101_500;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 2;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 1;
                    break;
                case Item_Type.MergePartyRaidRankFrame501_1000:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_501_1000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 1;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 1;
                    break;
                case Item_Type.MergePartyRaidRankFrame1001_5000:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value +=
                        GameBalance.murgePartyRaidRankRewardTicket_1001_5000;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 0;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value = 9;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.hellMark,
                ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value);
            userInfoParam.Add(UserInfoTable.chatFrame,
                ServerData.userInfoTable.GetTableData(UserInfoTable.chatFrame).Value);

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.SumiFire, ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value);


            transactionList.Add(
                TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));
            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //LogManager.Instance.SendLogType("RankReward", type.ToString(), "");
            });
        }
        else if (type.IsRelicRewardItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_1_relic;
                    break;
                case Item_Type.RankFrame2_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_2_relic;
                    break;
                case Item_Type.RankFrame3_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_3_relic;
                    break;
                case Item_Type.RankFrame4_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_4_relic;
                    break;
                case Item_Type.RankFrame5_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_5_relic;
                    break;
                case Item_Type.RankFrame6_20_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_6_20_relic;
                    break;
                case Item_Type.RankFrame21_100_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_21_100_relic;
                    break;
                case Item_Type.RankFrame101_1000_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_101_1000_relic;
                    break;
                case Item_Type.RankFrame1001_10000_relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value +=
                        GameBalance.rankRewardTicket_1001_10000_relic;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.RelicTicket, ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //  LogManager.Instance.SendLogType("RelicReward", type.ToString(), "");
            });
        }
        else if (type.IsRelicRewardItem_hell())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_1_relic_hell;
                    break;
                case Item_Type.RankFrame2_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_2_relic_hell;
                    break;
                case Item_Type.RankFrame3_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_3_relic_hell;
                    break;
                case Item_Type.RankFrame4_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_4_relic_hell;
                    break;
                case Item_Type.RankFrame5_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_5_relic_hell;
                    break;
                case Item_Type.RankFrame6_20_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_6_20_relic_hell;
                    break;
                case Item_Type.RankFrame21_100_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_21_100_relic_hell;
                    break;
                case Item_Type.RankFrame101_1000_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_101_1000_relic_hell;
                    break;
                case Item_Type.RankFrame1001_10000_relic_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_1001_10000_relic_hell;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //  LogManager.Instance.SendLogType("RelicReward", type.ToString(), "");
            });
        }
        //
        else if (type.IsHellWarItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_2_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_1_2_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 7;
                    break;
                case Item_Type.RankFrame3_5_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_3_5_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 6;
                    break;
                case Item_Type.RankFrame6_20_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_6_20_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 5;
                    break;
                case Item_Type.RankFrame21_50_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_21_50_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 4;
                    break;
                case Item_Type.RankFrame51_100_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_51_100_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 3;
                    break;
                case Item_Type.RankFrame101_1000_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_101_1000_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 2;
                    break;
                case Item_Type.RankFrame1001_10000_war_hell:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value +=
                        GameBalance.rankRewardTicket_1001_10000_war_hell;
                    ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value = 1;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Hel, ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value);

            Param userInfoParam = new Param();
            userInfoParam.Add(UserInfoTable.hellMark,
                ServerData.userInfoTable.GetTableData(UserInfoTable.hellMark).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
            transactionList.Add(
                TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, userInfoParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //  LogManager.Instance.SendLogType("RelicReward", type.ToString(), "");
            });
        }
        else if (type.IsMiniGameRewardItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_miniGame:
                case Item_Type.RankFrame2_miniGame:
                case Item_Type.RankFrame3_miniGame:
                case Item_Type.RankFrame4_miniGame:
                case Item_Type.RankFrame5_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value +=
                        GameBalance.rankReward_1_MiniGame;
                    break;

                case Item_Type.RankFrame6_20_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value +=
                        GameBalance.rankReward_6_20_MiniGame;
                    break;
                case Item_Type.RankFrame21_100_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value +=
                        GameBalance.rankReward_21_100_MiniGame;
                    break;
                case Item_Type.RankFrame101_1000_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value +=
                        GameBalance.rankReward_101_1000_MiniGame;
                    break;
                case Item_Type.RankFrame1001_10000_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value +=
                        GameBalance.rankReward_1001_10000_MiniGame;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.MiniGameReward,
                ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                // LogManager.Instance.SendLogType("MiniReward", type.ToString(), "");
            });
        }
        else if (type.IsMiniGameRewardItem2())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_new_miniGame:
                case Item_Type.RankFrame2_new_miniGame:
                case Item_Type.RankFrame3_new_miniGame:
                case Item_Type.RankFrame4_new_miniGame:
                case Item_Type.RankFrame5_new_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value +=
                        GameBalance.rankReward_new_1_MiniGame;
                    break;

                case Item_Type.RankFrame6_20_new_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value +=
                        GameBalance.rankReward_new_6_20_MiniGame;
                    break;
                case Item_Type.RankFrame21_100_new_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value +=
                        GameBalance.rankReward_new_21_100_MiniGame;
                    break;
                case Item_Type.RankFrame101_1000_new_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value +=
                        GameBalance.rankReward_new_101_1000_MiniGame;
                    break;
                case Item_Type.RankFrame1001_10000_new_miniGame:
                    ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value +=
                        GameBalance.rankReward_new_1001_10000_MiniGame;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.MiniGameReward2,
                ServerData.goodsTable.GetTableData(GoodsTable.MiniGameReward2).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                // LogManager.Instance.SendLogType("MiniReward", type.ToString(), "");
            });
        }
        else if (type.IsGuildRewardItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += GameBalance.rankReward_1_guild;
                    break;
                case Item_Type.RankFrame2_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += GameBalance.rankReward_2_guild;
                    break;
                case Item_Type.RankFrame3_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += GameBalance.rankReward_3_guild;
                    break;
                case Item_Type.RankFrame4_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += GameBalance.rankReward_4_guild;
                    break;
                case Item_Type.RankFrame5_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += GameBalance.rankReward_5_guild;
                    break;
                case Item_Type.RankFrame6_20_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_6_20_guild;
                    break;
                case Item_Type.RankFrame21_100_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_21_50_guild;
                    break;
                case Item_Type.RankFrame101_1000_guild:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_51_100_guild;
                    break;

                //

                case Item_Type.RankFrame1guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_1_guild_new;
                    break;
                case Item_Type.RankFrame2guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_2_guild_new;
                    break;
                case Item_Type.RankFrame3guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_3_guild_new;
                    break;
                case Item_Type.RankFrame4guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_4_guild_new;
                    break;
                case Item_Type.RankFrame5guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_5_guild_new;
                    break;
                case Item_Type.RankFrame6_20_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_6_20_guild_new;
                    break;
                case Item_Type.RankFrame21_50_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_21_50_guild_new;
                    break;
                case Item_Type.RankFrame51_100_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankReward_51_100_guild_new;
                    break;

                //
                case Item_Type.RankFrameParty1guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_1_guild_new;
                    break;
                case Item_Type.RankFrameParty2guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_2_guild_new;
                    break;
                case Item_Type.RankFrameParty3guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_3_guild_new;
                    break;
                case Item_Type.RankFrameParty4guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_4_guild_new;
                    break;
                case Item_Type.RankFrameParty5guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_5_guild_new;
                    break;
                case Item_Type.RankFrameParty6_20_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_6_20_guild_new;
                    break;
                case Item_Type.RankFrameParty21_50_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_21_50_guild_new;
                    break;
                case Item_Type.RankFrameParty51_100_guild_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value +=
                        GameBalance.rankRewardParty_51_100_guild_new;
                    break;
            }


            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.GuildReward, ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //LogManager.Instance.SendLogType("GuildReward", type.ToString(), "");
            });
        }
        else if (type.IsGangChulItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_1_new_boss;
                    break;
                case Item_Type.RankFrame2_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_2_new_boss;
                    break;
                case Item_Type.RankFrame3_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_3_new_boss;
                    break;
                case Item_Type.RankFrame4_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_4_new_boss;
                    break;
                case Item_Type.RankFrame5_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_5_new_boss;
                    break;
                case Item_Type.RankFrame6_10_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_6_10_new_boss;
                    break;
                case Item_Type.RankFrame10_30_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_10_30_new_boss;
                    break;
                case Item_Type.RankFrame30_50boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_30_50_new_boss;
                    break;
                case Item_Type.RankFrame50_70_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += GameBalance.rankReward_50_70_new_boss;
                    break;
                case Item_Type.RankFrame70_100_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value +=
                        GameBalance.rankReward_70_100_new_boss;
                    break;
                case Item_Type.RankFrame100_200_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value +=
                        GameBalance.rankReward_100_200_new_boss;
                    break;
                case Item_Type.RankFrame200_500_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value +=
                        GameBalance.rankReward_200_500_new_boss;
                    break;
                case Item_Type.RankFrame500_1000_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value +=
                        GameBalance.rankReward_500_1000_new_boss;
                    break;
                case Item_Type.RankFrame1000_3000_boss_new:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value +=
                        GameBalance.rankReward_1000_3000_new_boss;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Peach, ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //  LogManager.Instance.SendLogType("RelicReward", type.ToString(), "");
            });
        }
        else if (type.IsRealGangChulItem())
        {
            switch (type)
            {
                case Item_Type.RankFrame1_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_1_new_boss;
                    break;
                case Item_Type.RankFrame2_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_2_new_boss;
                    break;
                case Item_Type.RankFrame3_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_3_new_boss;
                    break;
                case Item_Type.RankFrame4_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_4_new_boss;
                    break;
                case Item_Type.RankFrame5_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_5_new_boss;
                    break;
                case Item_Type.RankFrame6_10_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_6_10_new_boss;
                    break;
                case Item_Type.RankFrame10_30_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_10_30_new_boss;
                    break;
                case Item_Type.RankFrame30_50_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_30_50_new_boss;
                    break;
                case Item_Type.RankFrame50_70_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_50_70_new_boss;
                    break;
                case Item_Type.RankFrame70_100_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_70_100_new_boss;
                    break;
                case Item_Type.RankFrame100_200_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_100_200_new_boss;
                    break;
                case Item_Type.RankFrame200_500_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_200_500_new_boss;
                    break;
                case Item_Type.RankFrame500_1000_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += GameBalance.rankReward_500_1000_new_boss;
                    break;
                case Item_Type.RankFrame1000_3000_boss_GangChul:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value +=
                        GameBalance.rankReward_1000_3000_new_boss;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            Param goodsParam = new Param();
            goodsParam.Add(GoodsTable.Cw, ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value);

            transactionList.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));

            SendTransaction(transactionList, successCallBack: () =>
            {
                //  LogManager.Instance.SendLogType("RelicReward", type.ToString(), "");
            });
        }
        else
        {
            switch (type)
            {
                case Item_Type.Gold:
                    ServerData.goodsTable.GetTableData(GoodsTable.Gold).Value += amount;
                    break;
                case Item_Type.Jade:
                    ServerData.goodsTable.GetTableData(GoodsTable.Jade).Value += amount;
                    break;
                case Item_Type.GrowthStone:
                    ServerData.goodsTable.GetTableData(GoodsTable.GrowthStone).Value += amount;
                    break;
                case Item_Type.Ticket:
                    ServerData.goodsTable.GetTableData(GoodsTable.Ticket).Value += amount;
                    break;
                case Item_Type.Marble:
                    ServerData.goodsTable.GetTableData(GoodsTable.MarbleKey).Value += amount;
                    break;
                case Item_Type.Dokebi:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiKey).Value += amount;
                    break;
                case Item_Type.WeaponUpgradeStone:
                    ServerData.goodsTable.GetTableData(GoodsTable.WeaponUpgradeStone).Value += amount;
                    break;
                case Item_Type.Songpyeon:
                    ServerData.goodsTable.GetTableData(GoodsTable.Songpyeon).Value += amount;
                    break;

                case Item_Type.Event_Collection:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection).Value += amount;
                    break;

                case Item_Type.SinsuRelic:
                    ServerData.goodsTable.GetTableData(GoodsTable.SinsuRelic).Value += amount;
                    break;
                case Item_Type.EventDice:
                    ServerData.goodsTable.GetTableData(GoodsTable.EventDice).Value += amount;
                    break;
                case Item_Type.Event_Collection_All:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Collection_All).Value += amount;
                    break;
                case Item_Type.Tresure:
                    ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value += amount;
                    break;


                case Item_Type.Relic:
                    ServerData.goodsTable.GetTableData(GoodsTable.Relic).Value += amount;
                    break;
                case Item_Type.RelicTicket:
                    ServerData.goodsTable.GetTableData(GoodsTable.RelicTicket).Value += amount;
                    break;
                case Item_Type.Event_Item_0:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_0).Value += amount;
                    break;

                case Item_Type.SumiFire:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value += amount;
                    break;

                case Item_Type.NewGachaEnergy:
                    ServerData.goodsTable.GetTableData(GoodsTable.NewGachaEnergy).Value += amount;
                    break;

                case Item_Type.DokebiBundle:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiBundle).Value += amount;
                    break;     
                case Item_Type.SuhoPetFeedClear:
                    ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeedClear).Value += amount;
                    break;  
                
                case Item_Type.GuildTowerHorn:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerHorn).Value += amount;
                    break;

                case Item_Type.FoxMaskPartial:
                    ServerData.goodsTable.GetTableData(GoodsTable.FoxMaskPartial).Value += amount;
                    break;

                case Item_Type.SuhoPetFeed:
                    ServerData.goodsTable.GetTableData(GoodsTable.SuhoPetFeed).Value += amount;
                    break;

                case Item_Type.SumiFireKey:
                    ServerData.goodsTable.GetTableData(GoodsTable.SumiFireKey).Value += amount;
                    break;


                case Item_Type.Mileage:
                    ServerData.goodsTable.GetTableData(GoodsTable.Mileage).Value += amount;
                    break;

                case Item_Type.Cw:
                    ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value += amount;
                    break; 
                case Item_Type.GuildTowerClearTicket:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildTowerClearTicket).Value += amount;
                    break;
                case Item_Type.DokebiFire:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value += amount;
                    break;   
                case Item_Type.SinsuMarble:
                    ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value += amount;
                    break;
                case Item_Type.DokebiFireKey:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireKey).Value += amount;
                    break;

                case Item_Type.DokebiTreasure:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiTreasure).Value += amount;
                    break;
                case Item_Type.SahyungTreasure:
                    ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value += amount;
                    break;
                case Item_Type.VisionTreasure:
                    ServerData.goodsTable.GetTableData(GoodsTable.VisionTreasure).Value += amount;
                    break;
                case Item_Type.DokebiFireEnhance:
                    ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value += amount;
                    break;
                case Item_Type.SusanoTreasure:
                    ServerData.goodsTable.GetTableData(GoodsTable.SusanoTreasure).Value += amount;
                    break;

                case Item_Type.Event_Item_1:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_1).Value += amount;
                    break;
                case Item_Type.Event_Item_SnowMan:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += amount;
                    break;
                case Item_Type.HellPower:
                    ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value += amount;
                    break;

                case Item_Type.SulItem:
                    ServerData.goodsTable.GetTableData(GoodsTable.SulItem).Value += amount;
                    break;

                case Item_Type.Fw:
                    ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value += amount;
                    break;

                case Item_Type.SP:
                    ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value += amount;
                    break;
                case Item_Type.Hel:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value += amount;
                    break;
                case Item_Type.Ym:
                    ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value += amount;
                    break;
                case Item_Type.du:
                    ServerData.goodsTable.GetTableData(GoodsTable.du).Value += amount;
                    break;

                //
                case Item_Type.Hae_Norigae:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hae_Norigae).Value += amount;
                    break;

                case Item_Type.Hae_Pet:
                    ServerData.goodsTable.GetTableData(GoodsTable.Hae_Pet).Value += amount;
                    break;
                //
                case Item_Type.StageRelic:
                    ServerData.goodsTable.GetTableData(GoodsTable.StageRelic).Value += amount;
                    break;
                case Item_Type.PeachReal:
                    ServerData.goodsTable.GetTableData(GoodsTable.Peach).Value += amount;
                    break;

                case Item_Type.FeelMulStone:
                    ServerData.goodsTable.GetTableData(GoodsTable.FeelMulStone).Value += amount;
                    break;

                case Item_Type.SmithFire:
                    ServerData.goodsTable.GetTableData(GoodsTable.SmithFire).Value += amount;
                    break;

                case Item_Type.Event_NewYear:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear).Value += amount;
                    break;

                case Item_Type.Event_NewYear_All:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_NewYear_All).Value += amount;
                    break;

                case Item_Type.Event_Mission:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission).Value += amount;
                    break;

                case Item_Type.Event_Mission_All:
                    ServerData.goodsTable.GetTableData(GoodsTable.Event_Mission_All).Value += amount;
                    break;

                case Item_Type.GuildReward:
                    ServerData.goodsTable.GetTableData(GoodsTable.GuildReward).Value += amount;
                    break;
            }

            List<TransactionValue> transactionList = new List<TransactionValue>();

            var tramsaction = GetItemTypeTransactionValue(type);
            transactionList.Add(tramsaction);

            SendTransaction(transactionList, successCallBack: () =>
            {
                //LogManager.Instance.SendLogType("Post", type.ToString(), $"{amount}");
            });
        }
    }
}