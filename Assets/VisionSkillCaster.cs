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
    private int _visionSkillIdx = 0;
    void Start()
    {
        Subscribe();
        RefreshSkillData();
    }

    private void RefreshSkillData()
    {
        _visionSkillIdx = ServerData.goodsTable.GetVisionSkillIdx();
        _skillTableData = TableManager.Instance.SkillTable.dataArray[_visionSkillIdx];
    }
    
    private void Subscribe()
    {
        AutoManager.Instance.AutoMode.AsObservable().Subscribe(e =>
        {
            if (e)
            {
                StartSkillRoutine();
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


    public void StartSkillRoutine()
    {
        if (skillRoutine != null)
        {
            StopCoroutine(skillRoutine);
        }
        skillRoutine = StartCoroutine(UserSkillRoutine());
    }
    
    private IEnumerator UserSkillRoutine()
    {
        while (true)
        {
            if (PlayerSkillCaster.Instance.visionChargeCount.Value < 1 &&
                !PlayerSkillCaster.Instance.useVisionSkill.Value &&
                _visionSkillIdx > 0 &&
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

}
