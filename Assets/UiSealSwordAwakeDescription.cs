using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiSealSwordAwakeDescription : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI abilDescription;


    [SerializeField]
    private TextMeshProUGUI gradeText;

    [SerializeField]
    private TextMeshProUGUI unlockDesc;

    [SerializeField]
    private GameObject equipFrame;

    private int currentIdx;

    private void Start()
    {
        currentIdx = PlayerStats.GetSealSwordAwakeGrade();

        Initialize(currentIdx);
    }

    public void Initialize(int idx)
    {
        if (idx == -1) idx = 0;

        var tableData = TableManager.Instance.SealSwordAwakeTable.dataArray[idx];
        unlockDesc.SetText($"{Utils.ConvertBigNumForRewardCell(tableData.Score)}");

        equipFrame.gameObject.SetActive(idx == PlayerStats.GetSealSwordAwakeGrade());

        gradeText.SetText($"{idx + 1}단계");

        var addDescription = "";

        //
        abilDescription.SetText($"요도 피해량 증가 +{Utils.ConvertBigNum(tableData.Awakevalue)}");
        //

        // if (idx < 109)
        // {
        //     image.sprite = Resources.Load<Sprite>($"SealAwake/{idx / 3}"); 
        // }
        // else
        // {
        //     image.sprite = Resources.Load<Sprite>($"SealAwake/36");
        // }
    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.SealSwordAwakeTable.dataArray.Length - 1);

        Initialize(currentIdx);

        if (currentIdx == -1)
        {
            PopupManager.Instance.ShowAlarmMessage("처음 단계 입니다!");
        }
    }

    public void OnClickRightButton()
    {
        if (currentIdx == TableManager.Instance.SealSwordAwakeTable.dataArray.Length - 1)
        {
            PopupManager.Instance.ShowAlarmMessage("업데이트 예정 입니다!");
        }

        currentIdx++;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.SealSwordAwakeTable.dataArray.Length - 1);

        Initialize(currentIdx);
    }
    
}