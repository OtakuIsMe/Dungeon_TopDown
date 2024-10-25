using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 5;
    private PlayerControls playerControls;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator myAnimation;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        FacingDirection();
        Move();
    }
    private void PlayerInput()
    {
        moveVector = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimation.SetFloat("MoveX", moveVector.x);
        myAnimation.SetFloat("MoveY", moveVector.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveVector * PlayerSpeed * Time.fixedDeltaTime);
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
