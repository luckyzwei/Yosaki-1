using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWolfRingView : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.statusTable.GetTableData(StatusTable.PetEquip_Level).AsObservable().Subscribe(e =>
        {

            RefreshUi();

        }).AddTo(this);

        ServerData.userInfoTable.TableDatas[UserInfoTable.getWolfRing].AsObservable().Subscribe(e =>
        {

            if (e == 1)
            {
                RefreshUi();
            }

        }).AddTo(this);
    }


    private void RefreshUi()
    {
        int idx = PlayerStats.GetCurrentWolfRingIdx();

        if (idx == -1 || ServerData.userInfoTable.TableDatas[UserInfoTable.getWolfRing].Value == 0)
        {
            this.icon.gameObject.SetActive(false);
        }
        else
        {
            this.icon.gameObject.SetActive(true);
            icon.sprite = CommonResourceContainer.GetWolfRingSprite(idx);
        }
    }
}
