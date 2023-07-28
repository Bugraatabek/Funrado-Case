using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public event Action onMoving;
    
    [SerializeField] private float _moveSpeed;

    private void Start() 
    {
        InputReader.instance.moveHorizontal += MoveHorizontal;
        InputReader.instance.moveVertical += MoveVertical;
    }
    private void OnDisable() 
    {
        InputReader.instance.moveHorizontal -= MoveHorizontal;
        InputReader.instance.moveVertical -= MoveVertical;
    }
   
    public void MoveVertical(float direction)
    {
        onMoving?.Invoke();
        transform.Translate(Vector3.forward * _moveSpeed * direction * Time.fixedDeltaTime);
        //add rotation
    }

    public void MoveHorizontal(float direction)
    {
        onMoving?.Invoke();
        transform.Translate(Vector3.right * _moveSpeed * direction * Time.fixedDeltaTime);
        //add rotation
    }
}
