using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StarMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private Vector3 FirstPosition = Vector3.zero;
    [SerializeField] private Vector3 moveDirection = Vector3.down;
    [SerializeField]private float speed = 3f;
    public void Initialize(Vector3 moveDir, float velocity)
    {
        rb.velocity = moveDir.normalized * velocity;


        if (GameManager.contentsType == GameManager.ContentsType.FoxTower)
        {
            FirstPosition = transform.localPosition;
        }
        else
        {
            FirstPosition = transform.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize(moveDirection , speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 19)
        {
            if (GameManager.contentsType == GameManager.ContentsType.DarkTower)
            {
                var randY= UnityEngine.Random.Range(-0.54f, 7.97f);
                var randSpeed= UnityEngine.Random.Range(-10f, 5f);
                this.gameObject.transform.localPosition = new Vector3(Mathf.Max(-7, FirstPosition.x), randY, 0);
                rb.velocity = moveDirection.normalized * (speed + randSpeed);
            }
            else if (GameManager.contentsType == GameManager.ContentsType.FoxTower)
            {
                this.gameObject.transform.localPosition = new Vector3(Mathf.Max(-7, FirstPosition.x), FirstPosition.y, 0);
                rb.velocity = moveDirection.normalized * (speed);
            }
        }
        
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.layer == 10)
    //     {
    //         var randX = UnityEngine.Random.Range(-15, 26);
    //         this.gameObject.transform.position = new Vector3(randX, FirstPosition.y, 0); 
    //         rb.velocity = Vector3.down.normalized * 3f;
    //     }
    // }
}
