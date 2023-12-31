﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGumihoContentsBoard : MonoBehaviour
{
    [SerializeField]
    private UiTwelveBossContentsView uiBossContentsViewPrefab;

    [SerializeField]
    private Transform cellParent;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tableDatas = TableManager.Instance.TwelveBossTable.dataArray;
        //길드보스 -1
        for (int i = 30; i < 39; i++)
        {
            var cell = Instantiate<UiTwelveBossContentsView>(uiBossContentsViewPrefab, cellParent);

            cell.Initialize(tableDatas[i]);
        }

    }
}
