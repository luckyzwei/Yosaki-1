using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetRandomPosition : MonoBehaviour
{
        
    [SerializeField]
    protected Rigidbody2D rb;
    
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    [SerializeField] private float _delay = 0;

    [SerializeField] private float _moveSpeed = 5f;
    public bool isMove;
    private Vector2 moveDir;
    
    private Transform playerTr;
    [SerializeField]
    private Transform projectileTr;
    private int _bossId;

    private bool _initiliazed;
    // Update is called once per frame
    //private Coroutine randPositionRoutine;

    private void OnEnable()
    {
        if (_bossId == 0)
        {
            _bossId = GameManager.Instance.bossId;
            playerTr = PlayerMoveController.Instance.transform;
        }
        if (//_bossId == 109||
            _bossId == 110
            )
        {
            SetRandPosition();
        }

        if (_delay > 0)
        {
            SetCoroutine();
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SetRandPositionRoutine());
    }

    public void SetRandPosition()
    {
        float x = Random.Range(_minX, _maxX + 1);
        float y = Random.Range(_minY, _maxY + 1);
        transform.position = new Vector3(x, y, 0);
    }

    public void SetDir()
    {
        moveDir = playerTr.position - transform.position;
    }
    
    private void SetCoroutine()
    {
        StartCoroutine(SetRandPositionRoutine());
    }
    
    private void Update()
    {
        //조조
        if (_bossId == 111||_bossId == 112)
        {
            if (isMove)
            {
                rb.velocity = moveDir.normalized * _moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

    }
    private IEnumerator SetRandPositionRoutine()
    {
        while (true)
        {
            float x = Random.Range(_minX, _maxX + 1);
            float y = Random.Range(_minY, _maxY + 1);
            transform.localPosition = new Vector3(x, y, 0);
            if (_bossId == 111||_bossId == 112)
            {
                SetDir();
                float angle = Mathf.Atan2(moveDir.normalized.y, moveDir.normalized.x) * Mathf.Rad2Deg;
                projectileTr.transform.localPosition = Vector3.zero;
                projectileTr.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
            
            yield return new WaitForSeconds(_delay);;

        }
    }
}
