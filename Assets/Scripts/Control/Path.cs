using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    const float waypointGizmoRadius = 0.3f;

    private void OnDrawGizmos() 
    {
        // Draw gizmos for each waypoint in the path
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i); // Get the index of the next waypoint in the path
            Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius); // Draw a sphere at the current waypoint position
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j)); // Draw a line between the current waypoint and the next waypoint
        }
    }

    // Get the positions of all the waypoints in the path
    public Vector3[] GetPath()
    {
        Vector3[] path = new Vector3[transform.childCount];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = GetWaypoint(i);
        }
        return path;
    }

    // Get the index of the next waypoint in the path
    public int GetNextIndex(int i)
    {
        if(i + 1 == transform.childCount)
        {
            return 0;
        } 
        return i + 1;
    }

    // Get the position of a specific waypoint in the path
    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
