using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSealSwordLock : MonoBehaviour
{
    [SerializeField]
    private int lockLevel = 2000000;

    private void OnEnable()
    {
        int currnetLevel = (int)ServerData.statusTable.GetTableData(StatusTable.Level).Value;

        bool isLock = currnetLevel < lockLevel;
        
        this.gameObject.SetActive(!isLock);

        if (isLock)
        {
            PopupManager.Instance.ShowAlarmMessage($"레벨 {Utils.ConvertBigNum(lockLevel)}이상일때 사용 가능합니다.");
        }
    }

}
