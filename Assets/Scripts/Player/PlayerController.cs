using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float PlayerSpeed = 5;
    private PlayerControls playerControls;
    private Vector2 moveVector;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void PlayerInput()
    {
        moveVector = playerControls.Movement.Move.ReadValue<Vector2>();
        Debug.Log(moveVector);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveVector * PlayerSpeed * Time.fixedDeltaTime);
    }

}
