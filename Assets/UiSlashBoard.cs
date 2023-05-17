using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSlashBoard : MonoBehaviour
{
    private void OnEnable()
    {
        UiPlayerStatBoard.Instance.Refresh();
    }
}