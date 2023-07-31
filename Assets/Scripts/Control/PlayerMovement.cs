using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : Movement
{
    public event Action onMoving;
    public event Action onStopMoving;

    private void Start() 
    {
        InputReader.instance.input += Move;
        InputReader.instance.noInput += StopMoving;
    }
    private void OnDisable() 
    {
        InputReader.instance.input -= Move;
        InputReader.instance.noInput += StopMoving;
    }

    private void StopMoving()
    {
        if(gameObject.activeInHierarchy == false) return;
        base.Stop();
        onStopMoving?.Invoke();
    }

    private void Move(float zDirection, float xDirection)
    {
        if(gameObject.activeInHierarchy == false) return;
        base.Resume();
        onMoving?.Invoke();
        Vector3 offset = (Vector3.right * xDirection + Vector3.forward * zDirection);
        base.SetDestination(transform.position , offset);
    }
}
