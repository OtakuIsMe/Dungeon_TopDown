using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    [SerializeField] private float CDWaitBeforeAttack = 1f;
    [SerializeField] private float CDAttack = 0.5f;
    [SerializeField] private Transform weaponCollider;

    private ActiveWeaponSkeleton activeWeaponSkeleton;
    private SkeletonController skeletonController;
    private Animator myAnimator;
    private bool isAttack = false;
    public bool isTakeDamage = false;
    public bool stopAttack { get; set; } = false;
    AudioManager audioManager;
    protected void Awake()
    {
        activeWeaponSkeleton = GetComponentInParent<ActiveWeaponSkeleton>();
        skeletonController = GetComponentInParent<SkeletonController>();
        myAnimator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

    public void Attack()
    {
        if (!isAttack && !stopAttack)
        {
            isAttack = true;
            StartCoroutine(WaitBeforeStart());
        }
    }

    private IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(CDWaitBeforeAttack);
        myAnimator.SetBool("IsAttack", true);
        audioManager.PlaySFX(audioManager.swing);
        isTakeDamage = true;
        yield return new WaitForSeconds(CDAttack);
        myAnimator.SetBool("IsAttack", false);
        isAttack = false;
        isTakeDamage = false;
    }

    public void setIsAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
    public bool getIsAttack()
    {
        return isAttack;
    }
}
