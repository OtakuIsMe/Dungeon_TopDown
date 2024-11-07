using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 3;
    [SerializeField] private GameObject gameOverObject;
    private int playerHealth;

    private Slider healthSlider;
    public KnockBack knockBack;
    public Animator myAnimator;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerHealth = playerMaxHealth;
        UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SkeletonController skeleton = other.gameObject.GetComponent<SkeletonController>();
        if (skeleton != null && !knockBack.gettingKnockedBack)
        {
            TakeDamage(1);
            knockBack.GetKnockBack(other.gameObject.transform);
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!knockBack.gettingKnockedBack)
        {
            playerHealth -= damage;
            UpdateHealthSlider();
            if (playerHealth > 0)
            {
                knockBack.GetKnockBack(SkeletonController.Instance.transform);
            }
            else
            {
                StartCoroutine(DeathAnimation());
            }
        }
    }
    public void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = playerMaxHealth;
        healthSlider.value = playerHealth;
    }

    private IEnumerator DeathAnimation()
    {
        playerHealth = 0;
        this.transform.GetChild(0).gameObject.SetActive(false);
        SkeletonSword.Instance.stopAttack = true;
        PlayerController.Instance.isControlPlayer = false;
        myAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        gameOverObject.SetActive(true);
    }
}
