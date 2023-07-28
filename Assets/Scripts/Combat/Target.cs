using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Level))]
public class Target : MonoBehaviour
{ 
    Health _health;
    Level _levelComp;

    private bool isDead = false;

    public virtual void CacheComponents()
    {
        _health = GetComponent<Health>();
        _levelComp = GetComponent<Level>();  
    }
    
    private void Awake() 
    {
        CacheComponents();
    }

    private void Start() 
    {
        _health.onDeath += ChangeLivingState;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
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