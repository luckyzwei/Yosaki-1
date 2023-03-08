using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBossBoard : MonoBehaviour
{
    [SerializeField]
    private List<UiTwelveBossContentsView> Boss;
    [SerializeField]
    private List<int> BossIdx;


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for(int i =  0; i < Boss.Count;i++)
        {
            Boss[i].Initialize(TableManager.Instance.TwelveBossTable.dataArray[BossIdx[i]]);
        }
         
    }
}
