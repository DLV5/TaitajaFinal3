using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed) => _animator.SetFloat("Speed", speed);

    public void Jump() => _animator.SetTrigger("Jump");
}
