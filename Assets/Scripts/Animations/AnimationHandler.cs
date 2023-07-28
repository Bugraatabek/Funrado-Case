using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _animator;
    private Health _health;
    private FxSpawner fxSpawner;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        fxSpawner = GetComponent<FxSpawner>();
        _health = GetComponent<Health>();
    }
    public virtual void Start() 
    {
        _health.onDeath += Die;
        CombatManager.instance.combat += Attack;
    }

    public virtual void OnDisable() 
    {
        _health.onDeath -= Die;
        CombatManager.instance.combat -= Attack;
    }

    protected void Run()
    {
        _animator.SetBool("Run", true);
    }

    protected void Idle()
    {
        _animator.SetBool("Run" , false);
    }

    protected void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    protected void Die()
    {
        fxSpawner.SpawnFX();
        gameObject.SetActive(false);
    }

    


}
