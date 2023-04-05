using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using UnityEngine;

public class VisionSkillCaster : SingletonMono<VisionSkillCaster>
{
    public Transform skillSpawnPos;

    private Coroutine skillRoutine;
    private SkillTableData _skillTableData;
    private int _visionCount = 0;
    void Start()
    {
        Subscribe();
        RefreshSkillData();
    }

    private void RefreshSkillData()
    {
        _visionCount = ServerData.goodsTable.GetVisionSkillHasCount();
        _skillTableData = TableManager.Instance.SkillTable.dataArray[45 + _visionCount];
    }
    
    private void Subscribe()
    {
        AutoManager.Instance.AutoMode.AsObservable().Subscribe(e =>
        {
            if (e)
            {
                if (skillRoutine != null)
                {
                    StopCoroutine(skillRoutine);
                }

                skillRoutine = StartCoroutine(UserSkillRoutine());
            }
            else
            {
                if (skillRoutine != null)
                {
                    StopCoroutine(skillRoutine);
                }
            }
        }).AddTo(this);
    }

    
    private IEnumerator UserSkillRoutine()
    {
        while (true)
        {
            if (PlayerSkillCaster.Instance.visionChargeCount.Value < 1 &&
                !PlayerSkillCaster.Instance.useVisionSkill.Value &&
                _visionCount > 0 &&
                SettingData.autoVisionSkill.Value > 0)
            {
                if (AutoManager.Instance.canAttack == false && GameManager.Instance.IsNormalField == true) continue;
                PlayerSkillCaster.Instance.UseSkill(_skillTableData.Id);
                PlayerSkillCaster.Instance.SetUseVisionSkill(true);
                StopCoroutine(skillRoutine);
            }

            yield return null;
        }

    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StopCoroutine(skillRoutine);
        }

    }
#endif

}
