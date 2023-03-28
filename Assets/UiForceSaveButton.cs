using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiForceSaveButton : MonoBehaviour
{
    private bool savedComplete = false;

    public void OnClickForceSaveButton()
    {
        if (savedComplete == true)
        {
            PopupManager.Instance.ShowAlarmMessage("저장 완료됐습니다.");
            return;
        }

        savedComplete = true;

        SaveManager.Instance.SyncDatasInQueue();
        SaveManager.Instance.SyncDailyMissions();
        
        PopupManager.Instance.ShowAlarmMessage("저장 완료");
    }
}
