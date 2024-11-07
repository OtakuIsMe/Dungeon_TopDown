using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private bool isTakeDamage = false;
    private bool isInRange = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        SkeletonHealth skeletonHealth = other.gameObject.GetComponent<SkeletonHealth>();
        if (skeletonHealth != null)
        {
            isInRange = true;
            if (!isTakeDamage && Sword.Instance.isAttack)
            {
                isTakeDamage = true;
                StartCoroutine(TakeDamage(skeletonHealth));
            }
        }
        else
        {
            isInRange = false;
        }
    }

    private IEnumerator TakeDamage(SkeletonHealth skeletonHealth)
    {
        yield return new WaitForSeconds(0.2f);
        if (isInRange)
        {
            skeletonHealth.TakeDamage(damage);
        }
        yield return new WaitForSeconds(0.3f);
        isTakeDamage = false;
    }
}
