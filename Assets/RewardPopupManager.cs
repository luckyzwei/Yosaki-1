using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopupManager : SingletonMono<RewardPopupManager>
{
    [SerializeField]
    MainTabButtons mainTabButtons;


    public void OnclickButton()
    {
        if (mainTabButtons == null)
        {
            mainTabButtons = this.gameObject.GetComponent<MainTabButtons>();
        }

        if (mainTabButtons != null)
        {
            mainTabButtons.OnClickButton();
        }
    }
    // Start is called before the first frame update
}