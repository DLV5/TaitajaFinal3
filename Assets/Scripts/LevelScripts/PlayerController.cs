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
        ApplyGravity();
        ApplyMovement();
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

        _gravityVelocity += _jumpPower;
        _animator.Jump();
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
}
