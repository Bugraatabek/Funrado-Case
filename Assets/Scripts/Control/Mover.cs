using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public event Action onMoving;
    
    //private NavMeshAgent navMeshAgent;
    [SerializeField] private float _moveSpeed;
    private void Awake() 
    {
        // navMeshAgent = GetComponent<NavMeshAgent>();
        // navMeshAgent.speed = _moveSpeed;
    }

    private void Start() 
    {
        InputReader.instance.moveHorizontal += MoveHorizontal; 
        InputReader.instance.moveVertical += MoveVertical;
        //Move input readers into subclass PlayerMover
    }
    private void OnDisable() 
    {
        InputReader.instance.moveHorizontal -= MoveHorizontal;
        InputReader.instance.moveVertical -= MoveVertical;
        //Move input readers into subclass PlayerMover
    }
   
    public virtual void MoveVertical(float direction) 
    {
        onMoving?.Invoke();
        transform.Translate(Vector3.forward * _moveSpeed * direction * Time.fixedDeltaTime);
        //change the function of this to navmesh destination
    }

    public virtual void MoveHorizontal(float direction)
    {
        onMoving?.Invoke();
        transform.Translate(Vector3.right * _moveSpeed * direction * Time.fixedDeltaTime);
         //change the function of this to navmesh destination
    }
}
