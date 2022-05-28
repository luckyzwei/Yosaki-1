﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase
{
    protected Transform playerTr;
    public SkillTableData skillInfo { get; private set; }
    protected PlayerSkillCaster playerSkillCaster;
    protected WaitForSeconds damageApplyInterval = new WaitForSeconds(0.05f);
    public void Initialize(Transform playerTr, SkillTableData skillInfo, PlayerSkillCaster playerSkillCaster)
    {
        this.playerTr = playerTr;
        this.skillInfo = skillInfo;
        this.playerSkillCaster = playerSkillCaster;
    }

    public bool CanUseSkill()
    {
        //mp계산 뒤에서해야됨.실제 엠피 차감해서
        return !SkillCoolTimeManager.HasSkillCooltime(skillInfo.Id) && PlayerStatusController.Instance.IsPlayerDead() == false;
    }
    protected virtual double GetSkillDamage(SkillTableData skillInfo)
    {
        double apDamage = PlayerStats.GetCalculatedAttackPower();

        double skillDamagePer = ServerData.skillServerTable.GetSkillDamagePer(skillInfo.Id);

        return apDamage * skillDamagePer;
    }

    public virtual void UseSkill()
    {
                                            //인드라 X
        if (skillInfo.Issonskill == false && skillInfo.Id != 18)
        {
            playerSkillCaster.PlayAttackAnim();
        }
        else
        {
            //SonSkillCaster.Instance.SonSkillAnim();
        }

        SkillCoolTimeManager.SetActiveSkillCool(skillInfo.Id, SkillCoolTimeManager.GetSkillCoolTimeMax(skillInfo));

        SpawnActiveEffect();

        PlaySoundEfx(skillInfo.Soundname);
    }

    private void PlaySoundEfx(string soundKey)
    {
        SoundManager.Instance.PlaySound(soundKey);
    }

    private bool CheckMp()
    {
        var currentMp = ServerData.userInfoTable.GetTableData(UserInfoTable.Mp);
        if (currentMp.Value >= skillInfo.Usecost)
        {
            PlayerStatusController.Instance.UpdateMp(-skillInfo.Usecost);
            return true;
        }

        if (AutoManager.Instance.IsAutoMode == false)
        {
            PopupManager.Instance.ShowAlarmMessage("마나가 부족합니다");
        }

        return false;
    }

    private void SpawnActiveEffect()
    {
        Transform targetTr = skillInfo.Issonskill == false ? PlayerMoveController.Instance.transform : SonSkillCaster.Instance.transform;

        Vector3 activeEffectSpawnPos = targetTr.position + Vector3.up * 0.5f;
        MoveDirection moveDirection = PlayerMoveController.Instance.MoveDirection;

        if (string.IsNullOrEmpty(skillInfo.Activeeffectname1) == false)
        {
            Transform parent = skillInfo.Iseffectrootplayer ? targetTr : null;
            var effect = EffectManager.SpawnEffectAllTime(skillInfo.Activeeffectname1, activeEffectSpawnPos, parent);

            if (effect != null)
            {

                if (skillInfo.Iseffectrootplayer == false)
                {
                    effect.transform.position = targetTr.position;
                    effect.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    effect.transform.localScale = new Vector3(Mathf.Abs(effect.transform.localScale.x) * (moveDirection == MoveDirection.Right ? 1f : -1f), effect.transform.localScale.y, effect.transform.localScale.z);

                }
            }
        }
        Vector3 activeEffectSpawnPos2 = targetTr.position + Vector3.up * 0.5f - Vector3.forward * 5f;

        if (string.IsNullOrEmpty(skillInfo.Activeeffectname2) == false)
        {
            Transform parent = skillInfo.Iseffectrootplayer ? targetTr : null;

            var effect = EffectManager.SpawnEffectAllTime(skillInfo.Activeeffectname2, activeEffectSpawnPos2, parent);

            if (effect != null)
            {

                if (skillInfo.Iseffectrootplayer == false)
                {
                    effect.transform.position = targetTr.position;
                    effect.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    effect.transform.localScale = new Vector3(Mathf.Abs(effect.transform.localScale.x) * (moveDirection == MoveDirection.Right ? 1f : -1f), effect.transform.localScale.y, effect.transform.localScale.z);

                }
            }
        }
    }
}
