using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _curHealth;
    private int _maxHealth;
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public void ChangeHealth(int value, int op)
    {
        // _curHealth += op * value;
        _curHealth = Math.Clamp(_curHealth + op * value, 0, _maxHealth);
        OnHealthChanged?.Invoke(_curHealth * 1 / (float)_maxHealth * 100);
        if (_curHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Setup(int health)
    {
        _maxHealth = _curHealth = health;
        OnHealthChanged?.Invoke(_curHealth * 1 / (float)_maxHealth * 100);
    }
}