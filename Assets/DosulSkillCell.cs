using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class DosulSkillCell : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private TextMeshProUGUI levelDescription;

    private DosulData dosulData;

    [SerializeField]
    private Image skillIcon;

    [SerializeField]
    private GameObject lockMask;

    [SerializeField]
    private TextMeshProUGUI lockDescription;

    public void Initialize(DosulData dosulData)
    {
        this.dosulData = dosulData;

        skillIcon.sprite = CommonResourceContainer.GetSkillIconSprite(dosulData.Unlock_Skill_Id);

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulLevel].AsObservable().Subscribe(e =>
        {
            var skillTableData = TableManager.Instance.SkillTable.dataArray[dosulData.Unlock_Skill_Id];
            
            //있으면
            if (e >= dosulData.Id)
            {
                lockMask.SetActive(false);
                description.SetText(skillTableData.Skilldesc + $"\n도술 피해량 :  {Utils.ConvertBigNum(ServerData.skillServerTable.GetSkillDamagePer(skillTableData.Id, applySkillDamAbility: false)/100)}%");

            }
            else
            {
                lockMask.SetActive(true);
                lockDescription.SetText($"도술 레벨\n{dosulData.Id+1}달성시 개방");
                description.SetText(skillTableData.Skilldesc + $"\n도술 피해량 :  {Utils.ConvertBigNum(ServerData.skillServerTable.GetSkillDamagePer(skillTableData.Id, applySkillDamAbility: false)/100)}%");
                levelDescription.SetText($"LV : {0}");
            }
            
        }).AddTo(this);
      
    }
}
