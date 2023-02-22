using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
public class UiBomnamulPassDesc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemName;

    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }
    void Subscribe()
    {
        ServerData.iapServerTable.TableDatas[UiCollectionPass0BuyButton.PassKey].buyCount.AsObservable().Subscribe(e =>
        {
            //미구매
            if(e==0)
            {
                itemName.SetText("패스권 구매");
            }
            else
            {
                itemName.SetText("구매 혜택");
            }
        }).AddTo(this);
    }
}
