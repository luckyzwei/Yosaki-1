using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiSuhoPetButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameDescription;

    private SuhopetTableData tableData;

    [SerializeField]
    private Image button;

    [SerializeField]
    private Color enableColor;

    [SerializeField]
    private Color disableColor;

    public void Initialize(SuhopetTableData tableData)
    {
        this.tableData = tableData;

        nameDescription.SetText($"{this.tableData.Gradedescription}\n{(tableData.Name)}");

        Subscribe();
    }

    private void Subscribe()
    {
        UiSuhoAnimalBoard.Instance.currentSelectedIdx.AsObservable().Subscribe(e =>
        {
            
            button.color = tableData.Id==e ? enableColor:disableColor;
            
        }).AddTo(this);

    }

    public void OnClickButton()
    {
        UiSuhoAnimalRewardPopup.Instance.Initialize(tableData.Id);

        UiSuhoAnimalBoard.Instance.currentSelectedIdx.Value = tableData.Id;
    }
}