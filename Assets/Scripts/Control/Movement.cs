using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private NavMeshAgent navMeshAgent;
    
    protected virtual void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = _moveSpeed;
    }

    protected void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    protected void SetDestination(Vector3 transformPosition, Vector3 offset)
    {
        navMeshAgent.destination = transformPosition + offset;
    }

    protected void Enable()
    {
        navMeshAgent.enabled = true;
    }

    protected void Disable()
    {
        navMeshAgent.enabled = false;
    }

    protected bool isThereADestination()
    {
       return navMeshAgent.destination != null;
    }

    protected void Move(Vector3 offset)
    {
        navMeshAgent.Move(offset);
    }

    protected void Stop()
    {
        navMeshAgent.isStopped = true;
    }

    protected void Resume()
    {
        navMeshAgent.isStopped = false;
    }

    private void LookAt(Transform transformToLookAt)
    {
        transform.LookAt(transformToLookAt);
    }
}

