using System.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiWolfRingEquipBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gradeText;

    [SerializeField]
    private TextMeshProUGUI abilDescription;

    [SerializeField]
    private TextMeshProUGUI currentAwakeLevel;

    [SerializeField]
    private TextMeshProUGUI requireText;

    [SerializeField]
    private Image dragonIcon;

    [SerializeField]
    private GameObject currentSelectedObejct;

    private int currentIdx = -1;

    private void OnEnable()
    {
        currentIdx = PlayerStats.GetCurrentWolfRingIdx();

        UpdateByCurrnetId();
    }

    private void UpdateByCurrnetId()
    {
        if (currentIdx == -1) return;

        currentSelectedObejct.SetActive(currentIdx == PlayerStats.GetCurrentWolfRingIdx());

        currentAwakeLevel.SetText($"현재 강화도 : + {ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).Value}강");

        var tableData = TableManager.Instance.BlackWolfRing.dataArray[currentIdx];

        dragonIcon.sprite = CommonResourceContainer.GetWolfRingSprite(currentIdx);

        requireText.SetText($"강화 {tableData.Require} 필요");

        string description = string.Empty;

        float abilValue0 = PlayerStats.GetWolfRingAbilValue(currentIdx, 0) * 100f;
        float abilValue1 = PlayerStats.GetWolfRingAbilValue(currentIdx, 1) * 100f;
        float abilValue2 = PlayerStats.GetWolfRingAbilValue(currentIdx, 2) * 100f;
        float abilValue3 = PlayerStats.GetWolfRingAbilValue(currentIdx, 3) * 100f;
        float abilValue4 = PlayerStats.GetWolfRingAbilValue(currentIdx, 4) * 100f;
        float abilValue5 = PlayerStats.GetWolfRingAbilValue(currentIdx, 5) * 100f;

        if (abilValue0 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype0)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 0) * 100f}%\n"; }
        if (abilValue1 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype1)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 1) * 100f}%\n"; }
        if (abilValue2 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype2)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 2) * 100f}%\n"; }
        if (abilValue3 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype3)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 3) * 100f}%\n"; }
        if (abilValue4 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype4)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 4) * 100f}%\n"; }
        if (abilValue5 != 0f) { description += $"{CommonString.GetStatusName((StatusType)tableData.Abiltype5)} {PlayerStats.GetWolfRingAbilValue(currentIdx, 5) * 100f}%\n"; }

        abilDescription.SetText(description);

        gradeText.SetText($"{currentIdx + 1}단계");
    }

    public void OnClickRightButton()
    {
        currentIdx++;
        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.BlackWolfRing.dataArray.Length - 1);
        UpdateByCurrnetId();
    }
    public void OnClickLeftButton()
    {
        currentIdx--;
        currentIdx = Mathf.Clamp(currentIdx, 0, TableManager.Instance.BlackWolfRing.dataArray.Length - 1);
        UpdateByCurrnetId();
    }
}
