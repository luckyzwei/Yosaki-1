using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiGoodsLockMask : MonoBehaviour
{
    [SerializeField] private string goodsName;
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        ServerData.weaponTable.TableDatas[goodsName].hasItem.AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(e != 1);
        }).AddTo(this);
        
    }

}
