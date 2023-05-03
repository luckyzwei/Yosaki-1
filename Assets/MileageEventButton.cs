using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MileageEventButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject effectObject;
    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        var isEvent = ServerData.userInfoTable.IsMileageEvent();
        effectObject.SetActive(isEvent);
        if (isEvent)
        {
            if (_image != null)
            {
                _image.color = Color.red;
            }
        }
    }
}
