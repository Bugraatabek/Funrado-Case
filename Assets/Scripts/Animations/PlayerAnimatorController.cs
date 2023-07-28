using UnityEngine;

public class PlayerAnimatorController : AnimatorController 
{
    Mover _mover;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
    }

    protected override void Start()
    {
        base.Start();
        InputReader.instance.idle += base.Idle;
        _mover.onMoving += base.Running;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _mover.onMoving -= base.Running;
        InputReader.instance.idle -= base.Idle;
    } 
}