using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UiVacationCostumeBuyButton : MonoBehaviour
{
    
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.costumeServerTable.TableDatas["costume137"].hasCostume.AsObservable().Subscribe(e =>
        {
            this.gameObject.SetActive(!e);
        }).AddTo(this);
    }
}
