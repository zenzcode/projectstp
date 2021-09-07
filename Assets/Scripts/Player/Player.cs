using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Health))]
public class Player : SingletonMonoBehaviour<Player>
{
    #region variables

    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float playerMovementSpeed = 5;
    [SerializeField] private float jumpForce = 2;
    private bool _isJumping;
    private Vector3 _baseScale;
    private bool _isUsingLight;
    private bool _canUseLight;
    private float _useLightDelay;

    private Animator _animator;

    [SerializeField]
    private GameObject groundCheck;

    [SerializeField] private LayerMask groundLayer;

    private bool _canMove;

    [SerializeField]
    private Light2D showLight;



    #endregion

    protected override void Awake()
    {
        base.Awake();
        _canMove = true;
        _isJumping = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _baseScale = transform.localScale;
    }

    private void Update()
    {
        if (!_canMove) return;
        if (GetIsGrounded() && _isJumping)
        {
            _isJumping = false;
        }
        
        CheckJump();
        CheckAttackTimer();
        Attack();

        if (transform.position.y <= Settings.LowestObjectY)
        {
            //Call Death Event
        }
    }
    
    public void SetCanMove(bool canMove)
    {
        this._canMove = canMove;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        MovePlayer();
    }

    private void CheckAttackTimer()
    {
        if (_isUsingLight) return;

        _useLightDelay -= Time.deltaTime;
        if (_useLightDelay <= 0)
            _canUseLight = true;
        else
            _canUseLight = false;
    }

    private void MovePlayer()
    {
        var moveVector = Vector2.zero;
        var horizontalAxis = Input.GetAxisRaw(Axes.Horizontal);

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

    private void Attack()
    {
        if (!_canUseLight || _isUsingLight) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isUsingLight = true;
            _canUseLight = false;
            _animator.SetTrigger(Settings.PlayerAttackAnimation);
        }
    }

    public void AttackEnd()
    {
        _isUsingLight = false;
        _canUseLight = true;
        _useLightDelay = Settings.MaxAttackDelay;
    }

    private void EnableShowLight()
    {
        showLight.intensity = 1;
    }

    public void DisableShowLight()
    {
        showLight.intensity = 0;
    }

    public void ResetMovementVelocity()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private bool GetIsGrounded()
    {
        var collisions = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.1f, groundLayer);
        return collisions.Length > 0;
    }
}
