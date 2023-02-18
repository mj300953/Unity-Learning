using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float jumpingPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxJump;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private int _jumpAmount;
    private float _horizontalInput;
    private bool _shouldJump;
    private bool _isGrounded;
    private bool _facingRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _facingRight = true;
    }

    private void Update()
    {
        CollectInput();
        Move();
        UpdateAnimations();

        if (MovingRight() && _facingRight) FlipCharacter();
        if (MovingLeft() && !_facingRight) FlipCharacter();


        if (ShouldJump())
        {
            UpdateJumpAnimations();
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HitGround(collision))
        {
            ReUpdateJumpAnimations();
            EnableJump();
        }
    }

    private void CollectInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _shouldJump = Input.GetKeyDown(KeyCode.W);
    }

    private void Move()
    {
        Vector2 _currentVelocity = _rigidbody.velocity;
        _rigidbody.velocity = new Vector2(moveSpeed * _horizontalInput, _currentVelocity.y);
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat("SpeedInAir", _rigidbody.velocity.y);
    }
    private bool MovingRight()
    {
        return _rigidbody.velocity.x < 0;
    }

    private bool MovingLeft()
    {
        return _rigidbody.velocity.x > 0;
    }

    private void FlipCharacter()
    {
        _facingRight = !_facingRight;
        transform.Rotate(Vector2.up * 180);
    }

    private bool ShouldJump()
    {
        return _shouldJump && (_isGrounded || _jumpAmount > 0);
    }

    private void UpdateJumpAnimations()
    {
        _animator.SetBool("InAir", true);
    }

    private void Jump()
    {
        _rigidbody.AddForce(jumpingPower * Vector2.up);
        _isGrounded = false;
        _jumpAmount--;
    }

    private static bool HitGround(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Ground");
    }

    private void ReUpdateJumpAnimations()
    {
        _animator.SetBool("InAir", false);
    }

    private void EnableJump()
    {
        _isGrounded = true;
        _jumpAmount = maxJump;
    }

}
