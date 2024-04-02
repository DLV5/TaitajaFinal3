using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health = 3;

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


    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
