using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UiGangChulView : SingletonMono<UiGangChulView>
{
    [SerializeField]
    private UiTwelveBossContentsView bossContentsView;

    [SerializeField]
    private GameObject guildRecordObject;

    public ObscuredInt rewardGrade = 0;

    [SerializeField]
    private Button recordButton;

    
    void Start()
    {
        Initialize();

        Subscribe();
    }

    private void Subscribe()
    {
    
    }
    private void OnEnable()
    {
 
    }

    private void Initialize()
    {
        
    }

    public void RecordGuildScoreButton()
    {
      
    }
}
