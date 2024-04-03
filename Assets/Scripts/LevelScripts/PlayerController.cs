using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _gravityMultiplier = 3.0f;

    [SerializeField] private float _jumpPower = 4.0f;

    [SerializeField] private PlayerAnimator _animator;

    private CharacterController _characterController;

    private Vector3 _direction;

    private float _gravity = -9.81f;

    private float _gravityVelocity;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (PlayerAttackHandler.IsAttacking) return;

        ApplyGravity();
        ApplyMovement();
        ShouldStopJumping();
    }

    public void SetDirection(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
        _direction  = new Vector3(_direction.x, 0, _direction.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if(!IsGrounded()) 
            return;

        AudioManager.Instance.PlaySFX("Jump");  // Jump sound effect
        _gravityVelocity += _jumpPower;
        _animator.StartJumping();
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _gravityVelocity < 0.0f)
        {
            _gravityVelocity = -1.0f;
        } else
        {
            _gravityVelocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _gravityVelocity;
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * _speed * Time.deltaTime);
        _animator.SetSpeed(Mathf.Abs(_direction.x) + Mathf.Abs(_direction.z));
    }

    private bool IsGrounded() => _characterController.isGrounded;

    private void ShouldStopJumping()
    {
        if (IsGrounded())
            _animator.StopJumping();
    }

    public void Attack()
    {
        int randomIndex = Random.Range(1, 3); // Generates random index for sound effect
        AudioManager.Instance.PlaySFX("Attack" + randomIndex);
        _animator.Attack();
    }
}
