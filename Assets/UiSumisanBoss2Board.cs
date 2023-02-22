using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSumisanBoss2Board : MonoBehaviour
{
    [SerializeField]
    private List<UiTwelveBossContentsView> sumisanBoss2;


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        
        sumisanBoss2[0].Initialize(TableManager.Instance.TwelveBossTable.dataArray[93]);
        sumisanBoss2[1].Initialize(TableManager.Instance.TwelveBossTable.dataArray[94]);
        sumisanBoss2[2].Initialize(TableManager.Instance.TwelveBossTable.dataArray[95]);
         
    }
}
