using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiThiefKingLockMask : MonoBehaviour
{
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        ServerData.weaponTable.TableDatas["weapon95"].hasItem.AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(e != 1);
        }).AddTo(this);
        
    }

}
