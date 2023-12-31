using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance
{
    public readonly static ObscuredFloat moveSpeed = 8f;
    public readonly static ObscuredFloat jumpPower = 17f;
    public readonly static ObscuredFloat doubleJumpPower = 25f;
}

public class PotionBalance
{ 
    public readonly static List<ObscuredFloat> recover_Potion = new List<ObscuredFloat>() { 0.2f, 0.5f, 1.0f };
    public readonly static List<ObscuredFloat> price_Potion = new List<ObscuredFloat>() { 1, 1, 1 };

    public readonly static ObscuredFloat potionUseDelay = 0.9f;
}


public static class GameBalance
{
    //레벨업시 얻는 스탯포인트
    public readonly static ObscuredInt StatPoint = 3;
    //레벨업시 얻는 스킬포인트
    public readonly static ObscuredInt SkillPointGet = 1;
    public readonly static ObscuredInt SkillPointResetPrice = 100000;

    //시작골드
    public readonly static ObscuredInt StartingMoney = 1000;

    //스킬 각성당 올릴수있는 스킬갯수
    public readonly static ObscuredInt SkillAwakePlusNum = 10;

    public readonly static ObscuredFloat initHp = 1000f;
    public readonly static ObscuredFloat initMp = 100f;

    public static ObscuredInt costumeMaxGrade = 5;

    public readonly static ObscuredInt levelUpSpinGet = 3;

    public readonly static ObscuredFloat potionUseDelay = 0.9f;

    public readonly static ObscuredFloat ticketPrice = 500f;
    public readonly static ObscuredInt contentsEnterprice = 0;
    public readonly static ObscuredInt dailyTickBuyCountMax = 5;
    public readonly static ObscuredInt bonusDungeonEnterCount = 10;
    public readonly static ObscuredInt smithEnterCount = 5;

    public readonly static ObscuredInt dokebiEnterCount = 10;

    public readonly static List<ObscuredFloat> potion_Option = new List<ObscuredFloat>() { 0.3f, 0.6f, 0.9f };

    public readonly static ObscuredInt bonusDungeonUnlockLevel = 30;
    public readonly static ObscuredInt InfinityDungeonUnlockLevel = 60;
    public readonly static ObscuredInt bossUnlockLevel = 100;

    public readonly static ObscuredInt fieldBossSpawnRequire = 30000;

    public readonly static ObscuredFloat fieldBossHpValue = 15f;

    public readonly static ObscuredInt bonusDungeonGemPerEnemy = 2000;
    public readonly static ObscuredInt bonusDungeonMarblePerEnemy = 200;
    public readonly static ObscuredInt effectActiveDistance = 15;
    public readonly static ObscuredInt firstSkillAwakeNum = 1;

    public readonly static ObscuredInt spawnIntervalTime = 1;
    public readonly static ObscuredInt spawnDivideNum = 2;

    //1시간
    // public readonly static ObscuredInt sleepRewardMinValue = 3600;
    public readonly static ObscuredInt sleepRewardMinValue = 600;
    //10시간
    public readonly static ObscuredInt sleepRewardMaxValue = 86400;
    public readonly static ObscuredInt oneDayConvertMin = 1440;
    public readonly static ObscuredFloat sleepRewardRatio = 1f;

    public readonly static ObscuredFloat marbleSpawnProb = 1;

    public readonly static ObscuredInt marbleAwakePrice = 8000000;

    public readonly static ObscuredInt skillSlotGroupNum = 3;

    public readonly static ObscuredInt marbleUnlockLevel = 400;

    public readonly static ObscuredInt skillCraftUnlockGachaLevel = 10;

    public readonly static ObscuredInt twelveDungeonEnterPrice = 50000;

    public readonly static ObscuredInt nickNameChangeFee = 500000;


    public readonly static ObscuredInt rankRewardTicket_1 = 1500;
    public readonly static ObscuredInt rankRewardTicket_2 = 1450;
    public readonly static ObscuredInt rankRewardTicket_3 = 1400;
    public readonly static ObscuredInt rankRewardTicket_4 = 1350;
    public readonly static ObscuredInt rankRewardTicket_5 = 1300;
    public readonly static ObscuredInt rankRewardTicket_6_20 = 1250;
    public readonly static ObscuredInt rankRewardTicket_21_100 = 1200;
    public readonly static ObscuredInt rankRewardTicket_101_1000 = 1100;
    public readonly static ObscuredInt rankRewardTicket_1001_10000 = 1000;

    public readonly static ObscuredInt partyRaidRankRewardTicket_1 = 6000;
    public readonly static ObscuredInt partyRaidRankRewardTicket_2 = 5500;
    public readonly static ObscuredInt partyRaidRankRewardTicket_3 = 5000;
    public readonly static ObscuredInt partyRaidRankRewardTicket_4 = 4000;
    public readonly static ObscuredInt partyRaidRankRewardTicket_5 = 3500;
    public readonly static ObscuredInt partyRaidRankRewardTicket_6_20 = 3000;
    public readonly static ObscuredInt partyRaidRankRewardTicket_21_100 = 2500;
    public readonly static ObscuredInt partyRaidRankRewardTicket_101_1000 = 1500;
    public readonly static ObscuredInt partyRaidRankRewardTicket_1001_10000 = 1000;
    
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_1 = 15000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_2 = 14000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_3 = 13000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_4 = 12000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_5 = 11000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_6_10 = 10000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_11_20 = 8000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_21_50 = 6000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_51_100 = 4000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_101_500 = 3000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_501_1000 = 2000;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_1001_5000 = 1000;
    
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_1 = 30;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_2 = 28;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_3 = 26;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_4 = 24;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_5 = 22;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_6_10 = 20;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_11_20 = 18;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_21_50 = 16;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_51_100 = 14;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_101_500 = 12;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_501_1000 = 10;
    public readonly static ObscuredInt murgePartyRaidRankRewardTicket_0_1001_5000 = 8;

    public readonly static ObscuredInt rankRewardTicket_1_relic = 120;
    public readonly static ObscuredInt rankRewardTicket_2_relic = 110;
    public readonly static ObscuredInt rankRewardTicket_3_relic = 100;
    public readonly static ObscuredInt rankRewardTicket_4_relic = 90;
    public readonly static ObscuredInt rankRewardTicket_5_relic = 80;
    public readonly static ObscuredInt rankRewardTicket_6_20_relic = 70;
    public readonly static ObscuredInt rankRewardTicket_21_100_relic = 60;
    public readonly static ObscuredInt rankRewardTicket_101_1000_relic = 50;
    public readonly static ObscuredInt rankRewardTicket_1001_10000_relic = 40;

    public readonly static ObscuredInt rankRewardTicket_1_relic_hell = 1200;
    public readonly static ObscuredInt rankRewardTicket_2_relic_hell = 1150;
    public readonly static ObscuredInt rankRewardTicket_3_relic_hell = 1100;
    public readonly static ObscuredInt rankRewardTicket_4_relic_hell = 1050;
    public readonly static ObscuredInt rankRewardTicket_5_relic_hell = 1000;
    public readonly static ObscuredInt rankRewardTicket_6_20_relic_hell = 900;
    public readonly static ObscuredInt rankRewardTicket_21_100_relic_hell = 800;
    public readonly static ObscuredInt rankRewardTicket_101_1000_relic_hell = 700;
    public readonly static ObscuredInt rankRewardTicket_1001_10000_relic_hell = 600;

    public readonly static ObscuredInt rankRewardTicket_1_2_war_hell = 600;
    public readonly static ObscuredInt rankRewardTicket_3_5_war_hell = 550;
    public readonly static ObscuredInt rankRewardTicket_6_20_war_hell = 500;
    public readonly static ObscuredInt rankRewardTicket_21_50_war_hell = 450;
    public readonly static ObscuredInt rankRewardTicket_51_100_war_hell = 400;
    public readonly static ObscuredInt rankRewardTicket_101_1000_war_hell = 350;
    public readonly static ObscuredInt rankRewardTicket_1001_10000_war_hell = 300;


    public readonly static ObscuredInt rankReward_1_MiniGame = 20;
    public readonly static ObscuredInt rankReward_6_20_MiniGame = 15;
    public readonly static ObscuredInt rankReward_21_100_MiniGame = 14;
    public readonly static ObscuredInt rankReward_101_1000_MiniGame = 13;
    public readonly static ObscuredInt rankReward_1001_10000_MiniGame = 12;

    public readonly static ObscuredInt rankReward_new_1_MiniGame = 20;
    public readonly static ObscuredInt rankReward_new_6_20_MiniGame = 15;
    public readonly static ObscuredInt rankReward_new_21_100_MiniGame = 14;
    public readonly static ObscuredInt rankReward_new_101_1000_MiniGame = 13;
    public readonly static ObscuredInt rankReward_new_1001_10000_MiniGame = 12;

    //구미호
    public readonly static ObscuredInt rankReward_1_guild = 40000;
    public readonly static ObscuredInt rankReward_2_guild = 38000;
    public readonly static ObscuredInt rankReward_3_guild = 36000;
    public readonly static ObscuredInt rankReward_4_guild = 34000;
    public readonly static ObscuredInt rankReward_5_guild = 32000;
    public readonly static ObscuredInt rankReward_6_20_guild = 30000;
    public readonly static ObscuredInt rankReward_21_50_guild = 28000;
    public readonly static ObscuredInt rankReward_51_100_guild = 26000;

    public readonly static ObscuredInt rankReward_1_guild_new = 400;
    public readonly static ObscuredInt rankReward_2_guild_new = 350;
    public readonly static ObscuredInt rankReward_3_guild_new = 300;
    public readonly static ObscuredInt rankReward_4_guild_new = 250;
    public readonly static ObscuredInt rankReward_5_guild_new = 200;
    public readonly static ObscuredInt rankReward_6_20_guild_new = 150;
    public readonly static ObscuredInt rankReward_21_50_guild_new = 100;
    public readonly static ObscuredInt rankReward_51_100_guild_new = 50;

    //대산군
    public readonly static ObscuredInt rankRewardParty_1_guild_new = 40000;
    public readonly static ObscuredInt rankRewardParty_2_guild_new = 38000;
    public readonly static ObscuredInt rankRewardParty_3_guild_new = 36000;
    public readonly static ObscuredInt rankRewardParty_4_guild_new = 34000;
    public readonly static ObscuredInt rankRewardParty_5_guild_new = 32000;
    public readonly static ObscuredInt rankRewardParty_6_20_guild_new = 30000;
    public readonly static ObscuredInt rankRewardParty_21_50_guild_new = 28000;
    public readonly static ObscuredInt rankRewardParty_51_100_guild_new = 26000;

    public readonly static ObscuredInt rankReward_1_new_boss = 3000;
    public readonly static ObscuredInt rankReward_2_new_boss = 2800;
    public readonly static ObscuredInt rankReward_3_new_boss = 2600;
    public readonly static ObscuredInt rankReward_4_new_boss = 2400;
    public readonly static ObscuredInt rankReward_5_new_boss = 2200;
    public readonly static ObscuredInt rankReward_6_10_new_boss = 2000;
    public readonly static ObscuredInt rankReward_10_30_new_boss = 1800;
    public readonly static ObscuredInt rankReward_30_50_new_boss = 1600;
    public readonly static ObscuredInt rankReward_50_70_new_boss = 1400;
    public readonly static ObscuredInt rankReward_70_100_new_boss = 1200;
    public readonly static ObscuredInt rankReward_100_200_new_boss = 1000;
    public readonly static ObscuredInt rankReward_200_500_new_boss = 800;
    public readonly static ObscuredInt rankReward_500_1000_new_boss = 600;
    public readonly static ObscuredInt rankReward_1000_3000_new_boss = 400;



    public readonly static ObscuredInt EventDropEndDay = 28;
    public readonly static ObscuredInt EventMakeEndDay = 28;
    public readonly static ObscuredInt EventPackageSaleEndDay = 28;

    public readonly static ObscuredFloat TitleEquipAddPer = 2;

    public readonly static ObscuredFloat HotTime_Start = 20;
    public readonly static ObscuredFloat HotTime_Start_Weekend = 20;
    public readonly static ObscuredFloat HotTime_End = 22;

    public readonly static ObscuredFloat HotTime_Exp = 25;
    public readonly static ObscuredFloat HotTime_Gold = 25;
    public readonly static ObscuredFloat HotTime_GrowthStone = 45;
    public readonly static ObscuredFloat HotTime_Marble = 6;

    public readonly static ObscuredFloat HotTime_Exp_Weekend = 30;
    public readonly static ObscuredFloat HotTime_Gold_Weekend = 30;
    public readonly static ObscuredFloat HotTime_GrowthStone_Weekend = 60;
    public readonly static ObscuredFloat HotTime_Marble_Weekend = 8;


    public readonly static ObscuredInt DailyRelicTicketGetCount = 3;
    public readonly static ObscuredInt DailyEventDiceGetCount = 1;

    public readonly static ObscuredInt DokebiKeyUseCount = 5;
    public readonly static ObscuredInt StageRelicUnlockLevel = 3000;
    public readonly static ObscuredFloat StageRelicUpgradePrice = 1000;

    public readonly static ObscuredFloat BossScoreSmallizeValue = 0.00000000000000000000000000000001f;
    public readonly static ObscuredFloat BossScoreConvertToOrigin = 100000000000000000000000000000000f;

    public readonly static ObscuredInt SonEvolutionDivdeNum = 3000;

    public readonly static ObscuredInt MaxDamTextNum = 120;

    public readonly static ObscuredInt YachaRequireLevel = 5300;

    public readonly static ObscuredFloat YachaSkillAddValuePerLevel = 0.08f;
    public readonly static ObscuredFloat YachaIgnoreDefenseAddValuePerLevel = 0.08f;
    public readonly static ObscuredFloat YachaChunSlashAddValuePerLevel = 0.0005f;

    public readonly static ObscuredInt SonCostumeUnlockLevel = 80000;
    public readonly static ObscuredInt YoungMulCreateEquipLevel = 120;
    public readonly static ObscuredInt YoungMulCreateEquipLevel2 = 3000;
    public readonly static ObscuredInt YoungMulCreateEquipLevel2_Smith = 300000;
    public readonly static ObscuredFloat PetAwakeValuePerLevel = 0.003f;
    public readonly static ObscuredFloat AwakePetUpgradePrice = 100000000;

    public readonly static ObscuredFloat GuildMakePrice = 100000000;
    public readonly static ObscuredInt GuildMemberMax = 20;

    public readonly static ObscuredFloat gumgiAttackValue200 = 10000000000;

    public readonly static ObscuredInt GuildCreateMinLevel = 5000;
    public readonly static ObscuredInt GuildEnterMinLevel = 1000;

    public readonly static ObscuredInt LeeMuGiGetLevel = 2000;
    public readonly static ObscuredInt GoldGetLevel = 30;

    public readonly static ObscuredInt fireExchangeMaxCount = 10;

    public readonly static ObscuredInt bandiPlusStageJadeValue = 2;
    public readonly static ObscuredInt bandiPlusStageMarbleValue = 2;
    public readonly static ObscuredInt bandiPlusStageDevideValue = 1000;

    public readonly static ObscuredInt gumgiDefenseValue200 = 4000;

    public static ObscuredFloat forestValue = 1f;
    public static ObscuredFloat dokebiImmuneTime = 1f;
    public static ObscuredInt recommendCountPerWeek = 5;
    public static ObscuredInt recommendCountPerWeek_PartyTower = 4;

    public static ObscuredInt sanGoonDogFeedCount = 3;
    public static ObscuredInt dokebiTreasureAddValue = 1;

    public static ObscuredFloat fastSleepRewardTimeValue = 3600;
    public static ObscuredInt fastSleepRewardMaxCount = 5;

    public static ObscuredFloat smithTreeAddValue = 100;
    public static ObscuredInt sonCloneAddValue = 10000;

    public static ObscuredInt getRingGoodsAmount = 1;

    public static ObscuredInt banditUpgradeLevel = 1000000;
    public static ObscuredInt costumeCollectionUnlockNum = 20;

    public static ObscuredInt passive2UnlockLevel = 1500000;
    public static ObscuredInt passive2PointDivideNum = 10000;

    public static ObscuredInt shadowCostumeGetLevel = 12;
   // public static ObscuredDouble sonGraduateScore = 100;
    public static ObscuredDouble sonGraduateScore = 1E+88;
    public static ObscuredDouble helGraduateScore = 1E+91;
    public static ObscuredDouble flowerGraduateScore = 2200;
    public static ObscuredDouble dokebiFireGraduateScore = 3500; //졸업하는수치
    public static ObscuredDouble dokebiFireFixedScore = 4000; //졸업시 고정되는 수치
    public static ObscuredFloat dokebiGraduatePlusValue = 1.5f; 
    
     
    
    public static ObscuredDouble GumSoulGraduateScore = 9000; // 졸업하는 수치
    public static ObscuredDouble GumSoulFixedScore = 11000; //  졸업시 고정되는 수치.
    public static ObscuredFloat GumSoulGraduatePlusValue = 2f; 
    
    //노리개 영혼
    public static ObscuredDouble NorigaeSoulGraduateScore = 1E+101; // 졸업하는 수치
    public static ObscuredDouble NorigaeSoulFixedScore = 1E+101; //  졸업시 고정되는 수치.
    public static ObscuredFloat NorigaeSoulGraduatePlusValue = 1.25f; 
    //악의 씨앗
    public static ObscuredDouble EvilSeedGraduateScore = 1E+98; // 졸업하는 수치
    public static ObscuredFloat  EvilSeedGraduatePlusValue = 3f; 
    //귀신나무
    public static ObscuredDouble GhostTreeGraduateScore = 1E+96; // 졸업하는 수치
    public static ObscuredFloat  GhostTreeGraduatePlusValue = 1.5f; 
    
    public static ObscuredInt DailyPetFeedClearGetValue = 2;
    public static ObscuredInt DolPassDiceRefundValue = 20;

    public static ObscuredFloat GraduateSoulRing = 50000;
    public static ObscuredFloat GraduateSoulRingGetInterval = 10000; // 졸업 후 n개의 수치마다 획득
    public static ObscuredInt GraduateSoulRingGetIndex = 19; // 졸업후 얻을 반지의 인덱스 12 기준 특급4
    public static ObscuredFloat JadeExchangeValuePerBooty = 1; // 전리품 1개당 획득하는 옥 갯수
    public static ObscuredFloat VisionTreasurePerDamage = 1; // 비전보물 1개당 스킬 데미지 증가량
    
    //public static ObscuredFloat GuildTowerTicketMaxCount = 3; // 비전보물 1개당 스킬 데미지 증가량
    public static ObscuredFloat GuildTowerTicketDailyGetAmount = 1; // 
    public static ObscuredFloat FoxTowerTicketDailyGetAmount = 2; // 
    public static ObscuredFloat SealSwordTicketDailyGetAmount = 4; // 
    
    public static ObscuredFloat GuildTowerChimAbilUpValue = 0.01f; // 
    
    public static ObscuredFloat dokebiExpPlusValue = 4000f;
    
    public static ObscuredInt suhoAnimalAwakeLevel  = 6;

    //핫타임 이벤트
    public readonly static ObscuredFloat HotTimeEvent_Exp = 10;
    public readonly static ObscuredFloat HotTimeEvent_Gold = 10;
    public readonly static ObscuredFloat HotTimeEvent_GrowthStone = 7;
    public readonly static ObscuredFloat HotTimeEvent_Marble = 3;
    
    public readonly static ObscuredFloat HotTimeEvent_Ad_Exp = 20;
    public readonly static ObscuredFloat HotTimeEvent_Ad_Gold = 20;
    public readonly static ObscuredFloat HotTimeEvent_Ad_GrowthStone = 15;
    public readonly static ObscuredFloat HotTimeEvent_Ad_Marble = 5;
    //
    public readonly static ObscuredBool isOddMonthlyPass = true; // 월간훈련 홀수월, 짝수월
    
    public readonly static ObscuredInt fireFlyRequire = 160;
    public readonly static ObscuredInt fireFlyFixedScore = 173;
    
    public readonly static ObscuredInt BlackWolfRingDevideIdx = 40000;
    
    public readonly static ObscuredFloat HpLevel_Gold = 5000f;
    public readonly static ObscuredFloat HpPer_StatPoint = 0.005f;
    public readonly static ObscuredFloat Stat_Sin_Slash = 0.00001f;
    public readonly static ObscuredFloat Stat_Hyung_Slash =  0.000001f;
    //
    public static List<float> warMarkAbils = new List<float>() { 0f, 400f, 500f, 600f, 700f, 800f, 900f, 1000f };
    
    //왕의시련
    public static ObscuredDouble yumGraduateScore = 1E+82;
    public static ObscuredDouble okGraduateScore = 1E+86;
    public static ObscuredDouble doGraduateScore = 1E+92;
    public static ObscuredDouble sumiGraduateScore = 1E+101;
    public static ObscuredDouble thiefGraduateScore = 1E+109;
    public static ObscuredDouble darkGraduateScore = 1E+119;
    
    public static ObscuredFloat yumGraduateValue = 2;
    public static ObscuredFloat okGraduateValue = 2;
    public static ObscuredFloat doGraduateValue = 2;
    public static ObscuredFloat sumiGraduateValue = 2;
    public static ObscuredFloat thiefGraduateValue = 2;
    public static ObscuredFloat darkGraduateValue = 2;
    
        
    public static ObscuredInt yumKingGraduate = 1;
    public static ObscuredInt okKingGraduate = 2;
    public static ObscuredInt doKingGraduate = 3;
    public static ObscuredInt sumiKingGraduate = 4;
    public static ObscuredInt thiefKingGraduate = 5;
    public static ObscuredInt darkKingGraduate = 6;
    // 신의 시련

    public static ObscuredInt swordGodGraduate = 1;
    public static ObscuredInt monkeyGodGraduate = 2;
    public static ObscuredInt hellGodGraduate = 3;
    public static ObscuredInt chunGodGraduate = 4;
    public static ObscuredInt doGodGraduate = 5;
    public static ObscuredInt sumiGodGraduate = 6;
    
    public static ObscuredFloat swordGodGraduateValue = 1.5f;
    public static ObscuredFloat monkeyGodGraduateValue = 1.3f;
    public static ObscuredFloat hellGodGraduateValue = 2f;
    public static ObscuredFloat doGodGraduateValue = 2f;
    public static ObscuredFloat sumiGodGraduateValue = 2f;
        
    public static ObscuredDouble swordGodGraduateScore = 2E+122;
    public static ObscuredDouble monkeyGodGraduateScore = 5E+122;
    public static ObscuredDouble hellGodGraduateScore = 1E+119;
    public static ObscuredDouble chunGodGraduateScore = 1E+119;
    public static ObscuredDouble doGodGraduateScore = 1E+119;
    public static ObscuredDouble sumiGodGraduateScore = 1E+119;
    //
    
    public static ObscuredDouble refundGoldBarRatio = 1E+34; //보유금화환불단위
    public static ObscuredInt refundCriDamGoldBarRatio = 4000;//크리티컬데미지환불 
    public static ObscuredInt criticalGraduateValue = 7500;//각성시 크리티컬레벨
    public static ObscuredInt attackGraduateValue = 50000;//각성시 공격력
    public static ObscuredInt criticalGraduateRefundStandard = 6950;//각성시 크리티컬 환불기준레벨
    public static ObscuredInt goldGraduateScore = 65950;// 금화능력치총합레벨조건
    
    public static ObscuredFloat Gum_memory = 0.0000010f;
    public static ObscuredFloat Special0_GoldBar = 0.0000000004f;
    public static ObscuredFloat Special1_GoldBar = 0.0000000015f;
    public static ObscuredFloat Special2_GoldBar = 0.0000000038f;
    
    public static ObscuredInt JumpStageStartValue = 1;//시작 가능 스테이지
    public static ObscuredInt JumpStageAdjustValue = 10;//강철이 데미지 스테이지 계산(스테이지보스체력x2<강철이체력)의 인덱스 -50짜리 보스
    public static ObscuredInt JumpStageLimit = 50;//최종스테이지 -JumpStageLimit이 점핑한계 ex)100일 경우  18000스테이지시가 최종일 때 17900~ 점핑불가능
    public static ObscuredInt JumpPoint = 1;//점프 단위

    public static ObscuredInt RelicDungeonGraduateScore = 80000;

    public static ObscuredInt TwelveBoss_155_RequireTower10 = 2;
    public static ObscuredInt TwelveBoss_156_RequireTower10 = 4;
    public static ObscuredInt TwelveBoss_157_RequireTower10 = 6;

    
    public static ObscuredInt addDay = 0;
    
    public static ObscuredInt dosulUnlockStage = 300;
    public static ObscuredInt dailyDosulClearTicketGetValue = 2;

    public static ObscuredFloat batterySafeCount = 100f; // 절전모드

    //사흉구슬
    public static ObscuredFloat sahyungUpgradeValue = 0.03f;
    //사신수구슬
    public static ObscuredFloat sinsuUpgradeValue = 0.15f;
    //천구재화
    public static ObscuredFloat chunguAbil = 0.015f;
    
    public static ObscuredInt visionSkill6GainIdx = 0;
    public static ObscuredInt visionSkill7GainIdx = 112;
    
    public static int GetSonIdx()
    {
        int level = ServerData.statusTable.GetTableData(StatusTable.Son_Level).Value;

        if (level >= 12000 && level < 50000)
        {
            return 3;
        }

        if (level >= 50000 && level < 100000)
        {
            return 4;
        }

        if (level >= 100000)
        {
            return 5;
        }

        int ret = 0;



        if (level >= 9000)
        {
            level -= 3000;
        }

        ret = level / SonEvolutionDivdeNum;
        ret = Mathf.Min(ret, CommonUiContainer.Instance.sonThumbNail.Count - 1);
        return ret;
    }
}

public static class DamageBalance
{
    public readonly static ObscuredFloat baseMinDamage = 0.98f;
    public readonly static ObscuredFloat baseMaxDamage = 1.02f;
    public static float GetRandomDamageRange()
    {
        return Random.Range(baseMinDamage + PlayerStats.GetDamBalanceAddValue(), baseMaxDamage);
    }
}

