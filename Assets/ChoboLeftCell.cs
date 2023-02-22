using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ChoboLeftCell : MonoBehaviour
{

    private UiChoboBoard uiChoboBoard;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private Image goodsIcon;

    private ChoboTableData tableData;
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {


    } 



    public void Initialize(ChoboTableData _choboTableData , UiChoboBoard _uiChoboBoard)
    {
        if (_uiChoboBoard != null)
        {
            uiChoboBoard = _uiChoboBoard;
        }
        tableData = _choboTableData;
        if (tableData.HELPTYPE == HelpType.Goods)
        {
            goodsIcon.sprite = CommonUiContainer.Instance.GetItemIcon((Item_Type)tableData.Itemtype);
        }
        else
        {
            goodsIcon.gameObject.SetActive(false);
        }

        SetText();
    }
    public void OnClickCell()
    {
        uiChoboBoard.SetIndex(tableData.Id);
    }
    private void SetText()
    {
        if (tableData.HELPTYPE == HelpType.Goods)
        {
            titleText.SetText($"{CommonString.GetItemName((Item_Type)tableData.Itemtype)}");
        }
        else
        {
            titleText.SetText($"{tableData.Name}");
        }

    }

}
