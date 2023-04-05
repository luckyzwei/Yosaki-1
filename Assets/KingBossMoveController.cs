using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

public class KingBossMoveController : MonoBehaviour
{
    private float moveSpeed = 0f;

    private Vector3 moveDir;

    [SerializeField]
    protected Rigidbody2D rb;

    private bool initialized = false;

    [SerializeField]
    private AgentHpController agentHpController;

    private bool isDamaged = false;

    private Transform playerTr;

    
    [SerializeField]
    private Transform viewTr;
    [SerializeField]
    private Transform targetTransform;

    [SerializeField] private float _moveSpeed = 1;
    private int _bossId;
    public float stoppingDistance = 0.1f;
    public ReactiveProperty<bool> isMoving;

    private void Start()
    {
        _bossId = GameManager.Instance.bossId;
        playerTr = PlayerMoveController.Instance.transform;

        InitializePattern();
    }


    public void InitializePattern()
    {
        if (_bossId == 109)
        {
            isMoving.Value = true;
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
            isMoving.Value = true;

            SetMoveDir(playerTr.position - transform.position);

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
            if (isMoving.Value)
            {
                rb.velocity = moveDir.normalized * moveSpeed;
            }
        }

          //항우
        else if (_bossId == 110)
        {
            if (isMoving.Value)
            {
                rb.velocity = moveDir.normalized * moveSpeed;
            }
        }


        // float playerDist = Vector3.Distance(playerTr.position, this.transform.position);
            //
            // if (playerDist >= 0.1f) 
            // {
            //     Vector3 moveDir = playerTr.position - this.transform.position;
            //     rb.velocity = moveDir.normalized * moveSpeed * 1.5f;
            // }

        

        viewTr.transform.localScale = new Vector3(rb.velocity.x > 0 ? -1 : 1, 1, 1);
    }

    private void StopMove()
    {
        rb.velocity=Vector2.zero;
        isMoving.Value = false;
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
