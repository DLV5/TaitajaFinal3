using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;

    private DamageFlash _damageFlash;

    public int Health { 
        get { return _health; } 
        set {  
            _health = value; 

            if(_health <= 0)
            {
                Destroy(gameObject);
            }
        } 
    }

    private void Awake()
    {
        _damageFlash = GetComponent<DamageFlash>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        _damageFlash.CallDamageFlash();
    }
}
