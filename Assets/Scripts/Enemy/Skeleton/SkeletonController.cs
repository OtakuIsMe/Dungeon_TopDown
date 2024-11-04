using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SkeletonController : Singleton<SkeletonController>
{
    private Vector2 EnemyUserVector;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector3 enemyPlayerVector;
    private SpriteRenderer mySpriteRenderer;
    protected void Awake()
    {
        base.Awake();
        GameObject player = GameObject.Find("Player");
        if (player)
        {
            playerTransform = player.transform;
            enemyTransform = GetComponent<Transform>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        NavigateEnemy();
    }

    private void NavigateEnemy()
    {
        Vector3 enemyVector = enemyTransform.position;
        Vector3 playerVector = playerTransform.position;
        Vector3 enemyPlayerVector = playerVector - enemyVector;
        if (enemyPlayerVector.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }
}
