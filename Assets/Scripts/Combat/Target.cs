using System;
using System.Collections;
using UnityEngine;

// This script represents a target game object that can be attacked and damaged.

[RequireComponent(typeof(Health))] // Require a Health component on the same game object.
[RequireComponent(typeof(Level))]  // Require a Level component on the same game object.
public class Target : MonoBehaviour
{ 
    // Events for attacking and defending actions.
    public event Action attack;
    public event Action defense;

    // References to the Health and Level components attached to this game object.
    Health _health;
    Level _levelComp;
    
    private bool isDead = false;
    private Target _target = null;
    
    // Called when the script instance is being loaded.
    protected virtual void Awake() 
    {
        // Get the Health and Level components attached to this game object.
        _health = GetComponent<Health>();
        _levelComp = GetComponent<Level>(); 
    }

    // Called just before the first frame update.
    private void Start() 
    {
        // Subscribe to the onDeath event of the Health component to handle the target's living state.
        _health.onDeath += ChangeLivingState;
    }

    // Method to perform an attack on another target.
    public void Attack(Target target)
    {
        _target = target;
        attack?.Invoke();
    }

    // Method to perform a defense action.
    public void Defense()
    {
        defense?.Invoke();
    }

    // Animation Event - Called from an animation event in the target's animation clip.
    void Hit()
    {
        // The target takes damage based on the attacker's level.
        _target.TakeDamage(GetLevel());
    }

    // Method to handle taking damage.
    public void TakeDamage(int damage)
    {
        // If the damage is equal to the target's level, increase the damage by 1 to ensure some damage is taken.
        if (damage == GetLevel())
        {
            damage += 1;
        }
        _health.TakeDamage(damage - GetLevel());
    }

    // Method to get the target's level.
    public int GetLevel()
    {
        return _levelComp.GetLevel();
    }

    // Method to check if the target is dead.
    public bool IsDead()
    {
        return isDead;
    }

    // Method to change the living state of the target to dead.
    private void ChangeLivingState()
    {
        isDead = true;
    }
}
