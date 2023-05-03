using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//삭제하거나순서절대바꾸면안됨
public enum StatusType
{
    AttackAddPer, // icon
    CriticalProb, //icon
    CriticalDam, //
    SkillCoolTime, //icon
    SkillDamage, //icon
    MoveSpeed,
    DamBalance,
    HpAddPer, //icon
    MpAddPer, //icon
    GoldGainPer, //icon
    ExpGainPer, //icon
    AttackAdd, //icon
    Hp, //icon
    Mp, //icon
    HpRecover, //icon
    MpRecover, //icon
    MagicStoneAddPer, //icon
    Damdecrease,
    IgnoreDefense,
    DashCount,
    DropAmountAddPer,
    BossDamAddPer,
    SkillAttackCount,
    PenetrateDefense,
    SuperCritical1Prob,
    SuperCritical1DamPer,
    MarbleAddPer,
    SuperCritical2DamPer, //필멸

    //Smith
    growthStoneUp,
    WeaponHasUp,
    NorigaeHasUp,
    PetEquipHasUp,
    PetEquipProbUp,
    DecreaseBossHp,
    OneYearBuff,
    SuperCritical3DamPer, //지옥
    SuperCritical4DamPer, //천상
    MonthBuff,
    FlowerHasValueUpgrade,
    SuperCritical5DamPer, //도깨비
    DokebiFireHasValueUpgrade,
    HellHasValueUpgrade,
    SuperCritical6DamPer, //신수
    SuperCritical7DamPer, //수미베기
    SumiHasValueUpgrade,
    SuperCritical8DamPer, //단전(?)베기 ->경락마사지에 가`됨 
    SuperCritical9DamPer, //흉수베기 
    SuperCritical10DamPer, //도적베기 
    NorigaeGoldAbilUp, //노리개 장착효과중 기본무공 효과 버프 
    SuperCritical11DamPer, //수호동물베기 
    SuperCritical12DamPer, //암흑베기 
    SuperCritical13DamPer, //중단전베기 
    TreasureHasValueUpgrade, // 보물당 섬광베기
    SuperCritical14DamPer, //여우베기 
}


public static class PlayerStats
{
    public static double GetTotalPower()
    {
        double baseAttack = GetBaseAttackPower();
        double baseAttackPer = GetBaseAttackAddPercentValue();
        double criProb = GetCriticalProb();
        double criDam = CriticalDam();
        double coolTIme = GetSkillCoolTimeDecreaseValue();
        double skillDam = GetSkillDamagePercentValue();
        double hpBase = GetMaxHp();
        double hpAddPer = GetMaxHpPercentAddValue();
        double mpBase = GetMaxMp();
        double mpAddPer = GetMaxMpPercentAddValue();

        double ignoreDefense = GetIgnoreDefenseValue();
        double decreaseDam = GetDamDecreaseValue();
        double skillAttackCount = GetSkillHitAddValue();
        double penetration = GetPenetrateDefense();
        double superCriticalProb = GetSuperCriticalProb();

        double feelMulDam = GetSuperCritical2DamPer();
        double hellDam = GetSuperCritical3DamPer();
        double chunSangDam = GetSuperCritical4DamPer();
        double dokebiDam = GetSuperCritical5DamPer();
        double sinsuDam = GetSuperCritical6DamPer();
        double sumiDam = GetSuperCritical7DamPer();

        double gyungRock = GetSuperCritical8DamPer();
        double saHung = GetSuperCritical9DamPer();
        double doJuk = GetSuperCritical10DamPer();
        double suho = GetSuperCritical11DamPer();
        double dark = GetSuperCritical12DamPer();
        double fox = GetSuperCritical14DamPer();

        double totalPower =
            ((baseAttack + baseAttack * baseAttackPer)
             * (Mathf.Max((float)criProb, 0.01f) * 100f * Mathf.Max((float)criDam, 0.01f))
             * (Mathf.Max((float)skillDam, 0.01f) * 100f)
             * (Mathf.Max((float)coolTIme, 0.01f)) * 100f)
            + ((hpBase + hpBase * hpAddPer) + (mpBase + mpBase * mpAddPer))
            + ((baseAttack + baseAttack * baseAttackPer)
               * (Mathf.Max((float)ignoreDefense, 0.01f)) * 100f
               * (Mathf.Max((float)decreaseDam, 0.01f)) * 100f
               * (Mathf.Max((float)skillAttackCount, 0.01f)) * 100f
               * (Mathf.Max((float)penetration, 0.01f)) * 100f
            );

        totalPower += totalPower * GetSuperCriticalDamPer() * superCriticalProb;

        totalPower += totalPower * feelMulDam;
        totalPower += totalPower * gyungRock;
        totalPower += totalPower * hellDam;
        totalPower += (totalPower * chunSangDam);
        totalPower += (totalPower * dokebiDam);
        totalPower += (totalPower * suho);
        totalPower += (totalPower * fox);
        totalPower += (totalPower * sinsuDam);
        totalPower += (totalPower * saHung);
        totalPower += (totalPower * sumiDam);
        totalPower += (totalPower * doJuk);
        totalPower += (totalPower * dark);

        //     float totalPower =
        //((baseAttack + baseAttack * baseAttackPer)
        // * (Mathf.Max(criProb, 0.01f) * 100f * Mathf.Max(criDam, 0.01f))
        // * (Mathf.Max(skillDam, 0.01f) * 100f)
        // * (Mathf.Max(coolTIme, 0.01f)) * 100)
        // + ((hpBase + hpBase * hpAddPer) + (mpBase + mpBase * mpAddPer));

        return totalPower * 0.01f;
    }

    public static float GetMoveSpeedValue()
    {
        float ret = 0f;
        ret += GetMarbleValue(StatusType.MoveSpeed);

        return ret;
    }

    public static float GetDropAmountAddValue()
    {
        float ret = 0f;

        ret += GetMarbleValue(StatusType.DropAmountAddPer);

        return ret;
    }

    public static float GetDamDecreaseValue()
    {
        float ret = 0f;

        ret += GetMarbleValue(StatusType.Damdecrease);
        ret += GetMagicBookHasPercentValue(StatusType.Damdecrease);
        ret += GetSinsuEquipEffect(StatusType.Damdecrease);
        ret += GetRelicHasEffect(StatusType.Damdecrease);
        return ret;
    }

    public static float GetBossDamAddValue()
    {
        float ret = 0f;

        ret += GetMarbleValue(StatusType.BossDamAddPer);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.BossDamage_memory);
        ret += GetMagicBookHasPercentValue(StatusType.BossDamAddPer);

        return ret;
    }

    public static int GetSkillHitAddValue()
    {
        int ret = 0;

        ret += (int)GetMarbleValue(StatusType.SkillAttackCount);
        ret += (int)GetMagicBookHasPercentValue(StatusType.SkillAttackCount);
        ret += (int)GetSinsuEquipEffect(StatusType.SkillAttackCount);

        return ret;
    }

    public static float sogulGab = 20f;
    public static float sogulValuePerGab = 0.5f;
    public static float baekPassiveLock = 200;

    public static float GetPassiveSkillValue(StatusType statusType)
    {
        float ret = 0f;

        var tableData = TableManager.Instance.PassiveSkill.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Abilitytype != (int)statusType) continue;

            var serverData = ServerData.passiveServerTable.TableDatas[tableData[i].Stringid];

            int level = serverData.level.Value;


            if (level != 0)
            {
                ret += level * tableData[i].Abilityvalue;
            }
        }

        ret = ret + ret * GetPassiveAdvanceValue();


        return ret;
    }

    public static float GetPassiveSkill2Value(StatusType statusType)
    {
        float ret = 0f;

        var tableData = TableManager.Instance.PassiveSkill2.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Abilitytype != (int)statusType) continue;

            var serverData = ServerData.passive2ServerTable.TableDatas[tableData[i].Stringid];

            int level = serverData.level.Value;


            if (level != 0)
            {
                ret += level * tableData[i].Abilityvalue;
            }
        }


        return ret;
    }

    public static float GetPassiveAdvanceValue()
    {
        int floor = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.yoguiSogulLastClear].Value;

        float gap = floor / sogulGab;

        return gap * sogulValuePerGab;
    }

    #region AttackPower

    public static float GetBaseAttackPower()
    {
        float ret = 0f;

        ret += ServerData.statusTable.GetStatusValue(StatusTable.AttackLevel_Gold);
        ret += ServerData.petTable.GetStatusValue(StatusType.AttackAdd);
        ret += GetWeaponEquipPercentValue(StatusType.AttackAdd);
        ret += GetSkillCollectionValue(StatusType.AttackAdd);
        ret += GetPassiveSkillValue(StatusType.AttackAdd);
        ret += GetMarbleValue(StatusType.AttackAdd);
        ret += GetSkillHasValue(StatusType.AttackAdd);
        ret += GetYomulUpgradeValue(StatusType.AttackAdd);
        //ret += GetTitleAbilValue(StatusType.AttackAdd);
        ret += GetTitleLevelAbilValue(StatusType.AttackAdd);

        ret += GetBuffValue(StatusType.AttackAdd);
        ret += GetRelicHasEffect(StatusType.AttackAdd);

        ret += GetStageRelicHasEffect(StatusType.AttackAdd);
        ret += GetSonAbilHasEffect(StatusType.AttackAdd);
        ret += GetCaveBeltAttackAdd();

        ret += GetGumGiAttackValue();
        ret += GetWeaponCollectionHasValue(StatusType.AttackAdd);

        return ret;
    }

    public static float GetGumGiAttackValue()
    {
        int idx = ServerData.equipmentTable.TableDatas[EquipmentTable.WeaponEnhance].Value;

        float ret = (float)TableManager.Instance.gumGiTable.dataArray[idx].Abilvalue;


        if (ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value >=
            TableManager.Instance.gumGiTable.dataArray[200].Require)
        {
            int over200 = Mathf.Max(0,
                ((int)ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value -
                 TableManager.Instance.gumGiTable.dataArray[200].Require) / 50000);
            ret += over200 * GameBalance.gumgiAttackValue200;
        }

        return ret + ret * GetGumgiAbilAddValue();
    }

    public static float GetGumIgDefenseValue()
    {
        int idx = ServerData.equipmentTable.TableDatas[EquipmentTable.WeaponEnhance].Value;

        float ret = (float)TableManager.Instance.gumGiTable.dataArray[idx].Abilvalue2;

        if (ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value >=
            TableManager.Instance.gumGiTable.dataArray[200].Require)
        {
            int over200 = Mathf.Max(0,
                ((int)ServerData.goodsTable.GetTableData(GoodsTable.SwordPartial).Value -
                 TableManager.Instance.gumGiTable.dataArray[200].Require) / 50000);
            ret += over200 * GameBalance.gumgiDefenseValue200;
        }

        return ret + ret * GetGumgiAbilAddValue();
    }

    public static float GetCollectionAbilValue(StatusType type)
    {
        var enemyTable = TableManager.Instance.EnemyTable.dataArray;
        float ret = 0f;

        for (int i = 0; i < enemyTable.Length; i++)
        {
            if ((StatusType)enemyTable[i].Collectionabiltype == type)
            {
                ret += ServerData.collectionTable.GetCollectionAbilValue(enemyTable[i]);
            }
        }

        return ret;
    }


    public static float GetBaseAttackAddPercentValue()
    {
        float ret = 0f;

        ret += ServerData.statusTable.GetStatusValue(StatusTable.IntLevelAddPer_StatPoint);
        ret += GetWeaponHasPercentValue(StatusType.AttackAddPer);
        ret += GetMagicBookHasPercentValue(StatusType.AttackAddPer);
        ret += GetCostumeAttackPowerValue();
        ret += GetSkillCollectionValue(StatusType.AttackAddPer);
        ret += GetPassiveSkillValue(StatusType.AttackAddPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.AttackAddPer);
        ret += GetMarbleValue(StatusType.AttackAddPer);
        ret += GetStageRelicHasEffect(StatusType.AttackAddPer);
        ret += GetSonAbilHasEffect(StatusType.AttackAddPer);
        ret += GetAsuraAbilValue(StatusType.AttackAddPer);
        ret += GetGuildPetEffect(StatusType.AttackAddPer);
        ret += GetMaskAttackAddPerDam();
        ret += GetRelicReleaseValue();
        ret += GetHellAbilHasEffect(StatusType.AttackAddPer);
        ret += GetChunAbilHasEffect(StatusType.AttackAddPer);
        ret += GetDokebiFireAbilHasEffect(StatusType.AttackAddPer);
        ret += GetMagicBookCollectionHasValue(StatusType.AttackAddPer);
        return ret;
    }

    public static ObscuredFloat relicReleaseValue = 30000f;

    public static float GetRelicReleaseValue()
    {
        int divideNum = (int)(ServerData.userInfoTable.TableDatas[UserInfoTable.usedRelicTicketNum].Value / 1000f);
        return relicReleaseValue * divideNum;
    }

    public static float GetCostumeAttackPowerValue()
    {
        float ret = ServerData.costumeServerTable.GetCostumeAbility(StatusType.AttackAddPer);
        return ret;
    }

    public static float GetCalculatedAttackPower()
    {
        float ret = 0f;

        float baseAttackPower = GetBaseAttackPower();

        float baseAttackAddPercentValue = GetBaseAttackAddPercentValue();

        ret += baseAttackPower;

        ret += baseAttackPower * baseAttackAddPercentValue;

        return ret;
    }

    public static float GetWeaponEquipPercentValue(StatusType type)
    {
        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.Weapon].Value;

        var e = TableManager.Instance.WeaponData.GetEnumerator();

        float ret = 0f;
        while (e.MoveNext())
        {
            if (e.Current.Value.Id != equipId) continue;
            if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Weaponeffectid,
                    out var effectData) == false) continue;

            int currentLevel = ServerData.weaponTable.GetWeaponData(e.Current.Value.Stringid).level.Value;

            if (effectData.Equipeffecttype1 == (int)type)
            {
                ret += effectData.Equipeffectbase1;
                ret += currentLevel * effectData.Equipeffectvalue1;
            }

            if (effectData.Equipeffecttype2 == (int)type)
            {
                ret += effectData.Equipeffectbase2;
                ret += currentLevel * effectData.Equipeffectvalue2;
            }

            break;
        }

        return ret;
    }

    public static float GetWeaponHasPercentValue(StatusType type)
    {
        var e = TableManager.Instance.WeaponData.GetEnumerator();

        float ret = 0f;
        while (e.MoveNext())
        {
            if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Weaponeffectid,
                    out var effectData) == false) continue;

            var weaponServertable = ServerData.weaponTable.TableDatas[e.Current.Value.Stringid];

            if (weaponServertable.hasItem.Value == 0) continue;

            int currentLevel = ServerData.weaponTable.GetWeaponData(e.Current.Value.Stringid).level.Value;

            if (effectData.Haseffecttype1 == (int)type)
            {
                ret += effectData.Haseffectbase1;
                ret += currentLevel * effectData.Haseffectvalue1;
            }

            if (effectData.Haseffecttype2 == (int)type)
            {
                ret += effectData.Haseffectbase2;
                ret += currentLevel * effectData.Haseffectvalue2;
            }
        }

        if (ActiveSmithValue(type))
        {
            ret = ret * GetSmithValue(StatusType.WeaponHasUp);
        }
        else
        {
        }


        return ret;
    }

    public static float GetWeaponCollectionHasValue(StatusType type)
    {
        var tableData = TableManager.Instance.WeaponTable.dataArray;

        var serverData = ServerData.weaponTable.TableDatas;

        float ret = 0f;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].WEAPONTYPE == WeaponType.Basic) continue;
            if (tableData[i].WEAPONTYPE == WeaponType.View) continue;
            if (serverData[tableData[i].Stringid].hasItem.Value == 0) continue;

            if ((StatusType)tableData[i].Collectioneffecttype == type)
            {
                ret += tableData[i].Collectioneffectvalue;
            }
        }

        return ret;
    }

    public static float GetRingCollectionHasValue(StatusType type)
    {
        var tableData = TableManager.Instance.NewGachaTable.dataArray;

        var serverData = ServerData.newGachaServerTable.TableDatas;

        float ret = 0f;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].RINGTYPE == RingType.Basic) continue;
            if (tableData[i].RINGTYPE == RingType.View) continue;
            if (serverData[tableData[i].Stringid].hasItem.Value == 0) continue;

            if ((StatusType)tableData[i].Collectioneffecttype == type)
            {
                ret += tableData[i].Collectioneffectvalue;
            }
        }

        return ret;
    }

    public static float GetMagicBookCollectionHasValue(StatusType type)
    {
        var tableData = TableManager.Instance.MagicBookTable.dataArray;

        var serverData = ServerData.magicBookTable.TableDatas;

        float ret = 0f;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].MAGICBOOKTYPE == MagicBookType.Basic) continue;
            if (tableData[i].MAGICBOOKTYPE == MagicBookType.View) continue;
            if (serverData[tableData[i].Stringid].hasItem.Value == 0) continue;

            if ((StatusType)tableData[i].Collectioneffecttype == type)
            {
                ret += tableData[i].Collectioneffectvalue;
            }
        }

        return ret;
    }

    public static float GetMagicBookEquipPercentValue(StatusType type)
    {
        var e = TableManager.Instance.MagicBoocDatas.GetEnumerator();

        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.MagicBook].Value;

        float ret = 0f;
        while (e.MoveNext())
        {
            if (e.Current.Value.Id != equipId) continue;
            if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Magicbookeffectid,
                    out var effectData) == false) continue;

            int currentLevel = ServerData.magicBookTable.GetMagicBookData(e.Current.Value.Stringid).level.Value;

            if (effectData.Equipeffecttype1 == (int)type)
            {
                ret += effectData.Equipeffectbase1;
                ret += currentLevel * effectData.Equipeffectvalue1;
            }

            if (effectData.Equipeffecttype2 == (int)type)
            {
                ret += effectData.Equipeffectbase2;
                ret += currentLevel * effectData.Equipeffectvalue2;
            }

            break;
        }

        return ret;
    }

    private static Dictionary<StatusType, float> magicBookHasValue = new Dictionary<StatusType, float>();

    private static void ResetMagicBookHas()
    {
        magicBookHasValue.Clear();
    }

    public static float GetMagicBookHasPercentValue(StatusType type)
    {
        float ret = 0f;

        if (magicBookHasValue.ContainsKey(type))
        {
            ret = magicBookHasValue[type];
        }
        else
        {
            var e = TableManager.Instance.MagicBoocDatas.GetEnumerator();


            while (e.MoveNext())
            {
                if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Magicbookeffectid,
                        out var effectData) == false) continue;

                var magieBookServerData = ServerData.magicBookTable.TableDatas[e.Current.Value.Stringid];

                if (magieBookServerData.hasItem.Value == 0) continue;

                int currentLevel = ServerData.magicBookTable.GetMagicBookData(e.Current.Value.Stringid).level.Value;

                if (effectData.Haseffecttype1 == (int)type)
                {
                    ret += effectData.Haseffectbase1;
                    ret += currentLevel * effectData.Haseffectvalue1;
                }

                if (effectData.Haseffecttype2 == (int)type)
                {
                    ret += effectData.Haseffectbase2;
                    ret += currentLevel * effectData.Haseffectvalue2;
                }

                if (effectData.Haseffecttype3 == (int)type)
                {
                    ret += effectData.Haseffectbase3;
                    ret += currentLevel * effectData.Haseffectvalue3;
                }
            }

            magicBookHasValue.Add(type, ret);
        }


        if (ActiveSmithValue(type))
        {
            ret = ret * GetSmithValue(StatusType.NorigaeHasUp);
        }
        else
        {
        }

        return ret;
    }

    public static float GetSkillCollectionValue(StatusType type)
    {
        var e = TableManager.Instance.SkillData.GetEnumerator();

        float ret = 0f;
        while (e.MoveNext())
        {
            if ((StatusType)e.Current.Value.Collectionabiltype != type) continue;

            int skillCurrentLevel =
                ServerData.skillServerTable.TableDatas[SkillServerTable.SkillCollectionLevel][e.Current.Value.Id].Value;

            if (skillCurrentLevel != 0)
            {
                ret += skillCurrentLevel * e.Current.Value.Collectionvalue;
            }
        }

        return ret;
    }


    public static float GetSkillHasValue(StatusType type)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.SkillTable.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (tableDatas[i].Haseffecttype != (int)type) continue;

            int awakeNum = ServerData.skillServerTable.TableDatas[SkillServerTable.SkillAwakeNum][tableDatas[i].Id]
                .Value;

            if (awakeNum == 0) continue;


            ret += awakeNum * tableDatas[i].Haseffectvalue;
        }

        return ret;
    }

    public static float GetMarbleValue(StatusType type)
    {
        float ret = 0f;

        bool isMarbleAwaked = ServerData.userInfoTable.TableDatas[UserInfoTable.marbleAwake].Value == 1;

        var tableDatas = TableManager.Instance.MarbleTable.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (ServerData.marbleServerTable.TableDatas[tableDatas[i].Stringid].hasItem.Value == 0) continue;

            for (int j = 0; j < tableDatas[i].Abilitytype.Length; j++)
            {
                if (tableDatas[i].Abilitytype[j] == (int)type)
                {
                    ret += isMarbleAwaked == false ? tableDatas[i].Abilityvalue[j] : tableDatas[i].Awakevalue[j];
                }
            }
        }

        return ret;
    }

    #endregion

    #region SkillDamage

    public static float GetSkillDamagePercentValue()
    {
        float ret = 0f;

        ret += GetWeaponEquipPercentValue(StatusType.SkillDamage);
        ret += GetMagicBookEquipPercentValue(StatusType.SkillDamage);
        ret += GetMagicBookHasPercentValue(StatusType.SkillDamage);


        ret += ServerData.statusTable.GetStatusValue(StatusTable.SkillDamage_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.SkillDamage);
        ret += GetSkillCollectionValue(StatusType.SkillDamage);
        ret += GetPassiveSkillValue(StatusType.SkillDamage);
        ret += ServerData.petTable.GetStatusValue(StatusType.SkillDamage);

        //ret += GetTitleAbilValue(StatusType.SkillDamage);
        ret += GetRelicHasEffect(StatusType.SkillDamage);

        ret += GetSinsuEquipEffect(StatusType.SkillDamage);
        ret += GetStageRelicHasEffect(StatusType.SkillDamage);
        ret += GetSonAbilHasEffect(StatusType.SkillDamage);
        ret += GetYachaSkillPercentValue();
        ret += GuildManager.abilValue;
        ret += GetTitleMagicBookAbilValue(StatusType.SkillDamage);

        return ret;
    }

    private const string yachaKey = "weapon21";

    public static float GetYachaSkillPercentValue()
    {
        bool hasYacha = ServerData.weaponTable.TableDatas[yachaKey].hasItem.Value == 1;

        if (hasYacha == false) return 0f;

        return ServerData.statusTable.GetTableData(StatusTable.Level).Value * GameBalance.YachaSkillAddValuePerLevel;
    }

    public static float GetYachaIgnoreDefenseValue()
    {
        bool hasYacha = ServerData.weaponTable.TableDatas[yachaKey].hasItem.Value == 1;

        bool cockAwake = ServerData.userInfoTable.TableDatas[UserInfoTable.cockAwake].Value == 1;

        if (hasYacha == false || cockAwake == false) return 0f;

        return ServerData.statusTable.GetTableData(StatusTable.Level).Value *
               GameBalance.YachaIgnoreDefenseAddValuePerLevel;
    }

    public static float GetYachaChunSlashValue()
    {
        bool hasYacha = ServerData.weaponTable.TableDatas[yachaKey].hasItem.Value == 1;

        bool dogAwake = ServerData.userInfoTable.TableDatas[UserInfoTable.dogAwake].Value == 1;

        if (hasYacha == false || dogAwake == false) return 0f;

        return ServerData.statusTable.GetTableData(StatusTable.Level).Value *
               GameBalance.YachaChunSlashAddValuePerLevel;
    }

    #endregion

    #region SkillCoolTime

    public static float GetSkillCoolTimeDecreaseValue()
    {
        float ret = 0f;

        ret += GetWeaponEquipPercentValue(StatusType.SkillCoolTime);
        ret += GetMagicBookEquipPercentValue(StatusType.SkillCoolTime);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.SkillCoolTime_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.SkillCoolTime);
        ret += GetBuffValue(StatusType.SkillCoolTime);
        ret += GetYomulUpgradeValue(StatusType.SkillCoolTime);

        ret += IsChunAttackSpeedAwake() ? 0.05f : 0f;

        return ret;
    }

    #endregion

    #region DamBalance

    public static float GetDamBalanceAddValue()
    {
        float addValue1 = ServerData.statusTable.GetStatusValue(StatusTable.DamageBalance_memory);
        return addValue1;
    }

    #endregion

    #region Critical

    public static bool ActiveCritical()
    {
        return Random.value < GetCriticalProb();
    }

    public static bool ActiveSuperCritical()
    {
        return Random.value < GetSuperCriticalProb();
    }

    public static float GetCriticalProb()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalLevel_Gold);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalLevel_StatPoint);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalLevel_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.CriticalProb);
        ret += ServerData.petTable.GetStatusValue(StatusType.CriticalProb);
        ret += GetSkillCollectionValue(StatusType.CriticalProb);

        return ret;
    }

    public static float CriticalDam()
    {
        float ret = 0f;

        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalDamLevel_Gold);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalDamLevel_StatPoint);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.CriticalDamLevel_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.CriticalDam);
        ret += GetWeaponHasPercentValue(StatusType.CriticalDam);
        ret += GetMagicBookHasPercentValue(StatusType.CriticalDam);
        ret += GetSkillCollectionValue(StatusType.CriticalDam);
        ret += ServerData.petTable.GetStatusValue(StatusType.CriticalDam);
        ret += GetStageRelicHasEffect(StatusType.CriticalDam);
        ret += GetSonAbilHasEffect(StatusType.CriticalDam);
        ret += GetSinsuEquipEffect(StatusType.CriticalDam);
        ret += GetSusanoAbil(StatusType.CriticalDam);
        ret += GetRingCollectionHasValue(StatusType.CriticalDam);

        return ret;
    }

    #endregion

    #region BuffEffect

    public static float GetGoldPlusValue()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.GoldGain_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.GoldGainPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.GoldGainPer);
        ret += GetBuffValue(StatusType.GoldGainPer);
        ret += GetMarbleValue(StatusType.GoldGainPer);
        ret += GetMagicBookHasPercentValue(StatusType.GoldGainPer);

        //ret += GetTitleAbilValue(StatusType.GoldGainPer);
        ret += GetHotTimeBuffEffect(StatusType.GoldGainPer);
        ret += GetHotTimeEventBuffEffect(StatusType.GoldGainPer);
        ret += GetGuildPetEffect(StatusType.GoldGainPer);

        return ret;
    }

    public static float GetGoldPlusValueExclusiveBuff()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.GoldGain_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.GoldGainPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.GoldGainPer);
        ret += GetMarbleValue(StatusType.GoldGainPer);
        ret += GetMagicBookHasPercentValue(StatusType.GoldGainPer);

        //ret += GetTitleAbilValue(StatusType.GoldGainPer);
        ret += GetHotTimeBuffEffect(StatusType.GoldGainPer);
        ret += GetHotTimeEventBuffEffect(StatusType.GoldGainPer);
        ret += GetGuildPetEffect(StatusType.GoldGainPer);


        return ret;
    }

    public static float GetBaseExpPlusValue_BuffAllIgnored()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.ExpGain_memory);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.ExpGainPer);
        ret += GetMarbleValue(StatusType.ExpGainPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.ExpGainPer);

        ret += GetTitleStageAbilValue(StatusType.ExpGainPer);
        ret += GetGuildPetEffect(StatusType.ExpGainPer);
        ret += GetPetHomeAbilValue(StatusType.ExpGainPer);
        ret += GetMarkBuffAddValue();
    
        ret += GetWeaponHasPercentValue(StatusType.ExpGainPer);

        return ret;
    }

    public static float GetExpPlusValue_WithAllBuff()
    {
        float ret = 0f;

        ret += GetBaseExpPlusValue_BuffAllIgnored();
        //
        ret += GetBuffValue(StatusType.ExpGainPer);
        ret += GetHotTimeBuffEffect(StatusType.ExpGainPer);
        ret += GetHotTimeEventBuffEffect(StatusType.ExpGainPer);
        ret += GetOneYearBuffValue(StatusType.ExpGainPer);
        ret += GetChuSeokBuffValue(StatusType.ExpGainPer);
        ret += GetChuSeokBuffValue2(StatusType.ExpGainPer);
        //

        return ret;
    }

    public static float GetExpPlusValueIncludeHotTimeBuffOnly()
    {
        float ret = 0f;
        
        ret += GetBaseExpPlusValue_BuffAllIgnored();

        ret += GetHotTimeEventBuffEffect(StatusType.ExpGainPer);
        
        return ret;
    }

    public static float GetMarkBuffAddValue()
    {
        int idx = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.hellMark].Value;
        if (idx == 0) return 0f;
        if (idx >= GameBalance.warMarkAbils.Count) return 0f;

        return GameBalance.warMarkAbils[idx];
    }

    //1주년 버프 키값
    private static string ob = "ob";

    private static float GetOneYearBuffValue(StatusType status)
    {
        if (ServerData.buffServerTable.TableDatas[ob].remainSec.Value <= 0f) return 0f;

        switch (status)
        {
            case StatusType.ExpGainPer:
            {
                return 100f;
            }
                break;
            case StatusType.MagicStoneAddPer:
            {
                return 200f;
            }
                break;
            case StatusType.MarbleAddPer:
            {
                return 50f;
            }
                break;
        }

        return 0f;
    }


    //월간 패스 키값
    private static string mf11 = "mf11";

    private static float GetChuSeokBuffValue(StatusType status)
    {
        if (ServerData.buffServerTable.TableDatas[mf11].remainSec.Value <= 0f) return 0f;

        switch (status)
        {
            case StatusType.ExpGainPer:
            {
                return 150f;
            }
                break;
            case StatusType.MagicStoneAddPer:
            {
                return 400f;
            }
                break;
            case StatusType.MarbleAddPer:
            {
                return 100f;
            }
                break;
        }

        return 0f;
    }

    private static string ma11 = "ma11";

    private static float GetChuSeokBuffValue2(StatusType status)
    {
        if (ServerData.buffServerTable.TableDatas[ma11].remainSec.Value <= 0f) return 0f;

        switch (status)
        {
            case StatusType.ExpGainPer:
            {
                return 200f;
            }
                break;
            case StatusType.MagicStoneAddPer:
            {
                return 500f;
            }
                break;
            case StatusType.MarbleAddPer:
            {
                return 200f;
            }
                break;
        }

        return 0f;
    }


    //월간 패스 키값
    private static string mf12 = "mf12";

    private static float GetMonthlyFreeBuffValue(StatusType status)
    {
        if (ServerData.buffServerTable.TableDatas[mf12].remainSec.Value <= 0f) return 0f;

        switch (status)
        {
            case StatusType.ExpGainPer:
            {
                return 150f;
            }
                break;
            case StatusType.MagicStoneAddPer:
            {
                return 400f;
            }
                break;
            case StatusType.MarbleAddPer:
            {
                return 100f;
            }
                break;
        }

        return 0f;
    }

    private static string ma12 = "ma12";

    private static float GetMonthlyAdBuffValue(StatusType status)
    {
        if (ServerData.buffServerTable.TableDatas[ma12].remainSec.Value <= 0f) return 0f;

        switch (status)
        {
            case StatusType.ExpGainPer:
            {
                return 200f;
            }
                break;
            case StatusType.MagicStoneAddPer:
            {
                return 500f;
            }
                break;
            case StatusType.MarbleAddPer:
            {
                return 200f;
            }
                break;
        }

        return 0f;
    }


    public static float GetMagicStonePlusValue()
    {
        float ret = 0f;

        ret += GetHotTimeBuffEffect(StatusType.MagicStoneAddPer);
        ret += GetHotTimeEventBuffEffect(StatusType.MagicStoneAddPer);
        ret += GetBuffValue(StatusType.MagicStoneAddPer);
        ret += GetOneYearBuffValue(StatusType.MagicStoneAddPer);
        ret += GetChuSeokBuffValue(StatusType.MagicStoneAddPer);
        ret += GetChuSeokBuffValue2(StatusType.MagicStoneAddPer);


        //  ret += GetMonthlyFreeBuffValue(StatusType.MagicStoneAddPer);
        //  ret += GetMonthlyAdBuffValue(StatusType.MagicStoneAddPer);

        return ret;
    }

    public static float GetMarblePlusValue()
    {
        float ret = 0f;

        ret += GetHotTimeBuffEffect(StatusType.MarbleAddPer);
        ret += GetHotTimeEventBuffEffect(StatusType.MarbleAddPer);
        ret += GetBuffValue(StatusType.MarbleAddPer);

        ret += GetOneYearBuffValue(StatusType.MarbleAddPer);
        ret += GetChuSeokBuffValue(StatusType.MarbleAddPer);
        ret += GetChuSeokBuffValue2(StatusType.MarbleAddPer);

        //  ret += GetMonthlyFreeBuffValue(StatusType.MarbleAddPer);
        // ret += GetMonthlyAdBuffValue(StatusType.MarbleAddPer);

        return ret;
    }

    public static float GetBuffValue(StatusType type)
    {
        float ret = 0f;

        var tableData = TableManager.Instance.BuffTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if ((int)type == tableData[i].Bufftype)
            {
                //-1은 무한

                if (tableData[i].Yomulid == -1)
                {
                    if (ServerData.buffServerTable.TableDatas[tableData[i].Stringid].remainSec.Value != 0f)
                    {
                        ret += tableData[i].Buffvalue;
                    }
                }
                //요물
                else
                {
                    if (ServerData.userInfoTable.TableDatas[UserInfoTable.buffAwake].Value == 0)
                    {
                        if (ServerData.buffServerTable.TableDatas[tableData[i].Stringid].remainSec.Value != 0f)
                        {
                            ret += tableData[i].Buffvalue;
                        }
                    }
                    //각성후
                    else
                    {
                        ret += tableData[i].Buffawakevalue;
                    }
                }
            }
        }

        return ret;
    }

    #endregion

    #region HP&MP

    public static float GetMaxHp()
    {
        float originHp = GetOriginHp();

        float hpAddPerValue = GetMaxHpPercentAddValue();

        return originHp + originHp * hpAddPerValue;
    }

    public static float GetOriginHp()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.HpLevel_Gold);
        ret += ServerData.petTable.GetStatusValue(StatusType.Hp);
        ret += GetMagicBookEquipPercentValue(StatusType.Hp);

        ret += GetSinsuEquipEffect(StatusType.Hp);
        ret += GetRelicHasEffect(StatusType.Hp);

        return ret;
    }

    public static float GetMaxHpPercentAddValue()
    {
        float ret = 0f;
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.HpAddPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.HpAddPer);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.HpPer_StatPoint);
        ret += GetPassiveSkillValue(StatusType.HpAddPer);

        //ret += GetTitleAbilValue(StatusType.HpAddPer);
        ret += GetRelicHasEffect(StatusType.HpAddPer);

        ret += GetMagicBookEquipPercentValue(StatusType.HpAddPer);

        return ret;
    }

    public static float GetMaxMp()
    {
        float originMp = GetOriginMp();

        float mpAddPerValue = GetMaxMpPercentAddValue();

        return originMp + originMp * mpAddPerValue;
    }

    public static float GetOriginMp()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.MpLevel_Gold);
        ret += ServerData.petTable.GetStatusValue(StatusType.Mp);
        return ret;
    }

    public static float GetMaxMpPercentAddValue()
    {
        float ret = 0f;
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.MpAddPer);
        ret += ServerData.petTable.GetStatusValue(StatusType.MpAddPer);
        ret += ServerData.statusTable.GetStatusValue(StatusTable.MpPer_StatPoint);
        return ret;
    }

    public static float GetHpRecover()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.HpRecover_Gold);
        return ret;
    }

    public static float GetMpRecover()
    {
        float ret = 0f;
        ret += ServerData.statusTable.GetStatusValue(StatusTable.MpRecover_Gold);
        return ret;
    }

    #endregion

    public static float GetIgnoreDefenseValue()
    {
        float ret = 300f;

        ret += ServerData.statusTable.GetStatusValue(StatusTable.IgnoreDefense_memory);
        ret += GetWeaponEquipPercentValue(StatusType.IgnoreDefense);
        ret += GetWeaponHasPercentValue(StatusType.IgnoreDefense);
        ret += ServerData.costumeServerTable.GetCostumeAbility(StatusType.IgnoreDefense);
        ret += ServerData.petTable.GetStatusValue(StatusType.IgnoreDefense);

        //ret += GetTitleAbilValue(StatusType.IgnoreDefense);
        ret += GetYomulUpgradeValue(StatusType.IgnoreDefense);

        ret += GetBuffValue(StatusType.IgnoreDefense);

        ret += GetRelicHasEffect(StatusType.IgnoreDefense);
        ret += GetStageRelicHasEffect(StatusType.IgnoreDefense);

        ret += GetSinsuEquipEffect(StatusType.IgnoreDefense);

        ret += GetYachaIgnoreDefenseValue();

        ret += GetAsuraAbilValue(StatusType.IgnoreDefense);
        ret += GetIndraAbilValue(StatusType.IgnoreDefense);

        ret += GetPassiveSkillValue(StatusType.IgnoreDefense);

        //
        ret += GetGumIgDefenseValue();
        //
        ret += GetGumihoAbil();

        return ret;
    }

    public static float GetPenetrateDefense()
    {
        float ret = 0f;

        ret += GetYomulUpgradeValue(StatusType.PenetrateDefense);

        ret += GetBuffValue(StatusType.PenetrateDefense);

        ret += GetStageRelicHasEffect(StatusType.PenetrateDefense);

        ret += GetPassiveSkillValue(StatusType.PenetrateDefense);

        ret += GetIndraAbilValue(StatusType.PenetrateDefense);

        ret += GetSusanoAbil(StatusType.PenetrateDefense);

        ret += GetOrochiAbilValue(StatusType.PenetrateDefense);

        return ret;
    }

    public static float GetSuperCriticalDamPer()
    {
        float ret = 0.5f;

        ret += GetSinsuEquipEffect(StatusType.SuperCritical1DamPer);
        ret += GetYomulUpgradeValue(StatusType.SuperCritical1DamPer);
        ret += GetStageRelicHasEffect(StatusType.SuperCritical1DamPer);
        ret += GetBuffValue(StatusType.SuperCritical1DamPer);
        ret += GetRelicHasEffect(StatusType.SuperCritical1DamPer);

        ret += ServerData.petTable.GetStatusValue(StatusType.SuperCritical1DamPer);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.ChunSlash_memory);

        ret += GetYachaChunSlashValue();

        ret += GetAsuraAbilValue(StatusType.SuperCritical1DamPer);

        ret += GetPassiveSkillValue(StatusType.SuperCritical1DamPer);

        return ret;
    }


    public static float GetSuperCriticalProb()
    {
        float ret = 0f;

        ret += GetYomulUpgradeValue(StatusType.SuperCritical1Prob);
        ret += GetBuffValue(StatusType.SuperCritical1Prob);
        ret += GetSinsuEquipEffect(StatusType.SuperCritical1Prob);

        return ret;
    }

    public static float GetSuperCritical2DamPer()
    {
        float ret = 0f;

        ret += GetWeaponHasPercentValue(StatusType.SuperCritical2DamPer);

        ret += GetFeelMulAddDam();

        ret += GetAsuraAbilValue(StatusType.SuperCritical2DamPer);

        ret += GetLeeMuGiAddDam();

        ret += GetRelicHasEffect(StatusType.SuperCritical2DamPer);

        //ret += GetTitleAbilValue(StatusType.SuperCritical2DamPer);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.FeelSlash_memory);

        ret += GetPetHomeAbilValue(StatusType.SuperCritical2DamPer);

        ret += GetGradeTestAbilValue(StatusType.SuperCritical2DamPer);
        
        ret += GetTitleWeaponAbilValue(StatusType.SuperCritical2DamPer);

        return ret;
    }

    public static float GetSuperCritical3DamPer()
    {
        float ret = 0f;

        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical3DamPer);

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical3DamPer);

        ret += GetRelicHasEffect(StatusType.SuperCritical3DamPer);

        ret += GetHellMarkValue();

        ret += GetHellAbilHasEffect(StatusType.SuperCritical3DamPer);

        ret += GetDragonBallAbil0Value();

        ret += GetHellRelicAbilValue();

        ret += GetPetHomeAbilValue(StatusType.SuperCritical3DamPer);

        ret += GetYumAbil(StatusType.SuperCritical3DamPer);

        ret += GetStageRelicHasEffect(StatusType.SuperCritical3DamPer);

        ret += GetSumiTowerEffect(StatusType.SuperCritical3DamPer);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.ZSlash_memory);

        return ret;
    }

    public static float GetSuperCritical4DamPer()
    {
        float ret = 0f;

        ret += GetChunMarkValue();

        ret += GetChunAbilHasEffect(StatusType.SuperCritical4DamPer);

        ret += GetRelicHasEffect(StatusType.SuperCritical4DamPer);

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical4DamPer);

        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical4DamPer);

        ret += GetDragonBallAbil1Value();

        ret += GetPetHomeAbilValue(StatusType.SuperCritical4DamPer);

        ret += GetOkAbil(StatusType.SuperCritical4DamPer);

        ret += GetStageRelicHasEffect(StatusType.SuperCritical4DamPer);

        ret += GetSumiTowerEffect(StatusType.SuperCritical4DamPer);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.Cslash_memory);

        return ret;
    }

    public static float GetSuperCritical5DamPer()
    {
        float ret = 0f;

        ret += GetDokebiMarkValue();

        ret += GetDokebiHornCritical5Add();

        ret += GetDokebiFireAbilHasEffect(StatusType.SuperCritical5DamPer);

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical5DamPer);

        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical5DamPer);

        ret += GetDoAbil(StatusType.SuperCritical5DamPer);

        ret += GetStageRelicHasEffect(StatusType.SuperCritical5DamPer);

        ret += GetRelicHasEffect(StatusType.SuperCritical5DamPer);

        ret += GetSumiTowerEffect(StatusType.SuperCritical5DamPer);

        ret += GetFoxCupAbilValue(GetCurrentFoxCupIdx(), 0);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.GiSlash_memory);

        return ret;
    }

    //수미베기
    public static float GetSuperCritical7DamPer()
    {
        float ret = 0f;

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical7DamPer);

        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical7DamPer);

        ret += GetSumiFireAbilHasEffect(StatusType.SuperCritical7DamPer);

        ret += GetSuAbil(StatusType.SuperCritical7DamPer);

        ret += GetStageRelicHasEffect(StatusType.SuperCritical7DamPer);

        ret += GetRelicHasEffect(StatusType.SuperCritical7DamPer);

        ret += GetSumiTowerEffect(StatusType.SuperCritical7DamPer);
        
        ret += GetFoxCupAbilValue(GetCurrentFoxCupIdx(), 2);

        ret += ServerData.statusTable.GetStatusValue(StatusTable.Gum_memory);

        return ret;
    }
 
    /// <summary>
    /// 혈자리베기
    /// </summary>
    /// <returns></returns>
    public static float GetSuperCritical8DamPer()
    {
        float ret = 0f;

        ret += GetGyungRockEffect(StatusType.SuperCritical8DamPer);
        
        return ret + ret * GetGuildTowerChimUpgradeValue();
    } 
    
    public static float GetSuperCritical13DamPer()
    {
        float ret = 0f;

        ret += GetGyungRockEffect2(StatusType.SuperCritical13DamPer);
        
        return ret + ret * GetGuildTowerChimUpgradeValue();
    }

    public static float GetSuperCritical9DamPer()
    {
        float ret = 0f;

        ret += GetSahyungTreasureAbilPlusValue();

        ret += GetPetHomeAbilValue(StatusType.SuperCritical9DamPer);
        
        ret += GetStageRelicHasEffect(StatusType.SuperCritical9DamPer);
        
        ret += GetWolfRingAbilValue(GetCurrentWolfRingIdx(), 0);
        
        return ret;
    }

    //섬광베기
    public static float GetSuperCritical10DamPer()
    {
        float ret = 0f;

        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical10DamPer);

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical10DamPer);

        ret += GetTresureAbilHasEffect(StatusType.SuperCritical10DamPer);

        ret += GetThiefAbil(StatusType.SuperCritical10DamPer);
        
        ret += GetStageRelicHasEffect(StatusType.SuperCritical10DamPer);
        
        return ret;
    }
    //심연베기
    public static float GetSuperCritical12DamPer()
    {
        float ret = 0f;
        
        ret += GetWeaponEquipPercentValue(StatusType.SuperCritical12DamPer);

        ret += GetMagicBookEquipPercentValue(StatusType.SuperCritical12DamPer);

        ret += GetDarkTreasureAbilHasEffect(StatusType.SuperCritical12DamPer);
        
        ret += GetDarkMarkValue();
        
        return ret;
    }
    //여우
    public static float GetSuperCritical14DamPer()
    {
        float ret = 0f;

        ret += GetFoxFireEffect(StatusType.SuperCritical14DamPer);
        
        return ret;
    }

    private static float superCritical11Value = -1;

    public static void ResetSuperCritical11CalculatedValue()
    {
        superCritical11Value = -1;
    }

    public static float GetSuperCritical11DamPer()
    {
        if (superCritical11Value == -1)
        {
            superCritical11Value = 0;
            
            var tableData = TableManager.Instance.suhoPetTable.dataArray;

            for (int i = 0; i < tableData.Length; i++)
            {
                var serverData = ServerData.suhoAnimalServerTable.TableDatas[tableData[i].Stringid];

                if (serverData.hasItem.Value == 0) continue;

                int currentLevel = serverData.level.Value;

                superCritical11Value += tableData[i].Abilvalue[currentLevel];
            }
        }
        
        //막아둠일단
        //return 0f;

        return superCritical11Value;
    }

    public static float GetSuperCritical6DamPer()
    {
        float ret = 0f;

        ret += GetPetHomeAbilValue(StatusType.SuperCritical6DamPer);

        ret += GetSasinsuStarAddValue();

        ret += GetFoxCupAbilValue(GetCurrentFoxCupIdx(), 1);

        ret += GetStageRelicHasEffect(StatusType.SuperCritical6DamPer);
        
        ret += GetSinsuTreasureAbilPlusValue();

        return ret;
    }

    public static float GetHellMarkValue()
    {
        float ret = 0f;
        if (ServerData.goodsTable.GetTableData(GoodsTable.h0).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[0].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h1).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[1].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h2).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[2].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h3).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[3].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h4).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[4].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h5).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[5].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h6).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[6].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h7).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[7].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h8).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[8].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.h9).Value == 1)
        {
            ret += TableManager.Instance.hellAbil.dataArray[9].Abilbasevalue;
        }

        return ret;
    }
    public static float GetDarkMarkValue()
    {
        float ret = 0f;
        if (ServerData.goodsTable.GetTableData(GoodsTable.d0).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[0].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d1).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[1].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d2).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[2].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d3).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[3].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d4).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[4].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d5).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[5].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d6).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[6].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.d7).Value >0)
        {
            ret += TableManager.Instance.DarkAbil.dataArray[7].Abilbasevalue;
        }


        return ret;
    }

    
    
    public static float GetChunMarkValue()
    {
        float ret = 0f;
        if (ServerData.goodsTable.GetTableData(GoodsTable.c0).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[0].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c1).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[1].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c2).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[2].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c3).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[3].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c4).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[4].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c5).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[5].Abilbasevalue;
        }

        if (ServerData.goodsTable.GetTableData(GoodsTable.c6).Value == 1)
        {
            ret += TableManager.Instance.chunMarkAbil.dataArray[6].Abilbasevalue;
        }

        return ret;
    }

    public static float GetDokebiMarkValue()
    {
        float ret = 0f;

        return ret;
    }

    public static float GetYomulUpgradeValue(StatusType type, bool onlyType1 = false, bool onlyType2 = false,
        int targetId = -1)
    {
        float ret = 0f;
        var tableDatas = TableManager.Instance.YomulAbilTable.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            var serverData = ServerData.yomulServerTable.TableDatas[tableDatas[i].Stringid];
            if (serverData.hasAbil.Value == 0) continue;
            if (targetId != -1 && i != targetId) continue;

            if (tableDatas[i].Abiltype == (int)type && onlyType2 == false)
            {
                if (type == StatusType.PenetrateDefense)
                {
                    float addValue = serverData.level.Value < 80
                        ? tableDatas[i].Abiladdvalue
                        : tableDatas[i].Abiladdvalue * 2f;
                    ret += tableDatas[i].Abilvalue + (serverData.level.Value * addValue);
                }
                else
                {
                    ret += tableDatas[i].Abilvalue + (serverData.level.Value * tableDatas[i].Abiladdvalue);
                }
            }

            if (tableDatas[i].Abiltype2 == (int)type && onlyType1 == false)
            {
                if (type == StatusType.PenetrateDefense)
                {
                    float addValue = tableDatas[i].Abiladdvalue2;
                    ret += tableDatas[i].Abilvalue2 + (serverData.level.Value * addValue);
                }
                else
                {
                    ret += tableDatas[i].Abilvalue2 + (serverData.level.Value * tableDatas[i].Abiladdvalue2);
                }
            }
        }

        return ret;
    }

    private static Dictionary<StatusType, float> titleHasValue = new Dictionary<StatusType, float>();

    public static void ResetTitleHas()
    {
        titleHasValue.Clear(); 
    }

    public static float GetTitleAbilValue(StatusType type)
    {
        float ret = 0f;

        if (titleHasValue.ContainsKey(type))
        {
            ret = titleHasValue[type];
        }
        else
        {
            
            var dicData = TableManager.Instance.TitleAbils[(int)type];

            for (int i = 0; i < dicData.Count; i++)
            {
                if (ServerData.titleServerTable.TableDatas[dicData[i].Stringid].clearFlag.Value == 0) continue;

                if (dicData[i].Id == ServerData.equipmentTable.TableDatas[EquipmentTable.TitleSelectId].Value)
                {
                    ret += dicData[i].Abilvalue1 * GameBalance.TitleEquipAddPer;
                }
                else
                {
                    ret += dicData[i].Abilvalue1;
                }
            }

            titleHasValue.Add(type, ret);
        }


        return ret;
    }
    public static float GetTitleLevelAbilValue(StatusType type)
    {
        float ret = 0f;

        if (titleHasValue.ContainsKey(type))
        {
            ret = titleHasValue[type];
        }
        else
        {
            
            var tableData = TableManager.Instance.titleLevel.dataArray;
            var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleLevel).Value;
            for (int i = 0; i < tableData.Length; i++)
            {
                //현재 레벨 초과시 break;
                if (currentLevel < i) break;
                
                ret += tableData[i].Abilvalue1;
            }

            titleHasValue.Add(type, ret);
        }


        return ret;
    }

    public static float GetTitleStageAbilValue(StatusType type)
    {
        float ret = 0f;

        if (titleHasValue.ContainsKey(type))
        {
            ret = titleHasValue[type];
        }
        else
        {
            
            var tableData = TableManager.Instance.titleStage.dataArray;
            var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleStage).Value;
            for (int i = 0; i < tableData.Length; i++)
            {
                //현재 레벨 초과시 break;
                if (currentLevel < i) break;
                
                ret += tableData[i].Abilvalue1;
            }

            titleHasValue.Add(type, ret);
        }


        return ret;
    }

    public static float GetTitleWeaponAbilValue(StatusType type)
    {
        float ret = 0f;

        if (titleHasValue.ContainsKey(type))
        {
            ret = titleHasValue[type];
        }
        else
        {
            
            var tableData = TableManager.Instance.titleWeapon.dataArray;
            var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleWeapon).Value;
            for (int i = 0; i < tableData.Length; i++)
            {
                //현재 레벨 초과시 break;
                if (currentLevel < i) break;
                
                ret += tableData[i].Abilvalue1;
            }

            titleHasValue.Add(type, ret);
        }


        return ret;
    }

    public static float GetTitleMagicBookAbilValue(StatusType type)
    {
        float ret = 0f;

        if (titleHasValue.ContainsKey(type))
        {
            ret = titleHasValue[type];
        }
        else
        {
            
            var tableData = TableManager.Instance.titleMagicBook.dataArray;
            var currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.titleMagicBook).Value;
            for (int i = 0; i < tableData.Length; i++)
            {
                //현재 레벨 초과시 break;
                if (currentLevel < i) break;
                
                ret += tableData[i].Abilvalue1;
            }

            titleHasValue.Add(type, ret);
        }


        return ret;
    }

    public static float GetHotTimeBuffEffect(StatusType statusType)
    {
        if (ServerData.userInfoTable.IsWeekend() == false)
        {
            float ret = 0f;

            if (ServerData.userInfoTable.IsHotTime() == false) return 0f;

            if (statusType == StatusType.GoldGainPer)
            {
                ret = GameBalance.HotTime_Gold;
            }
            else if (statusType == StatusType.ExpGainPer)
            {
                ret = GameBalance.HotTime_Exp;
            }
            else if (statusType == StatusType.MagicStoneAddPer)
            {
                ret = GameBalance.HotTime_GrowthStone;
            }
            else if (statusType == StatusType.MarbleAddPer)
            {
                ret = GameBalance.HotTime_Marble;
            }

            return ret;
        }
        else
        {
            float ret = 0f;

            if (ServerData.userInfoTable.IsHotTime() == false) return 0f;

            if (statusType == StatusType.GoldGainPer)
            {
                ret = GameBalance.HotTime_Gold_Weekend;
            }
            else if (statusType == StatusType.ExpGainPer)
            {
                ret = GameBalance.HotTime_Exp_Weekend;
            }
            else if (statusType == StatusType.MagicStoneAddPer)
            {
                ret = GameBalance.HotTime_GrowthStone_Weekend;
            }
            else if (statusType == StatusType.MarbleAddPer)
            {
                ret = GameBalance.HotTime_Marble_Weekend;
            }

            return ret;
        }
    }

    public static float GetHotTimeEventBuffEffect(StatusType statusType)
    {

        float ret = 0f;

        if (ServerData.userInfoTable.IsHotTimeEvent() == false) return 0f;

        if (statusType == StatusType.GoldGainPer)
        {
            ret = GameBalance.HotTimeEvent_Gold;
            if (ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0)
            {
                ret += GameBalance.HotTimeEvent_Ad_Gold;
            }
        }
        else if (statusType == StatusType.ExpGainPer)
        {
            ret = GameBalance.HotTimeEvent_Exp;
            if (ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0)
            {
                ret += GameBalance.HotTimeEvent_Ad_Exp;
            }
        }
        else if (statusType == StatusType.MagicStoneAddPer)
        {
            ret = GameBalance.HotTimeEvent_GrowthStone;
            if (ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0)
            {
                ret += GameBalance.HotTimeEvent_Ad_GrowthStone;
            }
        }
        else if (statusType == StatusType.MarbleAddPer)
        {
            ret = GameBalance.HotTimeEvent_Marble;
            if (ServerData.iapServerTable.TableDatas[UiColdSeasonPassBuyButton.seasonPassKey].buyCount.Value > 0)
            {
                ret += GameBalance.HotTimeEvent_Ad_Marble;
            }
        }

        return ret;
    }

    private static Dictionary<StatusType, float> sinsuHasValue = new Dictionary<StatusType, float>();

    private static void ResetSinsuBookHas()
    {
        sinsuHasValue.Clear();
    }

    public static float GetSinsuEquipEffect(StatusType statusType)
    {
        float ret = 0f;

        if (sinsuHasValue.ContainsKey(statusType))
        {
            ret = sinsuHasValue[statusType];
        }
        else
        {
            var tableDatas = TableManager.Instance.PetEquipment.dataArray;

            int petEquipLevel = ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value;

            for (int i = 0; i < tableDatas.Length; i++)
            {
                var serverData = ServerData.petEquipmentServerTable.TableDatas[tableDatas[i].Stringid];

                if (serverData.hasAbil.Value == 0) continue;

                if (tableDatas[i].Abiltype1 == (int)statusType)
                {
                    ret += (tableDatas[i].Abilvalue1 + serverData.level.Value * tableDatas[i].Abiladdvalue1 +
                            tableDatas[i].Leveladdvalue1 * petEquipLevel);
                }

                if (tableDatas[i].Abiltype2 == (int)statusType)
                {
                    ret += (tableDatas[i].Abilvalue2 + serverData.level.Value * tableDatas[i].Abiladdvalue2 +
                            tableDatas[i].Leveladdvalue2 * petEquipLevel);
                }
            }

            sinsuHasValue.Add(statusType, ret);
        }


        if (ActiveSmithValue(statusType))
        {
            ret = ret * GetSmithValue(StatusType.PetEquipHasUp);
        }
        else
        {
        }

        return ret;
    }

    private static bool ActiveSmithValue(StatusType statustype)
    {
        return statustype != StatusType.Damdecrease && statustype != StatusType.SuperCritical1Prob &&
               statustype != StatusType.ExpGainPer;
    }

    public static float GetRelicHasEffect(StatusType statusType)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.RelicTable.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            var serverData = ServerData.relicServerTable.TableDatas[tableDatas[i].Stringid];

            if (serverData.level.Value == 0) continue;

            if (tableDatas[i].Abiltype != (int)statusType) continue;

            ret += serverData.level.Value * tableDatas[i].Abilvalue;
        }

        if (statusType == StatusType.Hp ||
            statusType == StatusType.HpAddPer ||
            statusType == StatusType.Damdecrease
           )
        {
            return ret;
        }
        else
        {
            return ret * GetSpecialAbilRing();
        }
    }

    private static Dictionary<StatusType, float> stageRelicValue = new Dictionary<StatusType, float>();

    private static void ResetStageRelicHas()
    {
        stageRelicValue.Clear();
    }

    public const float divideNum = 500f;
    public const float divideAbilValue = 0.5f;

    public static float GetStageRelicHasEffect(StatusType statusType)
    {
        float ret = 0f;

        if (stageRelicValue.ContainsKey(statusType))
        {
            ret = stageRelicValue[statusType];
        }
        else
        {
            float totalLevel = ServerData.stageRelicServerTable.GetTotalStageRelicLevel();

            var tableDatas = TableManager.Instance.StageRelic.dataArray;

            for (int i = 0; i < tableDatas.Length; i++)
            {
                var serverData = ServerData.stageRelicServerTable.TableDatas[tableDatas[i].Stringid];

                if (tableDatas[i].Abiltype != (int)statusType) continue;

                if (serverData.level.Value == 0 && tableDatas[i].Istotalskill == false) continue;

                if (tableDatas[i].Istotalskill)
                {
                    //언락 체크
                    if (ServerData.goodsTable.GetTableData(tableDatas[i].Requiregoods).Value >=
                        tableDatas[i].Requiregoodsvalue)
                    {
                        ret += totalLevel * tableDatas[i].Abilvalue;
                    }
                }
                else
                {
                    ret += serverData.level.Value * tableDatas[i].Abilvalue;
                }
            }

            stageRelicValue.Add(statusType, ret);
        }

        ret *= GetStageAddValue();

        return ret;
    }

    public static float GetStageAddValue()
    {
        int currentStage = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.topClearStageId].Value;

        float divide = (int)(currentStage / divideNum);

        return (1 + divide * divideAbilValue);
    }

    public static float GetSonAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.SonAbil.dataArray;

        int currentLevel = ServerData.statusTable.GetTableData(StatusTable.Son_Level).Value + addLevel;

        currentLevel += (int)ServerData.userInfoTable.TableDatas[UserInfoTable.sonCloneClear].Value *
                        GameBalance.sonCloneAddValue;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }

        ret = ret * (1 + GetSonAbilPlusValue() + GetSonTransPlusValue());

        return ret;
    }

    public static float GetHellAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.hellAbilBase.dataArray;

        int currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.Hel).Value + addLevel;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * (tableDatas[i].Abiladdvalue + GetHellFireHasAddValue());
        }


        float hellPowerAddValue = GetHellPowerAddValue();
        float hellTransAddValue = GetHelTransPlusValue();
        ret = (ret * (1 + hellPowerAddValue)) * hellTransAddValue;

        return ret;
    }

    public static float GetChunAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        var ret = 0f;

        var tableDatas = TableManager.Instance.chunAbilBase.dataArray;

        var currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value + addLevel;

        for (var i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            var calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }

        if (statusType == StatusType.SuperCritical4DamPer)
        {
            if (IsChunFlowerDamageEnhance())
            {
                ret += 0.000015f * currentLevel;
            }

            ret += GetChunFlowerHasAddValue() * currentLevel;
        }

        var chunTransAddValue = GetChunTransPlusValue();
        ret = (ret * chunTransAddValue);

        return ret;
    }

    public static float GetDokebiFireAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        if (ServerData.statusTable.GetTableData(StatusTable.Level).Value < 500000) return 0f;

        float ret = 0f;

        var tableDatas = TableManager.Instance.dokebiAbilBase.dataArray;

        int currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.DokebiFire).Value + addLevel;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }

        if (statusType == StatusType.SuperCritical5DamPer)
        {
            ret += GetDokebiFireHasAddValue() * currentLevel;
        }
        var dokebiTransAddValue = GetDokebiTransPlusValue();
        ret = (ret * dokebiTransAddValue);
        
        return ret + ret * GetDokebiFireEnhanceAbilPlusValue();
    }

    public static float GetSumiFireAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        if (ServerData.statusTable.GetTableData(StatusTable.Level).Value < 1000000) return 0f;

        float ret = 0f;

        var tableDatas = TableManager.Instance.sumiAbilBase.dataArray;

        int currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.SumiFire).Value + addLevel;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }

        if (statusType == StatusType.SuperCritical7DamPer)
        {
            ret += GetSumiFireHasAddValue() * currentLevel;
        }

        return ret;
    }

    public static float GetTresureAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        if (ServerData.statusTable.GetTableData(StatusTable.Level).Value < 2000000) return 0f;

        float ret = 0f;

        var tableDatas = TableManager.Instance.tresureAbilBase.dataArray;

        int currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.Tresure).Value + addLevel;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }
        if (statusType == StatusType.SuperCritical10DamPer)
        {
            ret += GetTreasureHasAddValue() * currentLevel;
        }
        return ret;
    }
    public static float GetDarkTreasureAbilHasEffect(StatusType statusType, int addLevel = 0)
    {
        if (ServerData.statusTable.GetTableData(StatusTable.Level).Value < 2000000) return 0f;

        float ret = 0f;

        var tableDatas = TableManager.Instance.DarkTreasureAbilBase.dataArray;

        int currentLevel = (int)ServerData.goodsTable.GetTableData(GoodsTable.DarkTreasure).Value + addLevel;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if (currentLevel < tableDatas[i].Unlocklevel) continue;
            if (statusType != (StatusType)tableDatas[i].Abiltype) continue;

            int calculatedLevel = currentLevel - tableDatas[i].Unlocklevel;

            ret += tableDatas[i].Abilvalue + calculatedLevel * tableDatas[i].Abiladdvalue;
        }

        return ret;
    }

    public static float GetSumiTowerEffect(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.sumisanTowerTable.dataArray;

        int currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx4).Value;

        if (currentLevel == 0)
        {
            return 0f;
        }

        for (int i = 0; i < currentLevel; i++)
        {
            if ((StatusType)tableDatas[i].Rewardtype == statusType)
            {
                ret += tableDatas[i].Rewardvalue;
            }
        }

        return ret;
    }

    public static float GetGyungRockEffect(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.gyungRockTowerTable.dataArray;

        int currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx5).Value;

        if (currentLevel == 0)
        {
            return 0f;
        }

        for (int i = 0; i < currentLevel; i++)
        {
            if ((StatusType)tableDatas[i].Rewardtype == statusType)
            {
                ret += tableDatas[i].Rewardvalue;
            }
        }

        return ret;
    }
    
    public static float GetGyungRockEffect2(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.gyungRockTowerTable2.dataArray;

        int currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorIdx7).Value;

        if (currentLevel == 0)
        {
            return 0f;
        }

        for (int i = 0; i < currentLevel; i++)
        {
            if ((StatusType)tableDatas[i].Rewardtype == statusType)
            {
                ret += tableDatas[i].Rewardvalue;
            }
        }

        return ret;
    }

    public static float GetFoxFireEffect(StatusType statusType, int addLevel = 0)
    {
        float ret = 0f;

        var tableDatas = TableManager.Instance.FoxFire.dataArray;

        int currentLevel = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.foxFireIdx).Value;

        if (currentLevel == -1)
        {
            return 0f;
        }

        for (int i = 0; i < currentLevel+1; i++)
        {
            if ((StatusType)tableDatas[i].Abil_Type == statusType)
            {
                ret += tableDatas[i].Abil_Value;
            }
        }

        return ret;
    }

    public static float GetSmithValue(StatusType statusType)
    {
        int currentExp = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.smithExp].Value;

        currentExp += (int)ServerData.userInfoTable.TableDatas[UserInfoTable.smithTreeClear].Value *
                      (int)GameBalance.smithTreeAddValue;

        if (statusType == StatusType.growthStoneUp)
        {
            if (currentExp < 1000)
            {
                return 0;
            }
            else
            {
                currentExp = Mathf.Min(currentExp, 60000);

                int divide = currentExp / 1000;

                return (1 + (divide)) * 10;
            }
        }
        else if (statusType == StatusType.WeaponHasUp ||
                 statusType == StatusType.NorigaeHasUp ||
                 statusType == StatusType.PetEquipHasUp)
        {
            return 1f + (currentExp / 2500) * 0.05f;
        }
        else if (statusType == StatusType.PetEquipProbUp)
        {
            currentExp = Mathf.Min(currentExp, 50000);

            int divide = currentExp / 10000;

            return divide * 10;
        }

        return 0f;
    }

    public static float GetFeelMulAddDam()
    {
        return ServerData.statusTable.GetTableData(StatusTable.FeelMul).Value * 0.1f;
    }

    public static float GetLeeMuGiAddDam()
    {
        return ServerData.statusTable.GetTableData(StatusTable.LeeMuGi).Value * 0.2f;
    }

    public static string asuraKey0 = "a0";
    public static string asuraKey1 = "a1";
    public static string asuraKey2 = "a2";
    public static string asuraKey3 = "a3";
    public static string asuraKey4 = "a4";
    public static string asuraKey5 = "a5";

    public static string indraKey0 = "i0";
    public static string indraKey1 = "i1";
    public static string indraKey2 = "i2";

    public static string orochi0 = "or0";
    public static string orochi1 = "or1";


    public static ObscuredFloat asura0Value = 15000f;
    public static ObscuredFloat asura1Value = 25000f;
    public static ObscuredFloat asura2Value = 300f;
    public static ObscuredFloat asura3Value = 0.5f;
    public static ObscuredFloat asura4Value = 0.8f;
    public static ObscuredFloat asura5Value = 1.0f;

    public static ObscuredFloat indra0Value = 50000;
    public static ObscuredFloat indra1Value = 70000;
    public static ObscuredFloat indra2Value = 0.001f;

    public static ObscuredFloat orochi0Value = 0.001f;
    public static ObscuredFloat orochi1Value = 0.002f;

    public static ObscuredFloat gumihoValue0 = 5000;
    public static ObscuredFloat gumihoValue1 = 10000;
    public static ObscuredFloat gumihoValue2 = 15000;
    public static ObscuredFloat gumihoValue3 = 30000;
    public static ObscuredFloat gumihoValue4 = 40000;
    public static ObscuredFloat gumihoValue5 = 50000;
    public static ObscuredFloat gumihoValue6 = 70000;
    public static ObscuredFloat gumihoValue7 = 90000;
    public static ObscuredFloat gumihoValue8 = 120000;


    public static float GetGumihoAbil()
    {
        float ret = 0f;

        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho0).Value == 1 ? gumihoValue0 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho1).Value == 1 ? gumihoValue1 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho2).Value == 1 ? gumihoValue2 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho3).Value == 1 ? gumihoValue3 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho4).Value == 1 ? gumihoValue4 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho5).Value == 1 ? gumihoValue5 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho6).Value == 1 ? gumihoValue6 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho7).Value == 1 ? gumihoValue7 : 0;
        ret += ServerData.goodsTable.GetTableData(GoodsTable.gumiho8).Value == 1 ? gumihoValue8 : 0;

        return ret;
    }

    public static float GetAsuraAbilValue(StatusType type)
    {
        switch (type)
        {
            case StatusType.AttackAddPer:
            {
                if (ServerData.goodsTable.GetTableData(asuraKey0).Value == 0)
                {
                    return 0f;
                }

                return asura0Value;
            }
                break;
            case StatusType.IgnoreDefense:
            {
                if (ServerData.goodsTable.GetTableData(asuraKey1).Value == 0)
                {
                    return 0f;
                }

                return asura1Value;
            }
                break;
            case StatusType.SuperCritical1DamPer:
            {
                if (ServerData.goodsTable.GetTableData(asuraKey2).Value == 0)
                {
                    return 0f;
                }

                return asura2Value;
            }
                break;
            case StatusType.SuperCritical2DamPer:
            {
                float ret = 0f;

                if (ServerData.goodsTable.GetTableData(asuraKey3).Value == 0)
                {
                }
                else
                {
                    ret += asura3Value;
                }

                if (ServerData.goodsTable.GetTableData(asuraKey4).Value == 0)
                {
                }
                else
                {
                    ret += asura4Value;
                }

                if (ServerData.goodsTable.GetTableData(asuraKey5).Value == 0)
                {
                }
                else
                {
                    ret += asura5Value;
                }

                return ret;
            }
                break;
        }

        return 0f;
    }

    public static float GetIndraAbilValue(StatusType type)
    {
        switch (type)
        {
            case StatusType.IgnoreDefense:
            {
                float ret = 0f;

                if (ServerData.goodsTable.GetTableData(indraKey0).Value != 0)
                {
                    ret += indra0Value;
                }

                if (ServerData.goodsTable.GetTableData(indraKey1).Value != 0)
                {
                    ret += indra1Value;
                }

                return ret;
            }
                break;

            case StatusType.PenetrateDefense:
            {
                if (ServerData.goodsTable.GetTableData(indraKey2).Value == 0)
                {
                    return 0f;
                }

                return indra2Value;
            }
                break;
        }

        return 0f;
    }

    public static float GetOrochiAbilValue(StatusType type)
    {
        switch (type)
        {
            case StatusType.PenetrateDefense:
            {
                float ret = 0f;

                if (ServerData.goodsTable.GetTableData(orochi0).Value != 0)
                {
                    ret += orochi0Value;
                }

                if (ServerData.goodsTable.GetTableData(orochi1).Value != 0)
                {
                    ret += orochi1Value;
                }

                return ret;
            }
                break;
        }

        return 0f;
    }

    public static float GetGuildPetEffect(StatusType type)
    {
        int petLevel = GuildManager.Instance.guildPetExp.Value;

        switch (type)
        {
            case StatusType.AttackAddPer:
            {
                return petLevel * 0.1f;
            }
                break;
            case StatusType.ExpGainPer:
            {
                return petLevel * 0.01f;
            }
                break;
            case StatusType.GoldGainPer:
            {
                return petLevel * 0.01f;
            }
                break;
        }

        return 0f;
    }

    private static string adukCostumeKey = "costume26";
    private static string leeMuGiCostumeKey = "costume27";
    private static string nataCostumeKey = "costume35";
    private static string foxCostumeKey = "costume40";

    private static string hellCostumeKey0 = "costume43";
    private static string hellCostumeKey1 = "costume44";

    public static float DecreaseBossHp()
    {
        float ret = 0f;

        if (ServerData.costumeServerTable.TableDatas[adukCostumeKey].hasCostume.Value == true)
        {
            ret += 0.05f;
        }

        if (ServerData.costumeServerTable.TableDatas[leeMuGiCostumeKey].hasCostume.Value == true)
        {
            ret += 0.05f;
        }

        if (ServerData.costumeServerTable.TableDatas[nataCostumeKey].hasCostume.Value == true)
        {
            ret += 0.1f;
        }

        if (ServerData.costumeServerTable.TableDatas[foxCostumeKey].hasCostume.Value == true)
        {
            ret += 0.1f;
        }

        //
        if (ServerData.costumeServerTable.TableDatas[hellCostumeKey0].hasCostume.Value == true)
        {
            ret += 0.05f;
        }

        if (ServerData.costumeServerTable.TableDatas[hellCostumeKey1].hasCostume.Value == true)
        {
            ret += 0.1f;
        }

        if (IsChunBossHpDec())
        {
            ret += 0.05f;
        }

        return ret;
    }

    public static float GetGoldAbilAddRatio()
    {
        float ret = 1f;

        if (ServerData.equipmentTable.TableDatas[EquipmentTable.MagicBook].Value >= 0)
        {
            ret = (float)TableManager.Instance.MagicBookTable
                .dataArray[ServerData.equipmentTable.TableDatas[EquipmentTable.MagicBook].Value].Goldabilratio;
        }
        else
        {
            ret = 1f;
        }

        return ret;
    }

    public static float GetNorigaeSoulGradeValue()
    {
        float ret = 1f;

        int norigaeSoulGrade = PlayerStats.GetNorigaeSoulGrade();

        if (norigaeSoulGrade != -1 && norigaeSoulGrade < TableManager.Instance.norigaeJewel.dataArray.Length)
        {
            float abilValue = TableManager.Instance.norigaeJewel.dataArray[norigaeSoulGrade].Abilvalue0;

            ret = abilValue;
        }

        return ret * GetNorigaeSoulTransPlusValue();
    }

    public static float GetSpecialAbilRatio()
    {
        if (ServerData.equipmentTable.TableDatas[EquipmentTable.Weapon].Value >= 0)
        {
            return (float)TableManager.Instance.WeaponTable
                .dataArray[ServerData.equipmentTable.TableDatas[EquipmentTable.Weapon].Value].Specialadd;
        }
        else
        {
            return 1f;
        }
    }

    public static float GetSpecialAbilRing()
    {
        if (ServerData.equipmentTable.TableDatas[EquipmentTable.SoulRing].Value >= 0)
        {
            return (float)TableManager.Instance.NewGachaTable
                .dataArray[ServerData.equipmentTable.TableDatas[EquipmentTable.SoulRing].Value].Specialadd;
        }
        else
        {
            return 1f;
        }
    }

    public static float GetMaskAttackAddPerDam()
    {
        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.FoxMask].Value;
        if (equipId == -1) return 0f;

        return (float)TableManager.Instance.FoxMask.dataArray[equipId].Abilvalue * (1 + GetFoxMaskAbilPlusValue());
    }

    public static float GetDokebiHornCritical5Add()
    {
        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.DokebiHorn].Value;
        if (equipId == -1) return 0f;

        return (float)TableManager.Instance.DokebiHorn.dataArray[equipId].Abilvalue;
    }

    public static float GetCaveBeltAttackAdd()
    {
        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.CaveBelt].Value;
        if (equipId == -1) return 0f;

        return (float)TableManager.Instance.CaveBelt.dataArray[equipId].Abilvalue * (1 + GetTwoCaveBeltAbilPlusValue());
    }


    public static void ResetAbilDic()
    {
        PlayerStats.ResetMagicBookHas();
        PlayerStats.ResetSinsuBookHas();
        PetServerTable.ResetPetHas();
        PlayerStats.ResetTitleHas();
        PlayerStats.ResetStageRelicHas();
    }

    public static int GetSusanoGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.susanoTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.susanoScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetNorigaeSoulGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.norigaeJewel.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.norigaeScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetGradeTestGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.gradeTestTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.gradeScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetRingGrade()
    {
        return ServerData.equipmentTable.TableDatas[EquipmentTable.SoulRing].Value;
    }

    public static int GetYumGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.yumTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.yumScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetOkGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.okTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.okScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetDoGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.doTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.doScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetSumiGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.sumiTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.sumiScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }
    public static int GetThiefKingGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.ThiefTable.dataArray;

        var score = ServerData.userInfoTable.TableDatas[UserInfoTable.thiefScore].Value *
                    GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (score >= tableData[i].Score)
            {
                grade = i;
            }
        }

        return grade;
    }

    public static int GetBlackGrade()
    {
        int grade = -1;

        var tableData = TableManager.Instance.sasinsuTable.dataArray;

        var score = ServerData.sasinsuServerTable.TableDatas["b0"].score.Value * GameBalance.BossScoreConvertToOrigin;

        for (int i = 0; i < tableData[0].Score.Length; i++)
        {
            if (score >= tableData[0].Score[i])
            {
                grade = i;
            }
        }

        return grade;
    }

    public static float GetSusanoAbil(StatusType type)
    {
        int grade = GetSusanoGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.susanoTable.dataArray[grade];

        if (type == StatusType.CriticalDam)
        {
            return tableData.Abilvalue0 + tableData.Abilvalue0 * GetSusanoUpgradeAbilPlusValue();
        }
        else if (type == StatusType.PenetrateDefense)
        {
            return tableData.Abilvalue1;
        }

        return 0f;
    }

    public static float GetYumAbil(StatusType type)
    {
        int grade = GetYumGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.yumTable.dataArray[grade];
        //지옥베기
        if (type == StatusType.SuperCritical3DamPer)
        {
            return tableData.Abilvalue0;
        }
        //else if (type == StatusType.PenetrateDefense)
        //{

        //    return tableData.Abilvalue1;

        //}

        return 0f;
    }

    public static float GetOkAbil(StatusType type)
    {
        int grade = GetOkGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.okTable.dataArray[grade];

        if (type == StatusType.SuperCritical4DamPer)
        {
            return tableData.Abilvalue0;
        }
        //else if (type == StatusType.PenetrateDefense)
        //{

        //    return tableData.Abilvalue1;

        //}

        return 0f;
    }

    public static float GetDoAbil(StatusType type)
    {
        int grade = GetDoGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.doTable.dataArray[grade];

        if (type == StatusType.SuperCritical5DamPer)
        {
            return tableData.Abilvalue0;
        }
        //else if (type == StatusType.PenetrateDefense)
        //{

        //    return tableData.Abilvalue1;

        //}

        return 0f;
    }

    public static float GetSuAbil(StatusType type)
    {
        int grade = GetSumiGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.sumiTable.dataArray[grade];

        if (type == StatusType.SuperCritical7DamPer)
        {
            return tableData.Abilvalue0;
        }

        return 0f;
    }
    public static float GetThiefAbil(StatusType type)
    {
        int grade = GetThiefKingGrade();

        if (grade == -1) return 0f;

        var tableData = TableManager.Instance.ThiefTable.dataArray[grade];

        if (type == StatusType.SuperCritical10DamPer)
        {
            return tableData.Abilvalue0;
        }

        return 0f;
    }

    public static float yeoRaeMarbleValue = 0.1f;

    public static float GetSonAbilPlusValue()
    {
        return yeoRaeMarbleValue * ServerData.goodsTable.GetTableData(GoodsTable.Ym).Value;
    }

    public static float SonTransAddValue = 10f;

    public static float GetSonTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateSon).Value > 0)
        {
            return SonTransAddValue;
        }
        else
        {
            return 0f;
        }
    }

    public static float HelTransAddValue = 2f;

    public static float GetHelTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateHel).Value > 0)
        {
            return HelTransAddValue;
        }
        else
        {
            return 1f;
        }
    }

    public static float ChunTransAddValue = 2f;

    private static float GetChunTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateChun).Value > 0)
        {
            return ChunTransAddValue;
        }
        return 1f;
    }

    private static float GetDokebiTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateDokebiFire).Value > 0)
        {
            return GameBalance.dokebiGraduatePlusValue;
        }
        return 1f;
    }

    public static float GetGumSoulTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateGumSoul).Value > 0)
        {
            return GameBalance.GumSoulGraduatePlusValue;
        }
        else
        {
            return 1f;
        }
    }
    public static float GetNorigaeSoulTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateNorigaeSoul).Value > 0)
        {
            return GameBalance.NorigaeSoulGraduatePlusValue;
        }
        else
        {
            return 1f;
        }
    }
    public static float GetEvilSeedTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateEvilSeed).Value > 0)
        {
            return GameBalance.EvilSeedGraduatePlusValue;
        }
        else
        {
            return 1f;
        }
    }
    public static float GetGhostTreeTransPlusValue()
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.graduateGhostTree).Value > 0)
        {
            return GameBalance.GhostTreeGraduatePlusValue;
        }
        else
        {
            return 1f;
        }
    }

    public static float foxMaskPartialValue = 0.02f;

    public static float GetFoxMaskAbilPlusValue()
    {
        return (foxMaskPartialValue * ServerData.goodsTable.GetTableData(GoodsTable.FoxMaskPartial).Value)*GetGhostTreeTransPlusValue();
    }

    public static float susanoUpgradelValue = 0.025f;

    public static float GetSusanoUpgradeAbilPlusValue()
    {
        return (susanoUpgradelValue * ServerData.goodsTable.GetTableData(GoodsTable.SusanoTreasure).Value) *
               GetEvilSeedTransPlusValue();
    }

    public static float dokebiUpgradeValue = 0.0025f;

    public static float GetDokebiFireEnhanceAbilPlusValue()
    {
        return dokebiUpgradeValue * ServerData.goodsTable.GetTableData(GoodsTable.DokebiFireEnhance).Value;
    } //

    public static string caveBossKey = "b91";

    public static float GetTwoCaveBeltAbilPlusValue()
    {
        if (string.IsNullOrEmpty(ServerData.bossServerTable.TableDatas[caveBossKey].score.Value))
        {
            return 0f;
        }

        int.TryParse(ServerData.bossServerTable.TableDatas[caveBossKey].score.Value, out var score);
        return TableManager.Instance.twoCave.dataArray[score - 1].Beltaddvalue;
    }

    public static float sahyungUpgradeValue = 0.03f;
    public static float sinSuUpgradeValue = 0.03f;

    public static float GetSahyungTreasureAbilPlusValue()
    {
        return sahyungUpgradeValue * ServerData.goodsTable.GetTableData(GoodsTable.SahyungTreasure).Value;
    } 
    
    public static float GetSinsuTreasureAbilPlusValue()
    {
        return sinSuUpgradeValue * ServerData.goodsTable.GetTableData(GoodsTable.SinsuMarble).Value;
    } 
    
    
    //
    //public static float GetTwoCaveExpAbilPlusValue()
    //{
    //    string bossKey = "b91";
    //    if (string.IsNullOrEmpty(ServerData.bossServerTable.TableDatas[bossKey].score.Value))
    //    {
    //        return 0f;
    //    }
    //    int.TryParse(ServerData.bossServerTable.TableDatas[bossKey].score.Value, out var score);
    //    return TableManager.Instance.twoCave.dataArray[score-1].Expaddvalue;
    //}


    public static int GetCurrentFoxCupIdx()
    {
        var tableData = TableManager.Instance.foxCup.dataArray;
        int currentPetEquipGrade = ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value;
        int idx = -1;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (currentPetEquipGrade >= tableData[i].Require)
            {
                idx = i;
            }
        }

        return idx;
    }
    public static int GetCurrentWolfRingIdx()
    {
        var tableData = TableManager.Instance.BlackWolfRing.dataArray;
        int currentPetEquipGrade = ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value;
        int idx = -1;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (currentPetEquipGrade >= tableData[i].Require)
            {
                idx = i;
            }
        }

        return idx;
    }

    public static int GetCurrentDragonIdx()
    {
        var tableData = TableManager.Instance.dragonBall.dataArray;
        int currentPetEquipGrade = ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value;
        int idx = -1;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (currentPetEquipGrade >= tableData[i].Require)
            {
                idx = i;
            }
        }

        return idx;
    }

    public static int GetCurrentGumgiIdx()
    {
        int idx = ServerData.equipmentTable.TableDatas[EquipmentTable.WeaponEnhance].Value;

        return idx;
    }

    public static float GetDragonBallAbil0Value()
    {
        int idx = GetCurrentDragonIdx();

        if (idx == -1) return 0f;

        return TableManager.Instance.dragonBall.dataArray[idx].Abilvalue0;
    }

    public static float GetDragonBallAbil1Value()
    {
        int idx = GetCurrentDragonIdx();

        if (idx == -1) return 0f;

        return TableManager.Instance.dragonBall.dataArray[idx].Abilvalue1;
    }

    //
    public static float GetFoxCupAbilValue(int idx, int abilIdx)
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getFoxCup).Value == 0) return 0f;
        if (idx == -1) return 0f;

        switch (abilIdx)
        {
            case 0:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue0;
            }
                break;
            case 1:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue1;
            }
                break;
            case 2:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue2;
            }
                break;
            case 3:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue3;
            }
                break;
            case 4:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue4;
            }
                break;
            case 5:
            {
                return TableManager.Instance.foxCup.dataArray[idx].Abilvalue5;
            }
                break;
        }

        return 0f;
    }

    public static float GetWolfRingAbilValue(int idx, int abilIdx)
    {
        if (ServerData.userInfoTable.GetTableData(UserInfoTable.getWolfRing).Value == 0) return 0f;
        if (idx == -1) return 0f;

        switch (abilIdx)
        {
            case 0:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue0;
            }
                break;
            case 1:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue1;
            }
                break;
            case 2:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue2;
            }
                break;
            case 3:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue3;
            }
                break;
            case 4:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue4;
            }
                break;
            case 5:
            {
                return TableManager.Instance.BlackWolfRing.dataArray[idx].Abilvalue5;
            }
                break;
        }

        return 0f;
    }

    //

    public const float HellRelicAbilValue = 0.5f;
    public const int HellRelicAbilDivide = 100;

    public static float GetHellRelicAbilValue()
    {
        int kt =
            (int)(ServerData.userInfoTable.TableDatas[UserInfoTable.hellRelicKillCount].Value / HellRelicAbilDivide);
        return kt * HellRelicAbilValue;
    }

    public const float gumgiSoulAbilValue = 0.005f;
    public const int gumgiSoulDivideNum = 100;

    public static float GetGumgiAbilAddValue()
    {
        int kt = (int)(ServerData.userInfoTable.TableDatas[UserInfoTable.gumGiSoulClear].Value / gumgiSoulDivideNum);

        float addValue = ServerData.goodsTable.GetTableData(GoodsTable.Fw).Value;

        //천상계 마크
        float addValue2 = 0;
        if (PlayerStats.IsChunFlowerGumgiEnhance())
        {
            addValue2 = TableManager.Instance.chunMarkAbil.dataArray[6].Abiladdvalue;
        }

        return (kt * gumgiSoulAbilValue + (addValue * 0.01f) + addValue2) * GetGumSoulTransPlusValue();
    }

    public static bool IsChunQuickMoveAwake()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[0].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static bool IsChunAttackSpeedAwake()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[1].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static bool IsChunBossHpDec()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[2].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static bool IsChunMonsterSpawnAdd()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[3].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static bool IsChunFlowerDamageEnhance()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[4].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static bool IsChunFlowerCostumeEnhance()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[5].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static float IsCostumeCollectionEnhance()
    {
        float ret = 1f;

        var costumeNum = ServerData.costumeServerTable.GetCostumeHasAmount();
        var tableData = TableManager.Instance.costumeCollection.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (tableData[i].Require <= costumeNum)
            {
                ret = tableData[i].Plusvalue;
            }
            else
            {
                break;
            }
        }

        return ret;
    }

    public static bool IsChunFlowerGumgiEnhance()
    {
        var chunFlowerNum = ServerData.goodsTable.GetTableData(GoodsTable.Cw).Value;
        var requireFlower = TableManager.Instance.chunMarkAbil.dataArray[6].Requirespeicalabilflower;
        return chunFlowerNum >= requireFlower;
    }

    public static int GetPetHomeHasCount()
    {
        int ret = 0;

        var tableData = TableManager.Instance.PetTable.dataArray;

        for (int i = 8; i < tableData.Length; i++)
        {
            if (ServerData.petTable.TableDatas[tableData[i].Stringid].hasItem.Value == 1)
            {
                ret++;
            }
        }

        return ret;
    }


    public static float GetPetHomeAbilValue(StatusType type)
    {
        float ret = 0f;

        int petHomeHasCount = GetPetHomeHasCount();

        var tableData = TableManager.Instance.petHome.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            if (petHomeHasCount <= i) break;

            if (tableData[i].Abiltype == (int)type)
            {
                ret += tableData[i].Abilvalue;
            }
        }

        return ret;
    }

    public const float hellPowerStoneAddPer = 0.007f;

    public static float GetHellPowerAddValue()
    {
        var goodsNum = ServerData.goodsTable.GetTableData(GoodsTable.HellPowerUp).Value;

        return goodsNum * hellPowerStoneAddPer;
    }

    public static float GetGradeTestAbilValue(StatusType statusType)
    {
        int currentGrade = GetGradeTestGrade();

        if (currentGrade == -1) return 0f;

        var tableData = TableManager.Instance.gradeTestTable.dataArray[currentGrade];

        if (tableData.Abiltype.Length != tableData.Abilvalue.Length) return 0f;

        float ret = 0f;

        for (int i = 0; i < tableData.Abiltype.Length; i++)
        {
            if (tableData.Abiltype[i] == (int)statusType)
            {
                ret += tableData.Abilvalue[i];
            }
        }

        return ret;
    }

    public static float GetRingEquipAbilValue(StatusType type)
    {
        int equipId = ServerData.equipmentTable.TableDatas[EquipmentTable.SoulRing].Value;

        var e = TableManager.Instance.NewGachaData.GetEnumerator();

        float ret = 0f;
        while (e.MoveNext())
        {
            if (e.Current.Value.Id != equipId) continue;
            if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Effectid, out var effectData) ==
                false) continue;

            int currentLevel = ServerData.newGachaServerTable.GetNewGachaData(e.Current.Value.Stringid).level.Value;

            if (effectData.Equipeffecttype1 == (int)type)
            {
                ret += effectData.Equipeffectbase1;
                ret += currentLevel * effectData.Equipeffectvalue1;
            }

            if (effectData.Equipeffecttype2 == (int)type)
            {
                ret += effectData.Equipeffectbase2;
                ret += currentLevel * effectData.Equipeffectvalue2;
            }

            break;
        }

        return ret;
    }

    public static float GetRingHasPercentValue(StatusType type)
    {
        var e = TableManager.Instance.NewGachaData.GetEnumerator();

        float ret = 0f;
        while (e.MoveNext())
        {
            if (TableManager.Instance.WeaponEffectDatas.TryGetValue(e.Current.Value.Effectid, out var effectData) ==
                false) continue;

            var weaponServertable = ServerData.newGachaServerTable.TableDatas[e.Current.Value.Stringid];

            if (weaponServertable.hasItem.Value == 0) continue;

            int currentLevel = ServerData.newGachaServerTable.GetNewGachaData(e.Current.Value.Stringid).level.Value;

            if (effectData.Haseffecttype1 == (int)type)
            {
                ret += effectData.Haseffectbase1;
                ret += currentLevel * effectData.Haseffectvalue1;
            }

            if (effectData.Haseffecttype2 == (int)type)
            {
                ret += effectData.Haseffectbase2;
                ret += currentLevel * effectData.Haseffectvalue2;
            }
        }


        return ret;
    }

    public static float GetChunFlowerHasAddValue()
    {
        float ret = 0f;

        ret += GetSkillHasValue(StatusType.FlowerHasValueUpgrade);

        ret += GetGradeTestAbilValue(StatusType.FlowerHasValueUpgrade);

        ret += GetPassiveSkill2Value(StatusType.FlowerHasValueUpgrade);


        return ret;
    }

    public static float GetHellFireHasAddValue()
    {
        float ret = 0f;

        ret += GetGradeTestAbilValue(StatusType.HellHasValueUpgrade);

        ret += GetPassiveSkill2Value(StatusType.HellHasValueUpgrade);

        return ret;
    }

    public static float GetDokebiFireHasAddValue()
    {
        float ret = 0f;

        ret += GetSkillHasValue(StatusType.DokebiFireHasValueUpgrade);

        ret += GetGradeTestAbilValue(StatusType.DokebiFireHasValueUpgrade);

        ret += GetPassiveSkill2Value(StatusType.DokebiFireHasValueUpgrade);

        return ret;
    }

    public static float GetTreasureHasAddValue()
    {
        float ret = 0f;

        ret += GetSkillHasValue(StatusType.TreasureHasValueUpgrade);

        ret += GetGradeTestAbilValue(StatusType.TreasureHasValueUpgrade);

        return ret;
    }

    public static float GetSumiFireHasAddValue()
    {
        float ret = 0f;

        ret += GetSkillHasValue(StatusType.SumiHasValueUpgrade);

        ret += GetGradeTestAbilValue(StatusType.SumiHasValueUpgrade);

        ret += GetPassiveSkill2Value(StatusType.SumiHasValueUpgrade);
        return ret;
    }

    public static float GetSasinsuStarAddValue()
    {
        float ret = 0f;

        var tableData = TableManager.Instance.sasinsuTable.dataArray;

        for (int i = 0; i < tableData.Length; i++)
        {
            for (int j = 0; j < tableData[i].Score.Length; j++)
            {
                if (tableData[i].Score[j] < ServerData.sasinsuServerTable.TableDatas[$"b{i}"].score.Value)
                {
                    ret += tableData[i].Abilvalue0[j];
                }
            }
        }


        return ret;
    }

    public static float GetGuildTowerChimUpgradeValue()
    {
        return ServerData.goodsTable.TableDatas[GoodsTable.GuildTowerHorn].Value * GameBalance.GuildTowerChimAbilUpValue;
    }
}