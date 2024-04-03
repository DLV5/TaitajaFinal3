using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Attack() => _animator.SetTrigger("Attack");
}
