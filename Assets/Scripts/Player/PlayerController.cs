using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpImpulse = 30f;
    [SerializeField] private Transform rewardPos;
    [SerializeField] private float forceToGo;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    private bool _isCanJump=true;
    private PlayerAnimationController _playerAnimationController;
    private Health _health;
    private bool _isActive=true;

    private float _maxVelocityMagnitude;
    
    private BossEnemy _bossEnemy;

    public event Action OnWin;
    public event Action OnDead;
    public event Action OnCoinCollected;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _health = GetComponent<Health>();
        
        _bossEnemy = FindObjectOfType<BossEnemy>();
    }

    private void Start()
    {
        _maxVelocityMagnitude = MathF.Sqrt(MathF.Pow(jumpImpulse, 2) + MathF.Pow(horizontalSpeed, 2));
        _health.OnDie += OnDie;
    }

    private void OnDestroy()
    {
        _health.OnDie -= OnDie;
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        Movement();
        UpdateSide();

        if (!_bossEnemy.isActiveAndEnabled)
        {
            MoveToReward();
        }
    }

    private void MoveToReward()
    {
        Vector2 move = rewardPos.position - transform.position;
        move = move.normalized;
        move = move * forceToGo;
        _rigidbody2D.AddForce(move);
    }

    private void Movement()
    {
        HorizontalMovement();
        VerticalMovement();
        ClampVelocity();
    }


    private void HorizontalMovement()
    {
        float horizontalAxis = SimpleInput.GetAxis("Horizontal");
        Vector3 velocity = _rigidbody2D.velocity;
        velocity.x = horizontalAxis * horizontalSpeed;
        _rigidbody2D.velocity = velocity;
        _playerAnimationController.SetSpeed(velocity.x == 0 ? 0 : (int)Mathf.Sign(velocity.x));
    }
    
    private void VerticalMovement()
    {
        if (_isCanJump && SimpleInput.GetAxis("Vertical") > 0)
        {
            _isCanJump = false;
            _rigidbody2D.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            _playerAnimationController.SetJump();
        }
    }
    private void ClampVelocity()
    {
        float velocityMagnitude = _rigidbody2D.velocity.magnitude;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0f, _maxVelocityMagnitude);
        _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * velocityMagnitude;
    }

    private void UpdateSide()
    {
        bool isNeedUpdate = Mathf.Abs(_rigidbody2D.velocity.x) > 0f;
        if (!isNeedUpdate)
            return;

        float side = Mathf.Sign(_rigidbody2D.velocity.x);
        Vector2 localScale = transform.localScale;
        if (Mathf.Sign(localScale.x)!=side)
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_isActive) return;
        if (col.gameObject.GetComponent<Platform>() && Mathf.RoundToInt(_rigidbody2D.velocity.y) <= 0)
        {
            _isCanJump = true;
        }

        if (col.gameObject.TryGetComponent(out DangerousObject dangerousObject))
        {
            _health.SetDamage(dangerousObject.DamageValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_isActive) return;
        
        if (col.gameObject.GetComponent<Coin>())
        {
            col.gameObject.SetActive(false);
           OnCoinCollected?.Invoke();
        }

        if (col.gameObject.GetComponent<Finish>())
        {
           Win();
        }
    }

    private void Deactivate()
    {
        _rigidbody2D.velocity=Vector2.zero;
        _isActive = false;
    }
    private void OnDie()
    {
        Deactivate();
        OnDead?.Invoke();
    }

    private void Win()
    {
        _playerAnimationController.SetSpeed(0);
        Deactivate();
        OnWin?.Invoke();
    }
}
