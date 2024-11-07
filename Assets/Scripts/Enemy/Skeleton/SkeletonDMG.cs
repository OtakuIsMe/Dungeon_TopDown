using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonDMG : MonoBehaviour
{
    [SerializeField] private int damageWeapon = 1;
    private bool isTakeDamage = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && !isTakeDamage && SkeletonSword.Instance.getIsAttack())
        {
            isTakeDamage = true;
            StartCoroutine(TakeDamage(playerHealth));
        }
    }

    private IEnumerator TakeDamage(PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(0.7f);
        playerHealth.TakeDamage(damageWeapon);
        yield return new WaitForSeconds(0.3f);
        isTakeDamage = false;
    }
}
