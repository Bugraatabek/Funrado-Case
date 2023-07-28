using System;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] Path path;
    //[SerializeField] AIMover aIMover;  
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float waypointTolerance = 3f;
    private int _currentWaypointIndex = 0;

    public event Action running;
    public event Action idle;

    private void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        PatrolWaypoints();
    } 

    private void PatrolWaypoints()
    {
        if(CombatState() == true) 
        {
            navMeshAgent.isStopped = true;
            return;
        }
        navMeshAgent.isStopped = false;
        if(AtWaypoint(_currentWaypointIndex))
        {
            UpdateWaypointIndex();
            return;
        }
        navMeshAgent.destination = path.GetWaypoint(_currentWaypointIndex);
        AnnounceState();
    }

    private void UpdateWaypointIndex()
    {
        _currentWaypointIndex = path.GetNextIndex(_currentWaypointIndex);
        print(_currentWaypointIndex);
    }

    private bool AtWaypoint(int waypointIndex)
    {
        if(Vector3.Distance(transform.position, path.GetWaypoint(waypointIndex)) <= waypointTolerance)
        {
            print($"At waypoint {waypointIndex}");
            return true;
        }
        return false;
    }

    private void AnnounceState()
    {
        //add atacking state
        if(navMeshAgent.destination != null)
        {
            running?.Invoke();
            
        }
        else
        {
            idle?.Invoke();
        }
    }

    private bool? CombatState()
    {
        return CombatManager.instance.GetCombatState(this.gameObject);
    }
}