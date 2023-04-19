using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiSuhoSkillCell : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI levelDescription;

    private SkillTableData skillTableData;

    [SerializeField]
    private Image skillIcon;

    [SerializeField]
    private GameObject lockMask;

    [SerializeField]
    private TextMeshProUGUI lockDescription;

    private SuhopetTableData suhopetTable;
    
    public void Initialize(SuhopetTableData suhopetTable)
    {
        this.suhopetTable = suhopetTable;
        
        this.skillTableData = TableManager.Instance.SkillTable.dataArray[suhopetTable.Awakeskillid];

        skillIcon.sprite = CommonResourceContainer.GetSuhoAnimalSprite(suhopetTable.Id);

        Subscribe();
    }
    
    private void Subscribe()
    {
        ServerData.suhoAnimalServerTable.TableDatas[suhopetTable.Stringid].level.AsObservable().Subscribe(e =>
        {
            //있으면
            if (e >= GameBalance.suhoAnimalAwakeLevel)
            {
                lockMask.SetActive(false);
                description.SetText(skillTableData.Skilldesc + $"\n피해량:{Utils.ConvertBigNum(ServerData.skillServerTable.GetSkillDamagePer(skillTableData.Id, applySkillDamAbility: false) * 100f)}%\n히트수:{skillTableData.Hitcount} 시전속도:{skillTableData.Cooltime}");
              //  levelDescription.SetText($"LV : MAX");
            }
            else
            {
                lockMask.SetActive(true);
                lockDescription.SetText($"수호동물 각성시 획득");
                description.SetText(skillTableData.Skilldesc + $"\n피해량:{Utils.ConvertBigNum(ServerData.skillServerTable.GetSkillDamagePer(skillTableData.Id, applySkillDamAbility: false) * 100f)}%\n히트수:{skillTableData.Hitcount} 시전속도:{skillTableData.Cooltime}");
                //levelDescription.SetText($"LV : {0}");
            }
        }).AddTo(this);

    }

}
