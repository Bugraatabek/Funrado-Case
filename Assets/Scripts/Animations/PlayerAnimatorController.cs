using UnityEngine;

public class PlayerAnimatorController : AnimatorController 
{
    PlayerMovement _movement;

    protected override void Awake()
    {
        base.Awake();
        _movement = GetComponent<PlayerMovement>();
    }

    protected override void Start()
    {
        base.Start();
        _movement.onStopMoving += base.Idle;
        _movement.onMoving += base.Running;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _movement.onMoving -= base.Running;
        _movement.onStopMoving -= base.Idle;
    } 
}