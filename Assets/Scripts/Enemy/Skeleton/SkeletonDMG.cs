using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonDMG : MonoBehaviour
{
    [SerializeField] private int damageWeapon = 1;
    private bool isTakeDamage = false;
    private bool isInRange = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            isInRange = true;
            if (!isTakeDamage && this.GetComponentInParent<SkeletonController>().transform.GetChild(0).GetChild(0).gameObject.GetComponent<SkeletonSword>().getIsAttack())
            {
                isTakeDamage = true;
                StartCoroutine(TakeDamage(playerHealth));
            }
        }
        else
        {
            isInRange = false;
        }
    }

    private IEnumerator TakeDamage(PlayerHealth playerHealth)
    {
        yield return new WaitForSeconds(0.7f);
        if (isInRange)
        {
            playerHealth.TakeDamage(damageWeapon, this.gameObject.transform);
        }
        yield return new WaitForSeconds(0.3f);
        isTakeDamage = false;
    }
}
