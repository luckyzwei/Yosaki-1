using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;
using UnityEngine.UI.Extensions;
using Random = UnityEngine.Random;

public class KingBossMoveController : MonoBehaviour
{
    private float moveSpeed = 0f;

    private Vector3 moveDir;

    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField]
    protected Rigidbody2D rb;

    private bool initialized = false;


    private bool isDamaged = false;

    private Transform playerTr;

    
    [SerializeField]
    private Transform viewTr;
    [SerializeField]
    private Transform targetTransform;

    [SerializeField] private CapsuleCollider2D _capsuleCollider2D;
    
    [SerializeField] private float _moveSpeed = 1;
    private int _bossId;
    public bool isMoving;

    private void Start()
    {
        _bossId = GameManager.Instance.bossId;
        playerTr = PlayerMoveController.Instance.transform;

        InitializePattern();
    }

    private void OnDisable()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }

    public void ShowTip()
    {
        if (GameManager.Instance.bossId == 155)
        {
            if(ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).Value<GameBalance.TwelveBoss_155_RequireTower10)
            {
                
                PopupManager.Instance.ShowAlarmMessage2($"{TableManager.Instance.TwelveBossTable.dataArray[155].Name}의 힘에 의해 시야가 차단됩니다.\n시야 차단을 풀기 위해선 상단전 {GameBalance.TwelveBoss_155_RequireTower10}단계 개방이 필요합니다!");
            }
        }
        else if (GameManager.Instance.bossId == 156)
        {
            if(ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).Value<GameBalance.TwelveBoss_156_RequireTower10)
            {
                PopupManager.Instance.ShowAlarmMessage2($"{TableManager.Instance.TwelveBossTable.dataArray[156].Name}의 힘에 의해 시야가 차단됩니다.\n시야 차단을 풀기 위해선 상단전 {GameBalance.TwelveBoss_156_RequireTower10}단계 개방이 필요합니다!");
            }
        }
        else if (GameManager.Instance.bossId == 157)
        {
            if(ServerData.userInfoTable_2.GetTableData(UserInfoTable_2.gyungRockTower3).Value<GameBalance.TwelveBoss_157_RequireTower10)
            {
                PopupManager.Instance.ShowAlarmMessage2($"{TableManager.Instance.TwelveBossTable.dataArray[157].Name}의 힘에 의해 시야가 차단됩니다.\n시야 차단을 풀기 위해선 상단전 {GameBalance.TwelveBoss_157_RequireTower10}단계 개방이 필요합니다!");
            }
        }
    }

    public void InitializePattern()
    {
        if (_bossId == 109)
        {
            isMoving = true;
            var transform1 = transform;
            transform1.localPosition = targetTransform.localPosition;

            SetMoveDir(playerTr.position  - (transform1.position));

            if (initialized == false)
            {
                initialized = true;
            }
        }
        else if (_bossId == 110)
        {
            isMoving = true;

            SetMoveDir(playerTr.position - transform.position);

            if (initialized == false)
            {
                initialized = true;
            }
        }
        else if (_bossId == 154||_bossId == 155||_bossId == 157)
        {
            isMoving = true;

            SetMoveDir(playerTr.position - transform.position);
            _skeletonAnimation.AnimationState.SetAnimation(0, "run", true);
            if (initialized == false)
            {
                initialized = true;
            }
        }

    }



    private void Update()
    {
        //측천무후
         if (_bossId == 109)
        {
            if (isMoving)
            {
                rb.velocity = moveDir.normalized * moveSpeed;
            }
        }

          //항우
        else if (_bossId == 110)
        {
            if (isMoving)
            {
                rb.velocity = moveDir.normalized * moveSpeed;
            }
        }

         else if (_bossId == 154||_bossId == 155||_bossId == 157)
         {
             if (isMoving)
             {
                 rb.velocity = (playerTr.position - transform.position).normalized * moveSpeed;
             }
         }

        // float playerDist = Vector3.Distance(playerTr.position, this.transform.position);
            //
            // if (playerDist >= 0.1f) 
            // {
            //     Vector3 moveDir = playerTr.position - this.transform.position;
            //     rb.velocity = moveDir.normalized * moveSpeed * 1.5f;
            // }


            if (_bossId == 154||_bossId == 155||_bossId == 157)
            {
                viewTr.transform.localScale = new Vector3(rb.velocity.x < 0 ? -1 : 1, 1, 1);   
            }
            else
            {
                viewTr.transform.localScale = new Vector3(rb.velocity.x > 0 ? -1 : 1, 1, 1);
            }

    }

    private Coroutine moveCoroutine;
    
    private void MoveToPlayer()
    {
        moveCoroutine = StartCoroutine(MoveAttackToPlayer());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator MoveAttackToPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            
            _capsuleCollider2D.enabled = false;
            transform.position = playerTr.position;
            _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
            yield return new WaitForSeconds(0.3f);
            _capsuleCollider2D.enabled = true;   
            _skeletonAnimation.AnimationState.SetAnimation(0, "attack3", false);
        }
    }

    private void SetAnimationIdle()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
    }
    
    private void StopMove()
    {
        rb.velocity=Vector2.zero;
        isMoving = false;
    }
    private void SetMoveDir(Vector3 moveDir)
    {
        this.moveSpeed = _moveSpeed;
        this.moveDir = moveDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDamaged == true) return;

        // if (collision.gameObject.layer == LayerMask.NameToLayer(EnemyMoveController.DefenseWall_str))
        // {
        //     rb.velocity = -moveDir.normalized * moveSpeed;
        // }
    }
}
