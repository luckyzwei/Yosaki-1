using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class UiFoxFireContentsLockMask : MonoBehaviour
{
    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.userInfoTable.GetTableData(UserInfoTable.foxTowerIdx).AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(e < 1);
        }).AddTo(this);
    }

}
