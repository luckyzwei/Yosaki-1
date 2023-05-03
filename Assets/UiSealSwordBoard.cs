using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSealSwordBoard : SingletonMono<UiSealSwordBoard>
{
    [SerializeField]
    private UiInventoryWeaponView uiInventoryWeaponViewPrefab;
    
    private List<UiInventoryWeaponView> weaponViewContainer = new List<UiInventoryWeaponView>();

    [SerializeField]
    private Transform viewParentWeapon;
    
    public void Start()
    {
           var tableData = TableManager.Instance.sealSwordTable.dataArray;
           
           for (int i = 0; i < tableData.Length; i++)
           {
               UiInventoryWeaponView view = Instantiate<UiInventoryWeaponView>(uiInventoryWeaponViewPrefab, viewParentWeapon);

               view.Initialize(null, null, null, tableData[i] ,OnClickWeaponView);

               weaponViewContainer.Add(view);
           }
    }
    
    private void OnClickWeaponView(WeaponData weaponData, MagicBookData magicBookData)
    {
    }
    
    public void AllUpgradeWeapon(int myIdx)
    {
        for (int i = 0; i <= myIdx; i++)
        {
            weaponViewContainer[i].OnClickUpgradeButton();
        }
    }
}
