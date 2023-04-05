using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class VisionAutoButton : MonoBehaviour
{
   
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Toggle auto;
    private bool _initialized;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Image _image;
    
    private void Start()
    {
        Initialize();
        Subscribe();
    }

    private void Subscribe()
    {
        SettingData.autoVisionSkill.AsObservable().Subscribe(e =>
        {
            _image.sprite = _sprites[e];
        }).AddTo(this);
    }
    private void Initialize()
    {
        auto.isOn = PlayerPrefs.GetInt(SettingKey.autoVisionSkill) == 1;
        _initialized = true;
    }

    public void OnClickButton(bool on)
    {

        if (_initialized == false) return;

        if (on)
        {
            SoundManager.Instance.PlayButtonSound();
        }
        SettingData.autoVisionSkill.Value = on ? 1 : 0;
    }
}
