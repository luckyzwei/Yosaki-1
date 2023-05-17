using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSealSwordHideButton : MonoBehaviour
{
    public void OnClickHideButton()
    {
        PopupManager.Instance.ShowAlarmMessage("외형을 해제했습니다.");
        
        int currentIndex = ServerData.equipmentTable.TableDatas[EquipmentTable.SealSword_View].Value;

        if (currentIndex == -1) return;

        ServerData.equipmentTable.ChangeEquip(EquipmentTable.SealSword_View, -1);
    }
}
