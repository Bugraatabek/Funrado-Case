using UnityEngine;
using UnityEngine.AI;

// This is a base class that provides common movement functionality for objects using the NavMeshAgent component.

[RequireComponent(typeof(NavMeshAgent))] // Require a NavMeshAgent component on the same game object.
public class Movement : MonoBehaviour
{
    // Movement speed for the NavMeshAgent.
    [SerializeField] private float _moveSpeed;

    // Reference to the NavMeshAgent component attached to this game object.
    private NavMeshAgent navMeshAgent;
    
    // Called when the script instance is being loaded.
    protected virtual void Awake() 
    {
        // Get the NavMeshAgent component attached to this game object.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the movement speed of the NavMeshAgent to the specified value.
        navMeshAgent.speed = _moveSpeed;
    }

    // Method to set the destination for the NavMeshAgent to move towards.
    protected void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    // Method to set the destination for the NavMeshAgent with an offset from the specified position.
    protected void SetDestination(Vector3 transformPosition, Vector3 offset)
    {
        navMeshAgent.destination = transformPosition + offset;
    }

    // Method to enable the NavMeshAgent component, allowing movement.
    protected void Enable()
    {
        navMeshAgent.enabled = true;
    }

    // Method to disable the NavMeshAgent component, preventing movement.
    protected void Disable()
    {
        navMeshAgent.enabled = false;
    }

    // Method to check if there is a valid destination set for the NavMeshAgent.
    protected bool isThereADestination()
    {
        // Returns true if the NavMeshAgent has a destination assigned, otherwise false.
        return navMeshAgent.destination != null;
    }

    // Method to move the NavMeshAgent by the specified offset.
    protected void Move(Vector3 offset)
    {
        navMeshAgent.Move(offset);
    }

    // Method to stop the NavMeshAgent from moving.
    protected void Stop()
    {
        navMeshAgent.isStopped = true;
    }

    // Method to resume the movement of the NavMeshAgent after it has been stopped.
    protected void Resume()
    {
        navMeshAgent.isStopped = false;
    }

    // Method to make the game object look at a specified transform.
    private void LookAt(Transform transformToLookAt)
    {
        transform.LookAt(transformToLookAt);
    }
}


