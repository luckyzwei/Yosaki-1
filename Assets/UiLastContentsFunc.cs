﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameManager;

public class UiLastContentsFunc : MonoBehaviour
{
    [SerializeField]
    private ContentsType contentsType;

    public UnityEvent myUnityEvent;


    [SerializeField]
    private bool resetState = false;

    public static bool AutoInfiniteTower = false;
    public static bool AutoInfiniteTower2 = false;

    void Start()
    {
        if (GameManager.Instance.IsNormalField && contentsType == GameManager.Instance.lastContentsType)
        {
            myUnityEvent?.Invoke();
        }

        if (resetState == true)
        {
            GameManager.Instance.ResetLastContents();
        }
    }
}
