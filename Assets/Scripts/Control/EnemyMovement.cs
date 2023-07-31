using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Movement
{
   // Events for notifying when the enemy is idle or running.
    public event Action idle;
    public event Action running;

    // Reference to the Path script that contains waypoints for patrolling.
    [SerializeField] Path path; 

    // Tolerance distance for determining if the enemy has reached a waypoint.
    [SerializeField] private float waypointTolerance = 3f;

    // Index of the current waypoint the enemy is moving towards.
    private int _currentWaypointIndex = 0;

    // Reference to the FieldOfView script for detecting targets.
    FieldOfView fieldOfView;

    // Reference to the player GameObject.
    GameObject player;

    // Called when the script instance is being loaded.
    protected override void Awake()
    {
        // Call the base class Awake method to perform any necessary setup.
        base.Awake();

        // Find the player GameObject using the "Player" tag.
        player = GameObject.FindWithTag("Player");

        // Get a reference to the FieldOfView script attached to this enemy.
        fieldOfView = GetComponent<FieldOfView>();
    }

    // Called on a fixed time interval (physics update).
    private void FixedUpdate() 
    {
        // Control the enemy's behavior based on its current state.
        PatrolWaypoints();
    } 

    // Method to control the enemy's behavior when patrolling waypoints.
    private void PatrolWaypoints()
    {
        // If the enemy is in combat, stop moving and enter combat behavior.
        if (CombatState() == true) 
        {
            base.Stop();
            Combat();
            return;
        }

        // Resume normal movement if not in combat.
        base.Resume();
        
        // If the enemy can see the target (player), move towards the target's position.
        if (fieldOfView.CanSeeTarget)
        {
            base.SetDestination(player.transform.position);
            return;
        }

        // If the enemy has reached the current waypoint, update the waypoint index.
        if (AtWaypoint(_currentWaypointIndex))
        {
            UpdateWaypointIndex();
            return;
        }
        
        // Move the enemy towards the current waypoint.
        base.SetDestination(path.GetWaypoint(_currentWaypointIndex));

        // Announce the enemy's state (idle or running) based on whether there is a destination set.
        AnnounceState();
    }

    // Method for combat behavior, facing the player.
    private void Combat()
    {
        // If the player is active in the hierarchy, make the enemy face the player.
        if (player.activeInHierarchy)
        {
            transform.LookAt(player.transform);
        }
    }

    // Method to update the current waypoint index when the enemy reaches a waypoint.
    private void UpdateWaypointIndex()
    {
        _currentWaypointIndex = path.GetNextIndex(_currentWaypointIndex);
        print(_currentWaypointIndex);
    }

    // Method to check if the enemy is within the tolerance distance of a waypoint.
    private bool AtWaypoint(int waypointIndex)
    {
        // Check if the distance between the enemy and the waypoint is within the tolerance.
        if (Vector3.Distance(transform.position, path.GetWaypoint(waypointIndex)) <= waypointTolerance)
        {
            print($"At waypoint {waypointIndex}");
            return true;
        }
        return false;
    }

    // Method to announce the enemy's state (idle or running) based on whether it has a destination set.
    private void AnnounceState()
    {
        // If the enemy is in combat, return and do not announce the state.
        if (CombatState() == true) return;
        
        // Check if the enemy has a destination set.
        if (base.isThereADestination())
        {
            // Invoke the running event to notify subscribers that the enemy is running.
            running?.Invoke();
        }
        else
        {
            // Invoke the idle event to notify subscribers that the enemy is idle.
            idle?.Invoke();
        }
    }

    // Method to check the enemy's combat state using the CombatManager.
    private bool? CombatState()
    {
        return CombatManager.instance.GetCombatState(this.gameObject);
    }
}
