using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiGoldTransLockMask : MonoBehaviour
{
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.graduateGold).AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(e < 1);
        }).AddTo(this);
        
    }

}
