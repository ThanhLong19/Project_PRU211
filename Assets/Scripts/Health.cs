using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHealth;
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public void ChangeHealth(int value, int op)
    {
        curHealth += op * value;
        OnHealthChanged?.Invoke(curHealth);
        if (curHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Setup(int health)
    {
        curHealth = health;
        OnHealthChanged?.Invoke(curHealth);
    }
}