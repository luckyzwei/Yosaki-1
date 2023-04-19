using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiDarkAbilCell : MonoBehaviour
{

    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI name;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private GameObject lockIcon;


    private DarkAbilData tableData;

    public void Initialize(DarkAbilData tableData)
    {
        this.tableData = tableData;

        icon.sprite = CommonResourceContainer.GetDarkIconSprite(tableData.Id);
        this.name.SetText(tableData.Name);

        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.goodsTable.GetTableData(tableData.Goods).AsObservable().Subscribe(e =>
        {
            StatusType type = (StatusType)(tableData.Abiltype);

            description.SetText($"{CommonString.GetStatusName(type)}\n{tableData.Abilbasevalue * 100f}%");

            lockIcon.SetActive(e == 0);

        }).AddTo(this);
    }
}
