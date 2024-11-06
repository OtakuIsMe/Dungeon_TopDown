using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    [SerializeField] private float CDWaitBeforeAttack = 0.3f;
    [SerializeField] private float CDAttack = 0.5f;
    [SerializeField] private Transform weaponCollider;

    private ActiveWeaponSkeleton activeWeaponSkeleton;
    private SkeletonController skeletonController;

    private void Awake()
    {
        activeWeaponSkeleton = GetComponentInParent<ActiveWeaponSkeleton>();
        skeletonController = GetComponentInParent<SkeletonController>();
    }

    private void Start()
    {

    }
    private void Update()
    {
        EnemyFollowWithOffset();
    }

    private void EnemyFollowWithOffset()
    {
        var xPosition = skeletonController.enemyPlayerVector.x;
        if (xPosition < 0)
        {
            activeWeaponSkeleton.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeaponSkeleton.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
