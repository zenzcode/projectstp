using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool _hasLeftGround = false;
    private Vector3 _baseScale;
    private bool _isUsingLight;
    private bool _canUseLight;
    private float _useLightDelay;

    private bool _isInvincible;
    private float _invincibleTimer;

    private Coroutine UntilDieRoutine = null;

    public bool IsInvincible
    {
        get => _isInvincible;
    }

    private Collider2D[] _colliders;
    
    private Animator _animator;

    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject groundCheckRight;
    [SerializeField] private GameObject groundCheckLeft;

    [SerializeField] private LayerMask groundLayer;

    private bool _canMove;

    [SerializeField] private Light2D showLight;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        _canMove = true;
        _isJumping = false;
        _isInvincible = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _colliders = GameObject.FindGameObjectWithTag(Tags.Enemies).GetComponentsInChildren<Collider2D>();
        
        _baseScale = transform.localScale;
    }

    private void OnEnable()
    {
        EventHandler.PlayerDamagedEvent += PlayerTookDamage;
    }

    private void OnDisable()
    {
        EventHandler.PlayerDamagedEvent -= PlayerTookDamage;
    }

    private void Update()
    {
        if (!_canMove) return;

        if (_hasLeftGround && GetIsGrounded() && _isJumping)
        {
            _isJumping = false;
            _hasLeftGround = false;
            EventHandler.CallEffectSpawnEvent(VisualEffectType.JumpLand, GetTouchingGroundPosition());
        }

        CheckInvincible();
        CheckJump();
        CheckCheckpoint();
        CheckAttackTimer();
        Attack();
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

    private void CheckCheckpoint()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TeleportToCheckpoint();
        }
    }

    private void TeleportToCheckpoint()
    {
        transform.position = CheckpointManager.Instance.GetLastCheckpoint().transform.position;
        ResetMovementVelocity();
        ResetYVelocity();
        EventHandler.CallPlayerDeathEvent();
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
        EventHandler.CallEffectSpawnEvent(VisualEffectType.JumpStart, GetTouchingGroundPosition());
        AudioManager.Instance.PlaySound(SoundEffectType.Jump);
        var currentVelocity = _rigidbody2D.velocity;
        currentVelocity.y = jumpForce;
        _rigidbody2D.velocity = currentVelocity;
        _isJumping = true;
    }

    private Vector3 GetTouchingGroundPosition()
    {
        var collisions = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.1f, groundLayer);
        var collisionsRight = Physics2D.OverlapCircleAll(groundCheckRight.transform.position, 0.1f, groundLayer);
        var collisionsLeft = Physics2D.OverlapCircleAll(groundCheckLeft.transform.position, 0.1f, groundLayer);

        if (collisions.Length > 0)
            return groundCheck.transform.position;
        else if (collisionsRight.Length > 0)
            return groundCheckRight.transform.position;
        else if (collisionsLeft.Length > 0)
            return groundCheckLeft.transform.position;

        return Vector3.zero;
    }

    private void Attack()
    {
        if (!_canUseLight || _isUsingLight) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isUsingLight = true;
            _canUseLight = false;
            _animator.SetTrigger(Settings.PlayerAttackAnimation);
            AudioManager.Instance.PlaySound(SoundEffectType.LigntAttackIncrease);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            _hasLeftGround = false;
        }
        if (UntilDieRoutine == null) return;
        StopCoroutine(UntilDieRoutine);
        UntilDieRoutine = null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ground))
        {
            _hasLeftGround = true;
        }
        if (UntilDieRoutine != null) return;
        UntilDieRoutine = StartCoroutine(CheckPlayerDead());
    }

    private IEnumerator CheckPlayerDead()
    {
        yield return new WaitForSeconds(Settings.SecondsUntilDead);
        TeleportToCheckpoint();
    }

    private void CheckInvincible()
    {
        if (!_isInvincible) return;
        
        _invincibleTimer += Time.deltaTime;
        if (_invincibleTimer >= Settings.MaxInvincibleTime)
        {
            _isInvincible = false;
            _invincibleTimer = 0;
            EnableAllColliders();
            _animator.SetTrigger(Settings.PlayerInjuredEndAnimation);
        }
    }

    private void PlayerTookDamage()
    {
        _isInvincible = true;
        DisableAllColliders();
        _animator.SetTrigger(Settings.PlayerInjuredAnimation);

    }

    public void AttackEnd()
    {
        _isUsingLight = false;
        _canUseLight = true;
        _useLightDelay = Settings.MaxAttackDelay;
    }

    private void DisableAllColliders()
    {
        foreach (var colliderEnemy in _colliders)
        {
            colliderEnemy.enabled = false;
        }
    }
    
    private void EnableAllColliders()
    {
        foreach (var colliderEnemy in _colliders)
        {
            colliderEnemy.enabled = true;
        }
    }

    public void ResetMovementVelocity()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void ResetYVelocity()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
    }


    private bool GetIsGrounded()
    {
        var collisions = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.1f, groundLayer);
        var collisionsRight = Physics2D.OverlapCircleAll(groundCheckRight.transform.position, 0.1f, groundLayer);
        var collisionsLeft = Physics2D.OverlapCircleAll(groundCheckLeft.transform.position, 0.1f, groundLayer);
        return collisions.Length > 0 || collisionsRight.Length > 0 || collisionsLeft.Length > 0;
    }
}
