using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class InvisibleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }


    private void Subscribe()
    {
        
        if(GameManager.Instance.bossId==155)
        {
            ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).AsObservable().Subscribe(e =>
            {
                this.gameObject.SetActive(e >= GameBalance.TwelveBoss_155_RequireTower10);
            }).AddTo(this);
        }
        else if (GameManager.Instance.bossId == 156)
        {
            ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).AsObservable().Subscribe(e =>
            {
                this.gameObject.SetActive(e >= GameBalance.TwelveBoss_156_RequireTower10);
            }).AddTo(this);
        }
        else if (GameManager.Instance.bossId == 157)
        {
            ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).AsObservable().Subscribe(e =>
            {
                this.gameObject.SetActive(e >= GameBalance.TwelveBoss_157_RequireTower10);
            }).AddTo(this);
        }
    }
}
