using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
    public event Action idle;
    public event Action running;

    [SerializeField] Path path; 
    
    [SerializeField] private float waypointTolerance = 3f;
    private int _currentWaypointIndex = 0;

    FieldOfView fieldOfView;
    GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        fieldOfView = GetComponent<FieldOfView>();
    }

    private void Update() 
    {
        PatrolWaypoints();
    } 

    private void PatrolWaypoints()
    {
        if(CombatState() == true) 
        {
            base.Stop();
            Combat();
            return;
        }
        base.Resume();
        
        if(fieldOfView.CanSeeTarget)
        {
            base.SetDestination(player.transform.position);
            return;
        }

        if(AtWaypoint(_currentWaypointIndex))
        {
            UpdateWaypointIndex();
            return;
        }
        
        base.SetDestination(path.GetWaypoint(_currentWaypointIndex));
        AnnounceState();
    }

    private void Combat()
    {
        if(player.activeInHierarchy)
        {
            transform.LookAt(player.transform);
        }
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
        if(CombatState() == true) return;
        
        if(base.isThereADestination())
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