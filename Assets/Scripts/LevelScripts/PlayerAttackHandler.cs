using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    public static bool IsAttacking {  get; private set; }

    [SerializeField] private int _damage;
    [SerializeField] private Collider _attackHitBox;

    private void OnEnable()
    {
        AttackColisionDetection.OnHittedEnemy += Attack;
    }

    private void OnDisable()
    {
        AttackColisionDetection.OnHittedEnemy -= Attack;
    }

    public void Attack(IDamagable enemy)
    {
        enemy.TakeDamage(_damage);
    }

    public void StartedAttacking() => IsAttacking = true;
    public void StoppedAttacking() => IsAttacking = false;

    public void EnableHitBox()
    {
        _attackHitBox.gameObject.SetActive(true);
    }
    public void DisableHitBox()
    {
        _attackHitBox.gameObject.SetActive(false);
        StoppedAttacking();
    }
}
