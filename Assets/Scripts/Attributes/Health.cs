using System;
using UnityEngine;

// This script manages the health of a game object, allowing it to take damage and notify listeners when health changes or reaches zero.

public class Health : MonoBehaviour 
{
    // Maximum health value for this game object.
    [SerializeField] private int _maxHealth;
    
    // Current health value of this game object.
    private int _currentHealth;

    // Event invoked when this game object takes damage.
    public event Action onTakeDamage;

    // Event invoked when this game object's health reaches zero (death).
    public event Action onDeath;

    // Called when the script instance is being loaded.
    private void Awake() 
    {
        // Initialize the current health to the maximum health when the object is created.
        _currentHealth = _maxHealth;
    }

    // Method to handle taking damage.
    public void TakeDamage(int damage)
    {
        // Decrease the current health by the absolute value of the damage (no negative health).
        _currentHealth -= Math.Abs(damage);
        
        // Invoke the onTakeDamage event, notifying any listeners about the health change.
        onTakeDamage?.Invoke();
        
        // For debugging purposes, print the current health to the console.
        print(_currentHealth);

        // If the current health falls below or equal to zero, the object is considered dead.
        if(_currentHealth <= 0)
        {
            // Invoke the onDeath event, notifying any listeners about the object's death.
            onDeath?.Invoke();
        }
    }

    // Method to get the health as a fraction (current health divided by maximum health).
    public float GetFraction()
    {
        return (float)_currentHealth / _maxHealth;
    }
}
