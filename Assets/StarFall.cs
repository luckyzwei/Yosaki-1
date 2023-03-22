using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StarFall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private Vector3 FirstPosition = Vector3.zero;
    public void Initialize(Vector3 moveDir, float velocity)
    {
        rb.velocity = moveDir.normalized * velocity;

        FirstPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize(Vector3.down , 3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 10)
        {
            var randX = UnityEngine.Random.Range(-15, 26);
            this.gameObject.transform.position = new Vector3(randX, FirstPosition.y, 0); 
            rb.velocity = Vector3.down.normalized * 3f;
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
