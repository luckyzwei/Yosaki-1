using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiSumiLockMask : MonoBehaviour
{
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        ServerData.weaponTable.TableDatas["weapon84"].hasItem.AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(e != 1);
        }).AddTo(this);
        
    }

}
