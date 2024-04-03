using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public static event Action OnDied;

    [SerializeField] private int _health = 3;

    private ColoredFlash _flash;

    public int Health { 
        get { return _health; } 
        set {  
            _health = value; 

            if(_health <= 0)
            {
                OnDied?.Invoke();
                Destroy(gameObject);
            }
        } 
    }

    private void Awake()
    {
        _flash = GetComponent<ColoredFlash>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        _flash.Flash(Color.white);
    }
}
