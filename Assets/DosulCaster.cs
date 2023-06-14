using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class DosulCaster : MonoBehaviour
{
    public Transform skillSpawnPos;

    private Coroutine skillRoutine;

    private int myDosulSkillId = 0;

    [SerializeField]
    private GameObject dosulEffect;

    [SerializeField]
    private GameObject dosulParent;

    void Start()
    {
        StartCoroutine(UseDosulSkillRoutine());

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulLevel].AsObservable().Subscribe(e =>
        {
            
            UpdateMyDosulSkillId();

            dosulEffect.SetActive(myDosulSkillId != 0);
            
        }).AddTo(this);

        SettingData.showDosulSkill.AsObservable().Subscribe(e =>
        {
            dosulParent.SetActive(e > 0);
        }).AddTo(this);

        // AutoManager.Instance.AutoMode.AsObservable().Subscribe(e =>
        // {
        //     if (e)
        //     {
        //         if (skillRoutine != null)
        //         {
        //             StopCoroutine(skillRoutine);
        //         }
        //
        //         skillRoutine = StartCoroutine(UseDosulSkillRoutine());
        //     }
        //     else
        //     {
        //         if (skillRoutine != null)
        //         {
        //             StopCoroutine(skillRoutine);
        //         }
        //     }
        // }).AddTo(this);
    }


    private void UpdateMyDosulSkillId()
    {
        int myDosulLevel = (int)ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulLevel].Value;

        if (myDosulLevel != -1)
        {
            var tableData = TableManager.Instance.dosulTable.dataArray;

            for (int i = 0; i < myDosulLevel + 1; i++)
            {
                if (tableData[i].Unlock_Skill_Id != 0)
                {
                    myDosulSkillId = tableData[i].Unlock_Skill_Id;
                }
            }
        }
        else
        {
            myDosulSkillId = 0;
        }
    }

    private IEnumerator UseDosulSkillRoutine()
    {
        var skillTableDatas = TableManager.Instance.SkillData;

        while (true)
        {
            if (myDosulSkillId != 0)
            {
                if (PlayerSkillCaster.Instance.CanUseSkill(myDosulSkillId))
                {
                    Debug.LogError("@@@Do sul active");
                    PlayerSkillCaster.Instance.UseSkill(myDosulSkillId);
                }
            }

            yield return null;
        }
    }
}