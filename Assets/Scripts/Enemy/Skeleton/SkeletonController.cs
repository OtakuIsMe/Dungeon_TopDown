using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float OutSight = 10f;
    private Transform playerTransform;
    private Transform enemyTransform;
    public Vector3 enemyPlayerVector;
    private SpriteRenderer mySpriteRenderer;
    private Vector3 moveVector;
    private float distance;
    private float oldSpeed;
    private Animator myAnimator;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    protected void Awake()
    {
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            playerTransform = player.transform;
            enemyTransform = GetComponent<Transform>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        oldSpeed = speed;
        distance = (playerTransform.position - enemyTransform.position).magnitude;
    }

    private void Update()
    {
        if (knockBack.gettingKnockedBack) { return; }
        NavigateEnemy();
        MoveEnemy();
        CheckAttack();
    }

    private void NavigateEnemy()
    {
        Vector3 enemyVector = enemyTransform.position;
        Vector3 playerVector = playerTransform.position;
        enemyPlayerVector = playerVector - enemyVector;
        if (enemyPlayerVector.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

    private void MoveEnemy()
    {
        distance = (playerTransform.position - enemyTransform.position).magnitude;
        if (!this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SkeletonSword>().getIsAttack() && distance > 2)
        {
            Vector3 enemyVector = enemyTransform.position;
            Vector3 TargetMoveLeft = playerTransform.position;
            TargetMoveLeft.x -= 2;
            Vector3 TargetMoveRight = playerTransform.position;
            TargetMoveRight.x += 2;
            if ((TargetMoveLeft - enemyVector).sqrMagnitude <= (TargetMoveRight - enemyVector).sqrMagnitude)
            {
                moveVector = TargetMoveLeft;
            }
            else
            {
                moveVector = TargetMoveRight;
            }
            if (distance <= OutSight)
            {
                speed = oldSpeed;
            }
            else
            {
                speed = 0;
            }

            myAnimator.SetFloat("Speed", speed);

            Vector2 direction = ((Vector2)moveVector - rb.position).normalized;

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    private void CheckAttack()
    {
        if (distance <= 2)
        {
            rb.velocity = Vector2.zero;
            this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<SkeletonSword>().Attack();
            speed = 0;
            myAnimator.SetFloat("Speed", speed);
        }
    }

}
