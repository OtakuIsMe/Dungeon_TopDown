using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : Singleton<SkeletonHealth>
{
    [SerializeField] private int skeletonMaxHealth = 3;

    private int currentHealth;

    private KnockBack knockBack;
    private Animator myAnimator;


    private void Awake()
    {
        base.Awake();
        knockBack = GetComponent<KnockBack>();
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = skeletonMaxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (!knockBack.gettingKnockedBack)
        {
            currentHealth -= damage;
            if (currentHealth > 0)
            {
                Debug.Log("Knock");
                knockBack.GetKnockBack(PlayerController.Instance.transform);
            }
            else
            {
                StartCoroutine(DeathAnimation());
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        currentHealth = 0;
        this.transform.GetChild(0).gameObject.SetActive(false);
        myAnimator.SetTrigger("Death");
        SkeletonSword.Instance.stopAttack = true;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
