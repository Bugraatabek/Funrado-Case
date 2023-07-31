using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private Health _health;
    private FxSpawner _fxSpawner;
    private Target _target;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _fxSpawner = GetComponent<FxSpawner>();
        _health = GetComponent<Health>();
        _target = GetComponent<Target>();
    }
    protected virtual void Start() 
    {
        _health.onDeath += Die;
        _target.attack += Attacking;
        _target.defense += Idle;
    }

    protected virtual void OnDisable() 
    {
        _health.onDeath -= Die;
        _target.attack -= Attacking;
        _target.defense -= Idle;
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
        _fxSpawner.SpawnFX();
        gameObject.SetActive(false);
    }

    


}
