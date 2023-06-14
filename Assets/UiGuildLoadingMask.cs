using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGuildLoadingMask : SingletonMono<UiGuildLoadingMask>
{
    [SerializeField]
    private GameObject rootObject;

    public void Show(bool show)
    {
        rootObject.SetActive(show);
    }  
}
