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
            //�̱���
            if(e==0)
            {
                itemName.SetText("�н��� ����");
            }
            else
            {
                itemName.SetText("���� ����");
            }
        }).AddTo(this);
    }
}
