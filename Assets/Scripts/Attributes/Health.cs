using System;
using UnityEngine;

public class Health : MonoBehaviour 
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    
    public event Action onTakeDamage;
    public event Action onDeath;

    private void Awake() 
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= Math.Abs(damage);
        onTakeDamage?.Invoke();
        print(_currentHealth);

        if(_currentHealth <= 0)
        {
            onDeath?.Invoke();
        }
    }

    public float GetFraction()
    {
        return (float)_currentHealth/_maxHealth;
    }
}