using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] float PlayerSpeed = 5;
    private PlayerControls playerControls;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator myAnimation;
    private SpriteRenderer mySpriteRenderer;
    private KnockBack knockBack;
    AudioManager audioManager;
    public bool isControlPlayer { get; set; } = true;
    private bool isPlayAudio = false;

    private void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        if (isControlPlayer)
        {
            if (knockBack.gettingKnockedBack) { return; }
            FacingDirection();
            Move();
        }
    }
    private void PlayerInput()
    {
        moveVector = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimation.SetFloat("MoveX", moveVector.x);
        myAnimation.SetFloat("MoveY", moveVector.y);
        if (moveVector.x > 0.1 || moveVector.x < -0.1 || moveVector.y > 0.1 || moveVector.y < -0.1)
        {
            if (!isPlayAudio)
            {
                isPlayAudio = true;
                StartCoroutine(PlayAudio());
            }
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveVector * PlayerSpeed * Time.fixedDeltaTime);
    }
    private IEnumerator PlayAudio()
    {
        audioManager.PlaySFX(audioManager.footStep);
        yield return new WaitForSeconds(0.26f);
        isPlayAudio = false;
    }



    private void FacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPos.x)
        {
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }

}
