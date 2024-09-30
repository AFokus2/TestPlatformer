using UnityEngine;

public abstract class BaseUnitController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _moveSpeedForce = 10f;
    [Range(0, .3f)][SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private Vector2 _groundCheckPoint;
    [SerializeField] private float _groundRaycastLength = .2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _debug;

    protected bool _isGrounded;
    protected Rigidbody2D _rigidbody;
    private bool _facingRight = true;
    private Vector3 _currentVelocity = Vector3.zero;


    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        CheckIfGrounded();
    }

    public virtual void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * _moveSpeedForce, _rigidbody.velocity.y);
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);

        if (move > 0 && !_facingRight)
            Flip();
        else if (move < 0 && _facingRight)
            Flip();

        if (_isGrounded && jump)
        {
            _isGrounded = false;
            _rigidbody.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    public virtual void MoveTo(Vector2 newPosition) {
        transform.position = newPosition;
    }

    protected virtual void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected virtual void CheckIfGrounded() => _isGrounded = Physics2D.Raycast(transform.position + (Vector3)_groundCheckPoint, -transform.up, _groundRaycastLength, _groundLayer);

    void OnDrawGizmos()
    {
        if (!_debug)
            return;

        var groundCheckPointPos = transform.position + (Vector3)_groundCheckPoint;

        Gizmos.color = Color.white;

        Gizmos.DrawSphere(groundCheckPointPos, 0.05f);

        Gizmos.color = _isGrounded ? Color.red : Color.yellow;

        Gizmos.DrawLine(groundCheckPointPos, groundCheckPointPos + -transform.up * _groundRaycastLength);
    }
}
