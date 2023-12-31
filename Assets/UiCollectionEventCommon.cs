using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UiCollectionEventCommon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI eventDescription;

    private void Start()
    {
        SetDescriptionText();

        ServerData.userInfoTable.UpdateSnowCollectionComplete();
    }

    private void SetDescriptionText()
    {
        string description = string.Empty;

        description = "-";
        //description += $"<color=red>벚꽃 획득 3월 31일까지</color>\n";
        //description += $"<color=red>아이템 제작 3월 31일까지</color>\n";
        //description += $"<color=red>상품 판매 3월 31일까지</color>";

        eventDescription.SetText(description);
    }

#if UNITY_EDITOR
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += 1000000;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ServerData.goodsTable.GetTableData(GoodsTable.Event_Item_SnowMan).Value += 10;
        }
    }
#endif
}
