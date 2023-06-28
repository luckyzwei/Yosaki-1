using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLimitPeriod : MonoBehaviour
{
    [SerializeField] private int year; 
    [SerializeField] private int month; 
    [SerializeField] private int day;


    private void OnEnable()
    {
        var servertime =  ServerData.userInfoTable.currentServerTime;
        
        DateTime limitTime = new DateTime(year, month, day);
        
        if (servertime > limitTime.AddDays(1))
        {
            gameObject.SetActive(false);
        }
    }

}
