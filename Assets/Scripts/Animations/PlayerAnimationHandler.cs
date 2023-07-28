using UnityEngine;

public class PlayerAnimationHandler : AnimationHandler 
{
    Mover _mover;

    public override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
    }

    public override void Start()
    {
        base.Start();
        InputReader.instance.idle += base.Idle;
        _mover.onMoving += base.Run;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _mover.onMoving -= base.Run;
        InputReader.instance.idle -= base.Idle;
    } 
}