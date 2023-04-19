using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiVisionSkillCell : MonoBehaviour
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

    [SerializeField]
    private TextMeshProUGUI activeRequireDescription;

    [SerializeField] private int skillIdx = -1;
    public void Initialize(SkillTableData skillTableData)
    {
        this.skillTableData = skillTableData;

        skillIcon.sprite = CommonResourceContainer.GetSkillIconSprite(skillTableData.Id);

        if (ServerData.userInfoTable.TableDatas[UserInfoTable.currentFloorIdx7].Value >= 10)
        {
            activeRequireDescription.SetText($"기술 {this.skillTableData.Requirehit-GameBalance.HyulVisionSkillDecreaseValue}회 사용후 발동");
        }
        else
        {
            activeRequireDescription.SetText($"기술 {this.skillTableData.Requirehit}회 사용후 발동");
        }
      

        Subscribe();
    }

    private void Start()
    {
        InitializeByInspector();
    }

    private void InitializeByInspector()
    {
        if (skillIdx < 0)
        {
            return;
        }
        skillTableData = TableManager.Instance.SkillTable.dataArray[skillIdx];
        Initialize(skillTableData);
    }
    private void Subscribe()
    {
        ServerData.goodsTable.GetTableData($"{skillTableData.Skillclassname}").AsObservable().Subscribe(e =>
        {
            //있으면
            if (e > 0)
            {
                lockMask.SetActive(false);
                description.SetText(skillTableData.Skilldesc + $"\n피해량 :  {Utils.ConvertBigNum(ServerData.skillServerTable.GetSkillDamagePer(skillTableData.Id, applySkillDamAbility: false) * 100f)}%");
                levelDescription.SetText($"LV : MAX");
            }
            else
            {
                lockMask.SetActive(true);
                lockDescription.SetText($"궁극 기술 획득시 개방");
                description.SetText(skillTableData.Skilldesc + $"\n피해량 : {0}%");
                levelDescription.SetText($"LV : {0}");
            }
        }).AddTo(this);

    }

}
