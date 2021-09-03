using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SingletonMonoBehaviour<PlayerMovement>
{
    #region variables

    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float playerMovementSpeed = 5;
    [SerializeField] private float jumpForce = 2;
    private bool _isJumping;

    [SerializeField]
    private GameObject groundCheck;

    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] public bool canMove;


    #endregion

    protected override void Awake()
    {
        base.Awake();
        canMove = true;
        _isJumping = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove) return;
        if (GetIsGrounded() && _isJumping)
        {
            _isJumping = false;
        }
        
        CheckJump();

        if (transform.position.y <= Settings.LowestObjectY)
        {
            //Call Death Event
        }

    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        var moveVector = Vector2.zero;
        var horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (horizontalAxis == 0)
        {
            moveVector = new Vector2(0, _rigidbody2D.velocity.y);
        }
        else
        {
            moveVector = new Vector2(horizontalAxis * playerMovementSpeed, _rigidbody2D.velocity.y);
        }

        _rigidbody2D.velocity = moveVector;
    }

    private void CheckJump()
    {
        if (GetIsGrounded() && !_isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                PerformJump();
            }
        }
    }

    private void PerformJump()
    {
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity.y = jumpForce;
        _rigidbody2D.velocity = currentVelocity;
        _isJumping = true;
    }

    private bool GetIsGrounded()
    {
        var collisions = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.1f, groundLayer);
        return collisions.Length > 0;
    }
}
