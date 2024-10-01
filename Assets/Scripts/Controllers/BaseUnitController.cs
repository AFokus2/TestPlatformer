using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _moveSpeedForce = 10f;
    [Range(0, .3f)][SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private List<Vector2> _groundCheckPoints;
    [SerializeField] private float _groundRaycastLength = .2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _debug;

    protected bool _isGrounded;
    protected Rigidbody2D _rigidbody;
    private Vector3 _currentVelocity = Vector3.zero;

    public int FacingDirection { get; private set; } = 1;

    public bool MovementEnabled {get; set;} = true;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        CheckIfGrounded();
    }

    public virtual void Move(float move, bool jump)
    {
        if(!MovementEnabled)
            return;

        Vector3 targetVelocity = new Vector2(move * _moveSpeedForce, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);

        if ((move > 0 && FacingDirection == -1) || (move < 0 && FacingDirection == 1))
            Flip();

        if (_isGrounded && jump)
        {
            _isGrounded = false;
            _rigidbody.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    public virtual void Bounce() => _rigidbody.AddForce(new Vector2((int) Random.Range(-1f, 1f) * 100, _jumpForce / 2));

    public virtual void Bounce(Vector2 direction) => _rigidbody.AddForce(direction);

    public virtual void MoveTo(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void CheckIfGrounded()
    {
        foreach (Vector3 point in _groundCheckPoints)
        {
            _isGrounded = Physics2D.Raycast(transform.position + point, -transform.up, _groundRaycastLength, _groundLayer);

            if (_isGrounded)
                break;
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (!_debug)
            return;

        foreach (Vector3 point in _groundCheckPoints)
        {
            var groundCheckPointPos = transform.position + point;

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(groundCheckPointPos, 0.05f);

            Gizmos.color = _isGrounded ? Color.red : Color.yellow;
            Gizmos.DrawLine(groundCheckPointPos, groundCheckPointPos + -transform.up * _groundRaycastLength);
        }
    }
}
