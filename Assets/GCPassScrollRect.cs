using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GCPassScrollRect : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    

    private SeletableTab seletableTab;
    
    public void Initialize(SeletableTab _seletableTab)
    {
        seletableTab = _seletableTab;

        RegistSelectableTab();
    }

    private void RegistSelectableTab()
    {
        seletableTab.AddGameObject(this.gameObject);
    }

    public RectTransform GetRectTransform()
    {
        return content;
    }

}
