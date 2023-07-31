using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Level))]
public class Target : MonoBehaviour
{ 
    public event Action attack;
    public event Action defense;

    Health _health;
    Level _levelComp;
    
    private bool isDead = false;
    private Target _target = null;
    
    protected virtual void Awake() 
    {
        _health = GetComponent<Health>();
        _levelComp = GetComponent<Level>(); 
    }

    private void Start() 
    {
        _health.onDeath += ChangeLivingState;
    }

    public void Attack(Target target)
    {
        _target = target;
        attack?.Invoke();
    }

    public void Defense()
    {
        defense?.Invoke();
    }

    //Animation Event
    void Hit()
    {
        _target.TakeDamage(GetLevel());
    }

    public void TakeDamage(int damage)
    {
        if(damage == GetLevel())
        {
            damage += 1;
        }
        _health.TakeDamage(damage - GetLevel());
    }

    public int GetLevel()
    {
        return _levelComp.GetLevel();
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void ChangeLivingState()
    {
        isDead = true;
    }

    
}