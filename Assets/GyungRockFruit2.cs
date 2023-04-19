using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyungRockFruit2 : MonoBehaviour
{
    [SerializeField] private List<Sprite> fruits;
    [SerializeField] private Image icon_Image;
    [SerializeField] private SpriteRenderer icon_SpriteRenderer;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        icon_Image = GetComponent<Image>();
        icon_SpriteRenderer = GetComponent<SpriteRenderer>();

        int currentIdx = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.currentFloorIdx7].Value;
            
        if (icon_Image != null)
        {
            icon_Image.sprite = fruits[currentIdx];
        }

        if (icon_SpriteRenderer != null)
        {
            icon_SpriteRenderer.sprite = fruits[currentIdx];
        }
    }
}