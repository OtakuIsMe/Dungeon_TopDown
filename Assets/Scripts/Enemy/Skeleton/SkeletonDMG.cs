using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDMG : MonoBehaviour
{
    [SerializeField] private int damageWeapon = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
        }
    }
}
