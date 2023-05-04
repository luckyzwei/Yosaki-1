using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;

public class SealWeaponEquipView : MonoBehaviour
{
    [SerializeField]
    private Image weaponImage;

    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.equipmentTable.TableDatas[EquipmentTable.SealSword_View].AsObservable().Subscribe(WhenEquipIdxChanged).AddTo(this);

    }

    private void WhenEquipIdxChanged(int idx)
    {
        if (idx == -1)
        {
            weaponImage.gameObject.SetActive(false);
        }
        else
        {
            weaponImage.gameObject.SetActive(true);
            weaponImage.sprite = CommonResourceContainer.GetSealSwordIconSprite(idx); 
        }
    }

}
