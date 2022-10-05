using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpImpulse = 30f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimationController _animationController;
    private Health _health;
    private float _maxVelocityMagnitude;
    private bool _isCanJump = true;
    private bool _isActive = true;

    public event Action OnWin;
    public event Action OnDead;
    public event Action OnCoinCollected;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<PlayerAnimationController>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _maxVelocityMagnitude = Mathf.Sqrt(Mathf.Pow(jumpImpulse, 2) + Mathf.Pow(horizontalSpeed, 2));
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
    }

    private void Movement()
    {
        HorizontalMovement();
        VerticalMovement();
        ClampVelocity();
    }

    private void HorizontalMovement()
    {
        float horizontalAxis = SimpleInput.GetAxis("Horizontal"); //-1 : 1
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = horizontalAxis * horizontalSpeed;
        _rigidbody.velocity = velocity;
        _animationController.SetSpeed(velocity.x == 0 ? 0 : (int) Mathf.Sign(velocity.x));
    }

    private void VerticalMovement()
    {
        if (_isCanJump && SimpleInput.GetAxis("Vertical") > 0)
        {
            _isCanJump = false;
            _rigidbody.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            _animationController.SetJump();
        }
    }

    private void ClampVelocity()
    {
        float velocityMagnitude = _rigidbody.velocity.magnitude;
        velocityMagnitude = Mathf.Clamp(velocityMagnitude, 0f, _maxVelocityMagnitude);
        _rigidbody.velocity = _rigidbody.velocity.normalized * velocityMagnitude;
    }

    private void UpdateSide()
    {
        bool isNeedUpdate = Mathf.Abs(_rigidbody.velocity.x) > 0f;
        if (!isNeedUpdate)
            return;

        float side = Mathf.Sign(_rigidbody.velocity.x);
        Vector2 localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_isActive) return;

        if (col.gameObject.GetComponent<Platform>() && Mathf.RoundToInt(_rigidbody.velocity.y) <= 0)
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
        _rigidbody.velocity = Vector2.zero;
        _isActive = false;
    }

    private void OnDie()
    {
        Deactivate();
        OnDead?.Invoke();
    }

    private void Win()
    {
        _animationController.SetSpeed(0);
        Deactivate();
        OnWin?.Invoke();
    }
}