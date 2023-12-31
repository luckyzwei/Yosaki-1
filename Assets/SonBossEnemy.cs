﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;
using Spine.Unity;

public class SonBossEnemy : BossEnemyBase
{

    private void Start()
    {
        Initialize();
    }

    private void UpdateBossDamage()
    {


        hitObject.SetDamage(0f);
    }

    private IEnumerator BossAttackPowerUpdateRoutine()
    {
        var updateDelay = new WaitForSeconds(5.0f);

        while (true)
        {
            UpdateBossDamage();
            yield return updateDelay;
        }
    }

    private IEnumerator AttackRoutine()
    {

        UpdateBossDamage();
        //선딜
        yield return new WaitForSeconds(2.0f);

        while (true)
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            int attackType = Random.Range(0, 3);

#if UNITY_EDITOR
            Debug.LogError($"AttackType {attackType}");
#endif

            if (attackType == 0)
            {
                yield return StartCoroutine(AttackRoutine_2(1.0f));
            }
            else if (attackType == 1)
            {
                yield return StartCoroutine(AttackRoutine_3(1.0f));
            }
            else if (attackType == 2)
            {
                yield return StartCoroutine(AttackRoutine_4(1.0f));
            }

            StartCoroutine(PlayAttackAnim());

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator PlayAttackAnim()
    {
        yield return new WaitForSeconds(1.0f);
    }

    private void Initialize()
    {
        agentHpController.SetHp(float.MaxValue);

        agentHpController.SetDefense(10);

        StartCoroutine(BossAttackPowerUpdateRoutine());

        StartCoroutine(AttackRoutine());
    }


    private IEnumerator PlaySoundDelay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        SoundManager.Instance.PlaySound(name);
    }


    //?
    private IEnumerator AttackRoutine_2(float delay)
    {
        StartCoroutine(PlaySoundDelay(1f, "BossSkill2"));

        yield return new WaitForSeconds(delay);
    }

    //?
    private IEnumerator AttackRoutine_3(float delay)
    {
        StartCoroutine(PlaySoundDelay(1f, "BossSkill3"));

        yield return new WaitForSeconds(delay);
    }

    //
    private IEnumerator AttackRoutine_4(float delay)
    {
        StartCoroutine(PlaySoundDelay(1f, "BossSkill3"));

        yield return new WaitForSeconds(delay);
    }
}
