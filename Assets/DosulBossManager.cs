using Cinemachine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using CodeStage.AntiCheat.ObscuredTypes;
using static UiRewardView;
using BackEnd;
public class DosulBossManager : ContentsManagerBase
{
   [Header("BossInfo")]
    private BossEnemyBase singleRaidEnemy;
    private AgentHpController bossHpController;

    private BossTableData bossTableData;
    private ReactiveProperty<ObscuredDouble> damageAmount = new ReactiveProperty<ObscuredDouble>();
    private ReactiveProperty<ObscuredDouble> bossRemainHp = new ReactiveProperty<ObscuredDouble>();

    public override Transform GetMainEnemyObjectTransform()
    {
        return singleRaidEnemy.transform;
    }
    
    public override double GetBossRemainHpRatio()
    {
        return damageAmount.Value / bossRemainHp.Value;
    }
    public double BossRemainHp => bossRemainHp.Value;

    public override double GetDamagedAmount()
    {
        return damageAmount.Value;
    }

    [Header("Ui")]
    [SerializeField]
    private TextMeshProUGUI damageIndicator;
    [SerializeField]
    private Animator damagedAnim;
    private string DamageAnimName = "Play";

    [Header("State")]
    private ReactiveProperty<ObscuredInt> contentsState = new ReactiveProperty<ObscuredInt>((int)ContentsState.Fight);

    [SerializeField]
    private UiDosulBossResultPopup uiBossResultPopup;

    [SerializeField]
    private GameObject statusUi;

    [SerializeField]
    private GameObject directionUi;

    [SerializeField]
    private GameObject portalObject;

    [SerializeField]
    private Transform bossSpawnParent;

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
        damageAmount.Value.RandomizeCryptoKey();
        bossRemainHp.Value.RandomizeCryptoKey();
        contentsState.Value.RandomizeCryptoKey();
    }
    #endregion

    protected new void Start()
    {
        base.Start();
        Initialize();
        Subscribe();


    }
    private void Initialize()
    {
        SoundManager.Instance.PlaySound("BossAppear");

        SetBossHp();
    }

    private void Subscribe()
    {
        bossHpController.whenEnemyDamaged.AsObservable().Subscribe(WhenBossDamaged).AddTo(this);
        PlayerStatusController.Instance.whenPlayerDead.Subscribe(e => { WhenPlayerDead(); }).AddTo(this);

        damageAmount.AsObservable().Subscribe(whenDamageAmountChanged).AddTo(this);
        bossRemainHp.AsObservable().Subscribe(WhenBossDamaged).AddTo(this);

        contentsState.AsObservable().Subscribe(WhenBossModeStateChanged).AddTo(this);
    }

    private void WhenBossModeStateChanged(ObscuredInt state)
    {
        if (state != (int)ContentsState.Fight)
        {
            EndBossMode();
        }
    }

    private void SetBossHp()
    {
        bossRemainHp.Value = float.MaxValue;

        var prefab = Resources.Load<BossEnemyBase>($"Boss/DosulBoss");

        singleRaidEnemy = Instantiate<BossEnemyBase>(prefab, bossSpawnParent);
        singleRaidEnemy.transform.localPosition = Vector3.zero;
        singleRaidEnemy.gameObject.SetActive(false);
        bossHpController = singleRaidEnemy.GetComponent<AgentHpController>();
        bossHpController.SetRaidEnemy();
    }

    private float AchiveAmount=0f;
    
    public void GetAchieveReward()
    {
        var rewardedIdx = (int)ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.dosulRewardIdx).Value;

        int currentGradeId = PlayerStats.GetDosulGrade();
        //플레이 X
        if (currentGradeId < 0)
        {
            //PopupManager.Instance.ShowAlarmMessage("등록된 점수가 없습니다");
            return;
        }

        if (currentGradeId <= rewardedIdx)
        {
            //PopupManager.Instance.ShowAlarmMessage("받을 보상이 없습니다!");
            return;
        }

        var tableData = TableManager.Instance.dosulTowerTable.dataArray;

        float sumValue = 0f;
        //받보상 +1부터 현재 단계까지
        for (int i = rewardedIdx + 1; i <= currentGradeId; i++)
        {
            sumValue += tableData[i].Rewardvalue;
        }
        int rewarededindex = (int)ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.dosulRewardIdx).Value;

        if (currentGradeId <= rewarededindex)
        {
            PopupManager.Instance.ShowAlarmMessage("받을 보상이 없습니다!");
            return;
        }

        ServerData.goodsTable.TableDatas[GoodsTable.DosulGoods].Value += sumValue;

        ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.dosulRewardIdx).Value = currentGradeId;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param goodsParam = new Param();
        goodsParam.Add(GoodsTable.DosulGoods, ServerData.goodsTable.TableDatas[GoodsTable.DosulGoods].Value);

        Param userInfo2Param = new Param();
        userInfo2Param.Add(UserInfoTable_2.dosulRewardIdx, ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulRewardIdx].Value);

        transactions.Add(TransactionValue.SetUpdate(GoodsTable.tableName, GoodsTable.Indate, goodsParam));
        transactions.Add(TransactionValue.SetUpdate(UserInfoTable_2.tableName, UserInfoTable_2.Indate, userInfo2Param));

        ServerData.SendTransaction(transactions);
        AchiveAmount = sumValue;
    }

    private void whenDamageAmountChanged(ObscuredDouble hp)
    {
        damageIndicator.SetText(Utils.ConvertBigNum(hp));
        damagedAnim.SetTrigger(DamageAnimName);
    }

    private void WhenBossDamaged(ObscuredDouble hp)
    {
        //  bossHpBar.UpdateHpBar(hp, bossTableData.Hp);

        if (hp <= 0f && contentsState.Value == (int)ContentsState.Fight)
        {
            // WhenBossDead();
        }
    }

    private void WhenBossDamaged(double damage)
    {
        damageAmount.Value -= damage;
        bossRemainHp.Value += damage;
    }
    #region EndConditions
    //클리어조건1 플레이어 사망
    private void WhenPlayerDead()
    {
        if (contentsState.Value != (int)ContentsState.Fight) return;

        contentsState.Value = (int)ContentsState.Dead;
    }

    //클리어조건1 보스 처치 성공
    private void WhenBossDead()
    {
        //클리어 체크
        contentsState.Value = (int)ContentsState.Clear;

        //SendClearInfo();
    }

    //private void SendClearInfo()
    //{
    //    var serverData = ServerData.bossServerTable.TableDatas[bossTableData.Stringid];

    //    if (serverData.clear.Value != 1)
    //    {
    //        serverData.clear.Value = 1;

    //        ServerData.bossServerTable.UpdateData(bossTableData.Stringid);
    //    }
    //}

    //클리어조건1 타이머 종료
    protected override void TimerEnd()
    {
        base.TimerEnd();
        contentsState.Value = (int)ContentsState.TimerEnd;
    }
    #endregion

    private void EndBossMode()
    {
        //공격루틴 제거 + 클리어면 이펙트 켜주던지.?
        singleRaidEnemy.gameObject.SetActive(false);

        //타이머 종료
        if (contentsState.Value != (int)ContentsState.TimerEnd)
        {
            StopTimer();
        }

        //점수 전송
        SendScore();

        //보상팝업
        ShowResultPopup();
    }

    private void SendScore()
    {
        double reqValue = damageAmount.Value * GameBalance.BossScoreSmallizeValue;

        if (reqValue > ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulScore].Value)
        {
            ServerData.userInfoTable_2.TableDatas[UserInfoTable_2.dosulScore].Value = reqValue;

            ServerData.userInfoTable_2.UpData(UserInfoTable_2.dosulScore, false);
        }


    }

    private void ShowResultPopup()
    {
        //결과 UI
        GetAchieveReward();
        uiBossResultPopup.gameObject.SetActive(true);
        statusUi.SetActive(false);
        uiBossResultPopup.Initialize(damageAmount.Value,AchiveAmount);
    }

    protected override IEnumerator ModeTimer()
    {
        while (direciontEnd == false)
        {
            yield return null;
        }
        directionUi.SetActive(false);
        singleRaidEnemy.gameObject.SetActive(true);

        AutoManager.Instance.StartAutoWithDelay();

        portalObject.gameObject.SetActive(false);

        float remainSec = playTime;

        while (remainSec >= 0)
        {
            timerText.SetText($"남은시간 : {(int)remainSec}");
            yield return null;
            remainSec -= Time.deltaTime;
            this.remainSec = remainSec;
        }

        TimerEnd();
    }

    private ObscuredBool direciontEnd = false;

    public void WhenDirectionAnimEnd()
    {
        direciontEnd = true;
    }
    
}
