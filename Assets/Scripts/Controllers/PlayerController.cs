using UnityEngine;

public class PlayerController : BaseUnitController
{
    [Space]
    [SerializeField] private Animator _animator;

    private bool _isGoingJump;

    private void Start()
    {
        InputController.OnJump += OnJump;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Animate();

        Move(InputController.GetHorizontalInput(), _isGoingJump);
        _isGoingJump = false;
    }

    public void SetHurtAnimation(bool state) {
        _animator.SetBool("IsHurted", state);
    }

    private void Animate() {
        _animator.SetFloat("Speed", Mathf.Abs(InputController.GetHorizontalInput()));
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("Ydirection", Mathf.Clamp(_rigidbody.velocity.y, -1, 1));
    }

    private void OnJump() => _isGoingJump = true;

    void OnDestroy()
    {
        InputController.OnJump -= OnJump;
    }
}
