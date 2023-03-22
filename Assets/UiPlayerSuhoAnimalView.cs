using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerSuhoAnimalView : MonoBehaviour
{
    [SerializeField]
    private Image petView;
    [SerializeField]
    private Image charmView;
    
    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }
    
    private void Subscribe()
    {
        ServerData.equipmentTable.TableDatas[EquipmentTable.SuhoAnimal].AsObservable().Subscribe(e =>
        {
            petView.enabled = e != -1;
            charmView.enabled = e != -1;

            if (e != -1)
            {
                petView.sprite = CommonResourceContainer.GetSuhoAnimalSprite(e);
            }

        }).AddTo(this);
    }

}
