﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerNataRotater : MonoBehaviour
{
    [SerializeField]
    private GameObject rotateObject;

    [SerializeField]
    private GameObject rotateObject_Gang;

    [SerializeField]
    private GameObject dokebi0;

    [SerializeField]
    private GameObject dokebi1;

    [SerializeField]
    private GameObject dokebi2;

    [SerializeField]
    private GameObject dokebi3;

    [SerializeField]
    private GameObject dokebi4;

    [SerializeField]
    private GameObject dokebi5;

    [SerializeField]
    private GameObject dokebi6;

    [SerializeField]
    private GameObject newDokebi0;
    [SerializeField]
    private GameObject newDokebi1;
    [SerializeField]
    private GameObject newDokebi2;

    [SerializeField]
    private float rotateSpeed = 0f;

    private float currentAngle;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.equipmentTable.TableDatas[EquipmentTable.CostumeLook].AsObservable().Subscribe(e =>
        {
            //도깨비 이펙트
            dokebi0.SetActive(e == 66);
            dokebi1.SetActive(e == 67);
            dokebi2.SetActive(e == 68);
            dokebi3.SetActive(e == 70);
            dokebi4.SetActive(e == 71);
            dokebi5.SetActive(e == 74);
            dokebi6.SetActive(e == 75);
            newDokebi0.SetActive(e == 78);
            newDokebi1.SetActive(e == 79);
            newDokebi2.SetActive(e == 80);

            rotateObject.SetActive(e == 35);
            //강림,일직,월직 이펙트
            rotateObject_Gang.SetActive(e == 48 || e == 51 || e == 53);

        }).AddTo(this);
    }

    void Update()
    {
        currentAngle += Time.deltaTime * rotateSpeed;

        if (rotateObject.activeInHierarchy)
        {
            rotateObject.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

        if (rotateObject_Gang.activeInHierarchy)
        {
            rotateObject_Gang.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

        //
        if (dokebi0.activeInHierarchy)
        {
            dokebi0.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

        if (dokebi1.activeInHierarchy)
        {
            dokebi1.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

        if (dokebi2.activeInHierarchy)
        {
            dokebi2.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        if (dokebi3.activeInHierarchy)
        {
            dokebi3.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        
        if (dokebi4.activeInHierarchy)
        {
            dokebi4.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        if (dokebi5.activeInHierarchy)
        {
            dokebi5.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        
        if (dokebi6.activeInHierarchy)
        {
            dokebi6.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }        
        if (newDokebi0.activeInHierarchy)
        {
            newDokebi0.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        if (newDokebi1.activeInHierarchy)
        {
            newDokebi1.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }
        if (newDokebi2.activeInHierarchy)
        {
            newDokebi2.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
        }

 


        if (currentAngle >= 360f)
        {
            currentAngle = currentAngle - 360f;
        }
    }
}
