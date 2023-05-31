using System;
using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class UiMileageBoard : SingletonMono<UiMileageBoard>
{
    [SerializeField]
    private UiMileageRewardCell premiumCell;
    [SerializeField]
    private UiMileageRewardCell cell;

    [SerializeField]
    private Transform premiumParents;
    [SerializeField]
    private Transform parents;

    public void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var tableDatas = TableManager.Instance.mileageReward.dataArray;

        for (int i = 0; i < tableDatas.Length; i++)
        {
            if(tableDatas[i].Active==false) continue;
            //프리미엄
            if (tableDatas[i].Eventmultiplevalue!=0)
            {
                var button = Instantiate<UiMileageRewardCell>(premiumCell, premiumParents);
                button.Initialize(i);
            }
            else
            {
                var button = Instantiate<UiMileageRewardCell>(cell, parents);
                button.Initialize(i);
            }
        }
    }


}