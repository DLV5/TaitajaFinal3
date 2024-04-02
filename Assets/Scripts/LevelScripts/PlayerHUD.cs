using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerHealthChanged += SetHealth;
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnPlayerHealthChanged -= SetHealth;
    }

    public void SetHUD(int maxHealth, int currentHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;
    }

    public void SetHealth(int health) => _healthSlider.value = health;
}
