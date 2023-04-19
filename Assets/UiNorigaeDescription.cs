using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UiNorigaeDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilDescription;

    [SerializeField] private TextMeshProUGUI immuneDescription;

    [SerializeField] private TextMeshProUGUI gradeText;

    [SerializeField] private TextMeshProUGUI unlockDesc;

    [SerializeField] private GameObject equipFrame;

    [SerializeField] private Image image;

    private int currentIdx;

    private void Start()
    {
        currentIdx = PlayerStats.GetNorigaeSoulGrade();

        Initialize(currentIdx);
    }

    public void Initialize(int idx)
    {
        if (idx == -1) idx = 0;

        var tableData = TableManager.Instance.norigaeJewel.dataArray[idx];
        unlockDesc.SetText($"{Utils.ConvertBigNumForRewardCell(tableData.Score)}");



        equipFrame.gameObject.SetActive(idx == PlayerStats.GetNorigaeSoulGrade());

        gradeText.SetText($"{idx + 1}단계");

        abilDescription.SetText($"{CommonString.GetStatusName(StatusType.NorigaeGoldAbilUp)} {Utils.ConvertBigNum(tableData.Abilvalue0)}배 강화");

        image.sprite = Resources.Load<Sprite>($"MagicBook/{idx}");
    }

    public void OnClickLeftButton()
    {
        currentIdx--;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.norigaeJewel.dataArray.Length - 1);

        Initialize(currentIdx);

        if (currentIdx == -1)
        {
            PopupManager.Instance.ShowAlarmMessage("처음 단계 입니다!");
        }
    }

    public void OnClickRightButton()
    {
        if (currentIdx == TableManager.Instance.norigaeJewel.dataArray.Length - 1)
        {
            PopupManager.Instance.ShowAlarmMessage("업데이트 예정 입니다!");
        }

        currentIdx++;

        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.norigaeJewel.dataArray.Length - 1);

        Initialize(currentIdx);
    }
}