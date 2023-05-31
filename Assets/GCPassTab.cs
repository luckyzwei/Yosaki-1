using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GCPassTab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private Button _button;

    private SeletableTab seletableTab;
    private int idx = 0;

    public void Initialize(int _idx, SeletableTab _seletableTab)
    {
        idx = _idx;

        seletableTab = _seletableTab;

        SetText(idx+1);

        RegistSelectableTab();
        
        _button.onClick.AddListener(OnClickButton);
    }

    private void SetText(int idx)
    {
        _text.SetText($"{idx}");
    }
    private void RegistSelectableTab()
    {
        seletableTab.AddElement(GetComponent<Image>());
    }

    private void OnClickButton()
    {
        seletableTab.OnSelect(idx);
    }
}
