using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed) => _animator.SetFloat("Speed", speed);

    public void StartJumping() => _animator.SetBool("IsJumping", true);

    public void StopJumping() => _animator.SetBool("IsJumping", false);

    public void Attack() => _animator.SetTrigger("Attack");
}
