using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script represents a field of view (FOV) for an entity, allowing it to detect targets within a specified radius and angle.

public class FieldOfView : MonoBehaviour
{
    // Properties to expose information about the detected target and the FOV parameters.
    public bool CanSeeTarget { get { return canSeeTarget; } } // Property indicating whether the entity can see a target.
    public GameObject Target { get { return target; } } // Property representing the detected target GameObject.
    public float Angle { get { return angle; } } // Property representing the FOV angle in degrees.
    public float Radius { get { return radius; } } // Property representing the FOV radius.

    // Serialized fields for setting the FOV parameters and target detection settings.
    [SerializeField] private float radius; // The radius of the FOV.
    [Range(0, 360)] [SerializeField] private float angle; // The angle of the FOV (in degrees).
    [SerializeField] private LayerMask targetMask; // LayerMask to filter the target objects.
    [SerializeField] private LayerMask obstructionMask; // LayerMask to detect obstructions.

    // Private variables for tracking whether a target is currently visible within the FOV.
    private bool canSeeTarget;
    private GameObject target;

    // Called on start to initialize the script.
    void Start()
    {
        // Find the target GameObject using the "Player" tag. (Assumes the target has the "Player" tag)
        target = GameObject.FindWithTag("Player");

        // Start the FOV detection routine.
        StartCoroutine(FOVRoutine());
    }

    // FOV detection routine, repeatedly checks for targets within the FOV.
    private IEnumerator FOVRoutine()
    {
        // Set a delay interval for checking FOV (reduces frequent checks).
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while (true)
        {
            // Wait for the specified delay before checking FOV again.
            yield return delay;

            // Perform the FOV check.
            FieldOfViewCheck();
        }
    }

    // Method to check if any target is within the FOV.
    private void FieldOfViewCheck()
    {
        // Use Physics.OverlapSphere to get all colliders within the FOV radius and matching the targetMask.
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // If any target is within the FOV radius...
        if (rangeChecks.Length != 0)
        {
            // Get the transform of the closest target (assuming the first element of the array is the closest).
            Transform targetTransform = rangeChecks[0].transform;

            // Calculate the normalized direction from this entity to the target.
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            // Check if the target is within the FOV angle (angle / 2 degrees on each side).
            if (Vector3.Angle(transform.forward, directionToTarget) <= angle / 2)
            {
                // Calculate the distance to the target.
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                // Check for obstructions (raycast) between this entity and the target.
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    // If no obstructions are found, the entity can see the target.
                    canSeeTarget = true;
                }
                else
                {
                    // If an obstruction is found, the entity cannot see the target.
                    canSeeTarget = false;
                }
            }
            else
            {
                // If the target is outside the FOV angle, the entity cannot see the target.
                canSeeTarget = false;
            }
        }
        else if (canSeeTarget)
        {
            // If no targets are within the FOV radius but the entity could see a target before, reset the visibility state.
            canSeeTarget = false;
        }
    }
}

