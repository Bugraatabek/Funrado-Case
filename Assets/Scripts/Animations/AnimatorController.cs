using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private Health _health;
    private FxSpawner fxSpawner;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        fxSpawner = GetComponent<FxSpawner>();
        _health = GetComponent<Health>();
    }
    protected virtual void Start() 
    {
        _health.onDeath += Die;
        CombatManager.instance.combat += Attacking;
    }

    protected virtual void OnDisable() 
    {
        _health.onDeath -= Die;
        CombatManager.instance.combat -= Attacking;
    }

    protected void Running()
    {
        _animator.SetBool("Run", true);
    }

    protected void Idle()
    {
        _animator.SetBool("Run" , false);
    }

    protected void Attacking()
    {
        _animator.SetTrigger("Attack");
    }

    protected void Die()
    {
        fxSpawner.SpawnFX();
        gameObject.SetActive(false);
    }

    


}
