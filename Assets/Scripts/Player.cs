using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpingPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxJump;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private int _jumpAmount;
    private float _horizontalInput;
    private bool _gotJumpInput;
    private bool _isGrounded;
    private bool _facingRight = true;
    
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int SpeedInAirHash = Animator.StringToHash("SpeedInAir");
    private static readonly int InAirHash = Animator.StringToHash("InAir");

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

        if (ShouldJump())
        {
            Jump();
        }
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

    private void CollectInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _gotJumpInput = Input.GetKeyDown(KeyCode.W);
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

    private bool ShouldJump()
    {
        return _gotJumpInput && (_isGrounded || _jumpAmount > 0);
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
