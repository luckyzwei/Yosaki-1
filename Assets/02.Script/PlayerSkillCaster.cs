using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using CodeStage.AntiCheat.ObscuredTypes;

public enum SkillCastType
{
    Player,
    Son,
    Four,
    Vision,
    SuhoAnimal,
    Indra,
}


public class PlayerSkillCaster : SingletonMono<PlayerSkillCaster>
{
    [SerializeField]
    private PlayerMoveController playerMoveController;

    public PlayerMoveController PlayerMoveController => playerMoveController;

    public Dictionary<int, SkillBase> UserSkills { get; private set; } = new Dictionary<int, SkillBase>();

    public bool isSkillMoveRestriction = false;

    private ObscuredBool ignoreDamDecrease = false;

    private float addRange = 0f;

    private string newWeaponKey1 = "weapon23";
    private string newWeaponKey2 = "weapon24";

    private int addChargeCount = 1;
    public ReactiveProperty<int> visionChargeCount;
    public ReactiveProperty<bool> useVisionSkill;

    private static readonly List<int> _visionSkillIdxList= new List<int>();

    public List<int> GetVisionSkillIdxList()
    {
        return _visionSkillIdxList;
    }
    public static bool IsVisionSkill(int idx)
    {
        return _visionSkillIdxList.Contains(idx);
    }

    private void InitializeVisionSkillIdxList()
    {
        var skillData = TableManager.Instance.SkillTable.dataArray;
        foreach (var data in skillData)
        {
            if (data.SKILLCASTTYPE == SkillCastType.Vision)
            {
                _visionSkillIdxList.Add(data.Id);
                
            }
        }
    }
    public bool UseSkill(int skillIdx)
    {
        bool canUserSkill = UserSkills[skillIdx].CanUseSkill();

        if (canUserSkill)
        {
            UserSkills[skillIdx].UseSkill();

            // visionSkill
            if (useVisionSkill.Value == false &&
                (TableManager.Instance.SkillTable.dataArray[skillIdx].Requirehit < 0) &&
                (visionChargeCount.Value > 0)
               )
            {
                if (GameManager.contentsType == GameManager.ContentsType.NormalField)
                {
                    if (!MapInfo.Instance.canSpawnEnemy.Value)
                    {
                            visionChargeCount.Value-=addChargeCount;
                        
                    }
                }
                else
                {
                    visionChargeCount.Value-=addChargeCount;
                }
            }

            if (IsVisionSkill(skillIdx))
            {
                UiUltiSkillEffect.Instance.ShowUltSkillEffect(skillIdx);
            }

            //
        }

        return canUserSkill;
    }

    public void InitializeVisionSkill()
    {
        int visionIdx = ServerData.goodsTable.GetVisionSkillIdx();

        visionChargeCount.Value = TableManager.Instance.SkillTable.dataArray[visionIdx].Requirehit;


        useVisionSkill.Value = false;
        
        VisionSkillCaster.Instance.StartSkillRoutine();
    }

    public void SetUseVisionSkill(bool isUsed)
    {
        useVisionSkill.Value = isUsed;
    }

    protected virtual void Awake()
    {
        base.Awake();
        InitializeVisionSkillIdxList();
    }

    private void Start()
    {
        
        visionChargeCount.Value = 0;
        useVisionSkill.Value = false;

        InitSkill();
        ignoreDamDecrease = ServerData.userInfoTable.TableDatas[UserInfoTable.IgnoreDamDec].Value == 1;
        InitializeVisionSkill();
        Subscribe();
    }

    private void Subscribe()
    {
        ServerData.weaponTable.TableDatas[newWeaponKey1].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                addRange = 10;
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas[newWeaponKey2].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                addRange = 10;
            }
        }).AddTo(this);


        ServerData.userInfoTable.TableDatas[UserInfoTable.currentFloorIdx7].AsObservable().Subscribe(e =>
        {
            if (e >= 10)
            {
                addChargeCount = 2;
            }
                        
        }).AddTo(this);
    }

    public void SetMoveRestriction(float time)
    {
        if (time == 0f) return;

        StartCoroutine(MoveRestrictionRoutine(time));
    }

    private IEnumerator MoveRestrictionRoutine(float time)
    {
        isSkillMoveRestriction = true;
        yield return new WaitForSeconds(time);
        isSkillMoveRestriction = false;
    }

    private void InitSkill()
    {
        for (int i = 0; i < TableManager.Instance.SkillTable.dataArray.Length; i++)
        {
            var SkillTableData = TableManager.Instance.SkillTable.dataArray[i];

            if (ServerData.skillServerTable.HasSkill(SkillTableData.Id))
            {
                Type elementType = Type.GetType(SkillTableData.Skillclassname);

                object classType = Activator.CreateInstance(elementType);

                var skillBase = classType as SkillBase;

                skillBase.Initialize(this.transform, SkillTableData, this);

                UserSkills.Add(SkillTableData.Id, skillBase);
            }
        }
    }

    public Collider2D[] GetEnemiesInCircle(Vector2 origin, float radius)
    {
        return Physics2D.OverlapCircleAll(origin, radius + addRange, LayerMasks.EnemyLayerMask);
    }

    public RaycastHit2D[] GetEnemiesInRaycast(Vector2 origin, Vector2 rayDirection, float length)
    {
        return Physics2D.RaycastAll(origin, rayDirection, length, LayerMasks.EnemyLayerMask);
    }

    public RaycastHit2D[] GetEnemiesInBoxcast(Vector2 origin, Vector2 rayDirection, float length, float size)
    {
        return Physics2D.BoxCastAll(origin, Vector2.one * (size + addRange), 0f, rayDirection, length, LayerMasks.EnemyLayerMask);
    }

    private string wallString = "Wall";

    public Vector2 GetRayHitWallPoint(Vector2 origin, Vector2 rayDirection, float length)
    {
        int hitLayer = LayerMasks.PlatformLayerMask_Ray + LayerMasks.EnemyWallLayerMask_Ray;

        var rayHits = Physics2D.RaycastAll(origin, rayDirection, length, hitLayer);

        for (int i = 0; i < rayHits.Length; i++)
        {
            if (rayHits[i].collider.gameObject.tag.Equals(wallString))
            {
                return rayHits[i].point - rayDirection.normalized * 0.5f;
            }
        }

        return Vector2.zero;
    }

    public Vector2 GetRayHitPlatformPoint(Vector2 origin, Vector2 rayDirection, float length, bool ignoreEnemyWall = false)
    {
        int hitLayer = 0;

        if (ignoreEnemyWall == false)
        {
            hitLayer = LayerMasks.PlatformLayerMask_Ray + LayerMasks.EnemyWallLayerMask_Ray;
        }
        else
        {
            hitLayer = LayerMasks.PlatformLayerMask_Ray;
        }

        var rayHits = Physics2D.RaycastAll(origin, rayDirection, length, hitLayer);

        for (int i = 0; i < rayHits.Length; i++)
        {
            return rayHits[i].point;
        }

        return Vector2.zero;
    }


    public void PlayAttackAnim()
    {
        PlayerViewController.Instance.SetCurrentAnimation(PlayerViewController.AnimState.attack);
    }

    public Vector3 GetSkillCastingPosOffset(SkillTableData tableData)
    {
        return tableData.Activeoffset * Vector2.right * (playerMoveController.MoveDirection == MoveDirection.Right ? 1 : -1);
    }

    private Dictionary<int, AgentHpController> agentHpControllers = new Dictionary<int, AgentHpController>();
    private Dictionary<double, double> calculatedDamage = new Dictionary<double, double>();
    private Dictionary<double, double> calculatedDamage_critical = new Dictionary<double, double>();
    private Dictionary<double, double> calculatedDamage_superCritical = new Dictionary<double, double>();

    public IEnumerator ApplyDamage(Collider2D hitEnemie, SkillTableData skillInfo, double damage, bool playSound)
    {
        AgentHpController agentHpController;

        int instanceId = hitEnemie.GetInstanceID();

        if (agentHpControllers.ContainsKey(instanceId) == false)
        {
            agentHpControllers.Add(instanceId, hitEnemie.gameObject.GetComponent<AgentHpController>());

            agentHpController = agentHpControllers[instanceId];
        }
        else
        {
            agentHpController = agentHpControllers[instanceId];
        }

        int hitCount = 0;


        if (skillInfo.Id != 18)
        {
            hitCount = skillInfo.Hitcount + PlayerStats.GetSkillHitAddValue();
        }
        //인드라는 추가타X
        else
        {
            hitCount = skillInfo.Hitcount;
        }

        double defense = agentHpController.Defense + 1;

        bool isCritical = PlayerStats.ActiveCritical();
        bool isSuperCritical = PlayerStats.ActiveSuperCritical();

        double key = damage * defense;

        double calculatedDam = 0;

        if (isCritical)
        {
            //슈퍼크리
            if (isSuperCritical)
            {
                if (calculatedDamage_superCritical.ContainsKey(key) == false)
                {
                    agentHpController.ApplyDefense(ref damage);

                    agentHpController.ApplyPlusDamage(ref damage, isCritical, isSuperCritical);

                    calculatedDamage_superCritical.Add(key, damage);
                }

                calculatedDam = calculatedDamage_superCritical[key];
            }
            //그냥크리
            else
            {
                if (calculatedDamage_critical.ContainsKey(key) == false)
                {
                    agentHpController.ApplyDefense(ref damage);

                    agentHpController.ApplyPlusDamage(ref damage, isCritical,
                        isSuperCritical);

                    calculatedDamage_critical.Add(key, damage);
                }

                calculatedDam = calculatedDamage_critical[key];
            }
        }
        //노크리
        else
        {
            if (calculatedDamage.ContainsKey(key) == false)
            {
                agentHpController.ApplyDefense(ref damage);

                agentHpController.ApplyPlusDamage(ref damage, isCritical, isSuperCritical);

                calculatedDamage.Add(key, damage);
            }

            calculatedDam = calculatedDamage[key];
        }

        bool spawnDamText = SettingData.ShowDamageFont.Value == 1;

        double totalDamage = calculatedDam * hitCount;

        //데미지는 한프레임에 적용
        if (agentHpController.gameObject == null || agentHpController.gameObject.activeInHierarchy == false)
        {
            yield break;
        }
        else
        {
            agentHpController.UpdateHp(-totalDamage);
        }


        //이펙트는 최대 10개까지만 출력
        for (int hit = 0; hit < hitCount && hit < 10; hit++)
        {
            if (spawnDamText)
            {
                agentHpController.SpawnDamText(isCritical, isSuperCritical, calculatedDam);
            }

            //사운드
            //시전할때 사운드 있어서 따로재생X
            if (hit != 0 && playSound)
            {
                SoundManager.Instance.PlaySound(skillInfo.Soundname);
            }

            //이펙트
            if (string.IsNullOrEmpty(skillInfo.Hiteffectname) == false &&
                Vector3.Distance(this.transform.position, hitEnemie.gameObject.transform.position) < GameBalance.effectActiveDistance)
            {
                Vector3 spawnPos = hitEnemie.gameObject.transform.position + Vector3.forward * -1f + Vector3.up * 0.3f;
                spawnPos += (Vector3)UnityEngine.Random.insideUnitCircle * 0.5f;
                spawnPos += (Vector3)Vector3.back;
                EffectManager.SpawnEffectAllTime(skillInfo.Hiteffectname, spawnPos, limitSpawnSize: true);
            }

            float tick = 0f;

            while (tick < 0.05f)
            {
                tick += Time.deltaTime;
                yield return null;
            }
        }

        if (calculatedDamage.Count > 1000)
        {
            calculatedDamage.Clear();
        }

        if (calculatedDamage_critical.Count > 1000)
        {
            calculatedDamage_critical.Clear();
        }

        if (calculatedDamage_superCritical.Count > 1000)
        {
            calculatedDamage_superCritical.Clear();
        }
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}