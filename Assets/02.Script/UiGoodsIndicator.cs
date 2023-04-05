using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
public class UiGoodsIndicator : MonoBehaviour
{
    [SerializeField]
    private string goodsKey;

    [SerializeField]
    private TextMeshProUGUI goodsText;

    [SerializeField]
    private Button clickButton;

    private int itemIdx = -1;
    void Start()
    {
        Subscribe();

        //AddDescription();
    }

    private void AddDescription()
    {
        if (clickButton == null)
        {
            if (this.GetComponent<Button>() != null)
            {
                clickButton = this.GetComponent<Button>();
            }
            else
            {
                clickButton = this.gameObject.AddComponent<Button>();
            }
            clickButton.onClick.AddListener(OnClickButton);
        }
    }

    private void Subscribe()
    {
        if (ServerData.goodsTable.TableDatas.ContainsKey(goodsKey))
        {
            ServerData.goodsTable.GetTableData(goodsKey).AsObservable().Subscribe(goods =>
            {
                if (goodsKey.Equals(GoodsTable.GuildTowerClearTicket))
                {
                    goodsText.SetText($"{Utils.ConvertBigNum(goods).ToString()}/{GameBalance.GuildTowerTicketMaxCount}");
                }
                else
                {
                    goodsText.SetText($"{Utils.ConvertBigNum(goods).ToString()}");
                }
        
            }).AddTo(this);
        }
    }

    public void OnClickButton()
    {
        //item 없을떄
        if (itemIdx == -1)
        { 
            Item_Type a = ServerData.goodsTable.ServerStringToItemType(goodsKey);
            var tableData = TableManager.Instance.choboTable.dataArray;

            for (int i = 0; i < tableData.Length; i++)
            {
                if ((Item_Type)tableData[i].Itemtype == a)
                {
                    itemIdx = -1;
                    PopupManager.Instance.ShowConfirmPopup($"{CommonString.GetItemName((Item_Type)tableData[i].Itemtype)}", $"{tableData[i].Description0}", null);
                    break;
                }    
            }
            
        }
        //item 찾아놓음
        else
        {
            var tableData = TableManager.Instance.choboTable.dataArray;
            PopupManager.Instance.ShowConfirmPopup($"{CommonString.GetItemName((Item_Type)tableData[itemIdx].Itemtype)}", $"{tableData[itemIdx].Description0}", null);
        }

    }
}
