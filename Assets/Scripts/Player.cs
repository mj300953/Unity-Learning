using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Damageable
{
    [SerializeField] private float jumpingPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxJump;
    [SerializeField] private float attackDuration;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private int _jumpAmount;
    private int _attackCombo;
    private float _horizontalInput;
    private float _attackFinishTime;
    private bool _gotJumpInput;
    private bool _isGrounded;
    private bool _gotAttackInput;
    private bool _facingRight = true;
    
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int SpeedInAirHash = Animator.StringToHash("SpeedInAir");
    private static readonly int InAirHash = Animator.StringToHash("InAir");
    private static readonly int AttackComboCounter = Animator.StringToHash("ComboCounter");

    private const int MaxAttackCombo = 3;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = transform;
    }

    private void Update()
    {
        CollectInput();
        Move();
        UpdateAnimations();
        HandleFacing(); 
        HandleAttack();
        HandleJump();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HitGround(collision))
        {
            EnableJump();
        }
    }
    
    private void HandleFacing()
    {
        if (IsFacingWrongDirection())
        {
            FaceRightDirection();
        }
    }

    private void HandleAttack()
    {
        if (ShouldAttack())
        {
            Attack();
        }

        TryFinishingAttack();
    }
    
    private void HandleJump()
    {
        if (ShouldJump())
        {
            Jump();
        }
    }

    private void CollectInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _gotJumpInput = Input.GetKeyDown(KeyCode.W);
        _gotAttackInput = Input.GetKeyDown(KeyCode.Space);
    }

    private void Move()
    {
        Vector2 _currentVelocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(moveSpeed * _horizontalInput, _currentVelocity.y);
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat(SpeedHash, Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat(SpeedInAirHash, _rigidbody.velocity.y);
        _animator.SetInteger(AttackComboCounter, _attackCombo);
    }

    private bool IsFacingWrongDirection()
    {
        return _facingRight switch
        {
            true => _rigidbody.velocity.x < 0,
            false => _rigidbody.velocity.x > 0
        };
    }

    private void FaceRightDirection()
    {
        _facingRight = !_facingRight;
        _transform.Rotate(Vector2.up * 180);
    }

    private bool ShouldAttack()
    {
        return _gotAttackInput && _attackCombo < MaxAttackCombo && _isGrounded;
    }
    
    private void Attack()
    {
        if (_attackCombo == 0)
        {
            _attackFinishTime = Time.time + attackDuration;
        }
        else
        {
            _attackFinishTime += attackDuration;
        }

        _attackCombo++;
    }
    
    private void TryFinishingAttack()
    {
        if (Time.time >= _attackFinishTime)
        {
            _attackCombo = 0;
        }
    }
    
    private bool ShouldJump()
    {
        return _gotJumpInput && (_isGrounded || _jumpAmount > 0) && Time.time >= _attackFinishTime;
    }

    private void Jump()
    {
        _rigidbody.AddForce(jumpingPower * Vector2.up);
        _animator.SetBool(InAirHash, true);
        _isGrounded = false;
        _jumpAmount--;
    }

    private static bool HitGround(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Ground");
    }

    private void EnableJump()
    {
        _animator.SetBool(InAirHash, false);
        _isGrounded = true;
        _jumpAmount = maxJump;
    }
}
