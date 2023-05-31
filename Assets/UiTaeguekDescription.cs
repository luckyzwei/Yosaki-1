using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTaeguekDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilDescription;


    [SerializeField]
    private TextMeshProUGUI gradeText_Current;

    [SerializeField]
    private TextMeshProUGUI gradeText_pref;
    
    [SerializeField]
    private TextMeshProUGUI gradeText_next;
    
    [SerializeField]
    private TextMeshProUGUI unlockDesc;

    [SerializeField]
    private GameObject equipFrame;

    [SerializeField]
    private Image image;

    public int currentIdx;

    private void Start()
    {
        currentIdx = PlayerStats.GetTaeguekGrade();

        Initialize(currentIdx);
    }

    public void Initialize(int idx)
    {
        if (idx == -1) idx = 0;

        var tableData = TableManager.Instance.taegeukTitle.dataArray[idx];

        unlockDesc.SetText($"{Utils.ConvertBigNumForRewardCell(tableData.Hp)}");

        equipFrame.gameObject.SetActive(idx == PlayerStats.GetTaeguekGrade());

        gradeText_Current.SetText($"{tableData.Titlename}");

        var addDescription = "";

        abilDescription.SetText($"{CommonString.GetStatusName(StatusType.SuperCritical16DamPer)}{tableData.Abilvalue * 100f}");
        
        //
        int prefIdx = idx - 1;
        if (prefIdx >= 0)
        {
            var prefTableData = TableManager.Instance.taegeukTitle.dataArray[prefIdx];
            
            gradeText_pref.SetText($"{prefTableData.Titlename}");
        }
        else
        {
            gradeText_pref.SetText("없음");
        }
        
        int nextIdx = idx + 1;
        
        if (nextIdx < TableManager.Instance.taegeukTitle.dataArray.Length)
        {
            var nextTableData = TableManager.Instance.taegeukTitle.dataArray[nextIdx];
            
            gradeText_next.SetText($"{nextTableData.Titlename}");
        }
        else
        {
            gradeText_next.SetText("최고단계");
        }

    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.taegeukTitle.dataArray.Length - 1);

        Initialize(currentIdx);

        if (currentIdx == -1)
        {
            PopupManager.Instance.ShowAlarmMessage("처음 단계 입니다!");
        }
    }

    public void OnClickRightButton()
    {
        if (currentIdx == TableManager.Instance.taegeukTitle.dataArray.Length - 1)
        {
            PopupManager.Instance.ShowAlarmMessage("마지막 단계입니다!");
        }

        currentIdx++;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.taegeukTitle.dataArray.Length - 1);

        Initialize(currentIdx);
    }
}