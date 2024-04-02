using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] private int _damage;

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
}
