using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Singleton<Sword>, IWeapon
{
    [SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private Transform weaponCollider;
    private Animator myAnimator;
    private ActiveWeapon activeWeapon;
    private PlayerController playerController;
    public bool isAttack { get; set; } = false;
    AudioManager audioManager;

    private void Awake()
    {
        base.Awake();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
    }

    void Update()
    {
        if (PlayerController.Instance.isControlPlayer)
        {
            MouseFollowWithOffset();
        }
    }
    public void Attack()
    {
        myAnimator.SetBool("IsAttack", true);
        isAttack = true;
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        audioManager.PlaySFX(audioManager.swing);
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
        myAnimator.SetBool("IsAttack", false);
        isAttack = false;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
