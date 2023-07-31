using UnityEngine;

public class EnemyAnimatorController : AnimatorController 
{
    EnemyMovement ai;

    protected override void Awake()
    {
        base.Awake();
        ai = GetComponent<EnemyMovement>();
    }

    protected override void Start()
    {
        base.Start();
        ai.running += base.Running;
        ai.idle += base.Idle;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ai.running -= base.Running;
        ai.idle -= base.Idle;
    }
}