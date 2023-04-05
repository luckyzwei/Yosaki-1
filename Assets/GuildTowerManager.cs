using BackEnd;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using static UiRewardView;

public class GuildTowerManager : ContentsManagerBase
{
    [SerializeField]
    private Transform enemySpawnPos;

    private ReactiveProperty<ObscuredInt> contentsState = new ReactiveProperty<ObscuredInt>((int)ContentsState.Fight);

    private List<Enemy> spawnedEnemyList = new List<Enemy>();

    //null 일때 클리어 못한거
    private List<RewardData> rewardDatas;

    [SerializeField]
    private UiHellTowerResult uiInfinityTowerResult;

    public UnityEvent retryFunc;

    public static string poolName;

    private new void Start()
    {
        base.Start();

        Subscribe();

        StartCoroutine(ContentsRoutine());
    }

    #region Security

    private void OnEnable()
    {
        StartCoroutine(RandomizeRoutine());
    }

    private IEnumerator RandomizeRoutine()
    {
        var delay = new WaitForSeconds(1.0f);

        while (true)
        {
            RandomizeKey();
            yield return delay;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        RandomizeKey();
    }

    private void RandomizeKey()
    {
        contentsState.Value.RandomizeCryptoKey();
    }

    #endregion

    //종료조건

    #region EndConditions

    private void EnemyDeadCallBack(Enemy enemy)
    {
        spawnedEnemyList.Remove(enemy);

        //전부 처치함
        if (spawnedEnemyList.Count == 0)
        {
            contentsState.Value = (int)ContentsState.Clear;
        }
    }

    private void WhenPlayerDead()
    {
        //클리어 됐을때 죽지 않게
        if (contentsState.Value != (int)ContentsState.Fight) return;

        //  UiLastContentsFunc.AutoInfiniteTower2 = false;

        contentsState.Value = (int)ContentsState.Dead;
    }

    protected override void TimerEnd()
    {
        //  UiLastContentsFunc.AutoInfiniteTower2 = false;
        base.TimerEnd();
        contentsState.Value = (int)ContentsState.TimerEnd;
    }

    #endregion

    private void Subscribe()
    {
        contentsState.AsObservable().Subscribe(WhenTowerModeStateChanged).AddTo(this);

        PlayerStatusController.Instance.whenPlayerDead.Subscribe(e => { WhenPlayerDead(); }).AddTo(this);
    }


    private void WhenTowerModeStateChanged(ObscuredInt state)
    {
        if (state == (int)ContentsState.Clear)
        {
            //반드시 점수전송먼저
            SendScore();

            SetClear();
        }

        if (state != (int)ContentsState.Fight)
        {
            EndInfiniteTower();
        }
    }

    private void SendScore()
    {
        //int currentScore = (int)DatabaseManager.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value;
        //RankManager.Instance.UpdatInfinityTower_Score(currentScore);
    }

    private void EndInfiniteTower()
    {
        //몹 꺼줌
        spawnedEnemyList.ForEach(e => e.gameObject.SetActive(false));

        //타이머 종료
        if (contentsState.Value != (int)ContentsState.TimerEnd)
        {
            StopTimer();
        }

        //보상팝업
        ShowResultPopup();
    }


    private void ShowResultPopup()
    {
        //클리어 팝업출력
        uiInfinityTowerResult.gameObject.SetActive(true);
        uiInfinityTowerResult.Initialize((ContentsState)(int)contentsState.Value, rewardDatas);
        //
    }

    private IEnumerator ContentsRoutine()
    {
        yield return null;

        SpawnEnemy();

        AutoManager.Instance.StartAutoWithDelay();
    }


    private void SpawnEnemy()
    {
        int stageId = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value;

        var TowerTableData2 = TableManager.Instance.guildTowerTable.dataArray[stageId];
        EnemyTableData spawnEnemyData = GetSpawnedEnemy(stageId);

        for (int i = 0; i < TowerTableData2.Spawnnum; i++)
        {
            poolName = $"Enemy/GuildTower/{spawnEnemyData.Prefabname}";

            var enemyObject = BattleObjectManager.Instance.GetItem(poolName) as Enemy;

            enemyObject.transform.SetParent(enemySpawnPos.transform);

            enemyObject.transform.localPosition = Vector3.zero;

            enemyObject.transform.localScale = Vector3.one * 1.3f;

            enemyObject.SetEnemyDeadCallBack(EnemyDeadCallBack);

            enemyObject.Initialize(spawnEnemyData, updateSubHpBar: true);

            spawnedEnemyList.Add(enemyObject);
        }
    }

    private EnemyTableData GetSpawnedEnemy(int stageId)
    {
        stageId = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value;

        var tableData = TableManager.Instance.guildTowerTable.dataArray[stageId];

        EnemyTableData enemy = new EnemyTableData();

        enemy.Prefabname = tableData.Materialtype;
        enemy.Attackpower = tableData.Attackpower;
        enemy.Movespeed = tableData.Movespeed;
        enemy.Hp = tableData.Hp;
        enemy.Knockbackpower = tableData.Knockbackpower;
        enemy.Defense = (int)tableData.Defense;

        return enemy;
    }


    private void SetClear()
    {
        //보상지급
        int currentFloor = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value;

        var TowerTableData3 = TableManager.Instance.guildTowerTable.dataArray[currentFloor];

        rewardDatas = new List<RewardData>();

        var rewardData = new RewardData((Item_Type)TowerTableData3.Rewardtype, TowerTableData3.Rewardvalue);

        rewardDatas.Add(rewardData);

        List<TransactionValue> transactionList = new List<TransactionValue>();

        HashSet<int> syncDataList = new HashSet<int>();

        //데이터 적용(로컬)
        for (int i = 0; i < rewardDatas.Count; i++)
        {
            if (syncDataList.Contains((int)rewardDatas[i].itemType) == true)
            {
                PopupManager.Instance.ShowConfirmPopup(CommonString.Notice, $"Duplicated tower itemType : {(Item_Type)(int)rewardDatas[i].itemType}", null);
                return;
            }
            else
            {
                syncDataList.Add((int)rewardDatas[i].itemType);
            }

            ServerData.AddLocalValue(rewardDatas[i].itemType, rewardDatas[i].amount);

            //서버 트랙잭션
            var rewardTransactionValue = ServerData.GetItemTypeTransactionValue((Item_Type)(int)rewardDatas[i].itemType);
            transactionList.Add(rewardTransactionValue);
        }

        //단계상승
        ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value++;

        Param floorParam = new Param();

        floorParam.Add(UserInfoTable.currentFloorGuildTower, ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value);

        transactionList.Add(TransactionValue.SetUpdate(UserInfoTable.tableName, UserInfoTable.Indate, floorParam));

        ServerData.SendTransaction(transactionList, successCallBack: () =>
        {
            //  StartCoroutine(AutoPlayRoutine());
        });

        UiGuildMemberList.myCurrentGuildTowerScore = (int)ServerData.userInfoTable.GetTableData(UserInfoTable.currentFloorGuildTower).Value;
        
        //점수 전송
        UpdateGuildTowerScore(UiGuildMemberList.serverRecordedTowerScore);
    }
    
    public void UpdateGuildTowerScore(int serverScore)
    {
        int currentScore = (int)ServerData.userInfoTable.TableDatas[UserInfoTable.currentFloorGuildTower].Value;

        if (currentScore <= serverScore) return;

        int addValue = currentScore - serverScore;

        if (addValue <= 0) return;

        UiGuildMemberList.serverRecordedTowerScore = currentScore;
        
        SendQueue.Enqueue(Backend.Social.Guild.ContributeGoodsV3, goodsType.goods6, addValue, (callback) => 
        {
           Debug.LogError($"Add Guild Score {addValue}");
        });
    }
}