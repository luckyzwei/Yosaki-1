using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SealSkillCaster : SingletonMono<SealSkillCaster>
{
    public Transform skillSpawnPos;

    private Coroutine skillRoutine;

    public static ReactiveProperty<int> currentHitCount = new ReactiveProperty<int>();

    private SkillTableData currentSkillData = null;

    [SerializeField]
    private GameObject gaugeRoot;

    [SerializeField]
    private Image gauge;

    [SerializeField]
    private Animator animator;

    private string maxAnimTrigger = "Play";

    
    void Start()
    {
        currentHitCount.Value = 0;

        StartCoroutine(UserFourSkillRoutine());

        StartCoroutine(SkillCountAnimRoutine());

        ServerData.equipmentTable.TableDatas[EquipmentTable.SealSword_View].AsObservable().Subscribe(e =>
            {
                if (e != -1)
                {
                    gaugeRoot.SetActive(true);
                }
                else
                {
                    gaugeRoot.SetActive(false);
                }
                
              
            }).AddTo(this);
            
    }


    private int count_Real;
    private int count_Max = 100;
    private int count_Showing;

    private WaitForSeconds delay = new WaitForSeconds(0.01f);
    private WaitForSeconds directionDelay = new WaitForSeconds(0.3f);

    
    private IEnumerator SkillCountAnimRoutine()
    {
        while (true)
        {
            if (AutoManager.Instance.IsAutoMode)
            {
                if (count_Showing <= currentHitCount.Value)
                {
                    count_Showing++;
                }

                if (count_Showing >= count_Max -10)
                {
                    animator.SetTrigger(maxAnimTrigger);

                    count_Showing = 0;

                    gauge.fillAmount = 1f;

                    yield return directionDelay;

                    count_Showing = currentHitCount.Value;
                }

                gauge.fillAmount = count_Showing / (float)count_Max;
            }

            yield return null;
        }
    }

    private IEnumerator UserFourSkillRoutine()
    {
        var skillTableDatas = TableManager.Instance.SkillData;


        while (true)
        {
            int currentSealSwordEquipIdx = (int)ServerData.equipmentTable.TableDatas[EquipmentTable.SealSword].Value;

            if (currentSealSwordEquipIdx == -1)
            {
                yield return null;
                continue;
            }

            var skillIdx = TableManager.Instance.sealSwordTable.dataArray[currentSealSwordEquipIdx].Awakeskillid;

            if (skillIdx >= 0 && skillIdx < TableManager.Instance.SkillTable.dataArray.Length)
            {
                currentSkillData = TableManager.Instance.SkillTable.dataArray[skillIdx];

                count_Max = currentSkillData.Requirehit;

                if (currentSkillData.Requirehit <= currentHitCount.Value)
                {
                    Debug.LogError("Use Mini Ult SKill");
                    
                    PlayerSkillCaster.Instance.UseSkill(skillIdx);

                    currentHitCount.Value = 0;
                }
            }

            yield return null;
        }
    }
}