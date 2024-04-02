using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public static event Action<int> OnPlayerHealthChanged;
    public static event Action OnPlayerHealthDied;

    [SerializeField] private PlayerHUD _playerHUD;

    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;

            OnPlayerHealthChanged(_currentHealth);

            if(_currentHealth <= 0)
            {
                OnPlayerHealthDied?.Invoke();
            }
        }
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
        _playerHUD.SetHUD(_maxHealth, _currentHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
