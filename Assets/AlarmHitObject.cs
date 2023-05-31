using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlarmHitObject : MonoBehaviour
{
    private double damage = 10;

    [SerializeField]
    private Animator animator;

    public void AttackStart()
    {
        this.gameObject.SetActive(true);
        animator.SetTrigger("Attack");
    }

    float percentDamage = 0f;

    public void SetDamage(double damage, float percentDamage = 0f)
    {
        this.damage = damage;
        this.percentDamage = percentDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(Tags.Player) == false) return;

        PlayerStatusController.Instance.UpdateHp(-damage, percentDamage);
    }

    public void Shuffle()
    {
        float randomXIndex = Random.Range(8, 23);
        float randY = Random.Range(0, 3);
        transform.localPosition = new Vector3(randomXIndex, -7.87f + randY * 5.62f, 0);
    }

    public void MoveToPlayer()
    {
        if (GameManager.Instance.bossId == 146||GameManager.Instance.bossId == 147||GameManager.Instance.bossId == 148||GameManager.Instance.bossId == 149)
        {
            transform.Rotate(0, 0, 90);
            transform.position = PlayerMoveController.Instance.transform.position;
        }
    }

    private bool isMove = false;
    private Coroutine movementCoroutine;
    public float movementSpeed = 20f; // 이동 속

    public void StartMovement()
    {
        isMove = true;
        movementCoroutine = StartCoroutine(MoveToTarget());
    }

    public void StopMovement()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        isMove = false;
    }

    IEnumerator MoveToTarget()
    {
        Vector3 startPosition = transform.position;
        Vector3 target = PlayerMoveController.Instance.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            if (!isMove)
            {
                yield break; // 이동 멈춤 시 Coroutine 종료
            }

            elapsedTime += Time.deltaTime * movementSpeed;
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime);
            yield return null;
        }

        // 이동 완료
        isMove = false;
    }
}
