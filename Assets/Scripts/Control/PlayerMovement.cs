using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This script extends the Movement base class to provide movement functionality for the player character.

public class PlayerMovement : Movement
{
    // Events for notifying when the player starts or stops moving.
    public event Action onMoving;
    public event Action onStopMoving;

    // Called when the script instance is being loaded.
    private void Start()
    {
        // Subscribe to the input events from the InputReader and the gameFinished event from the FinishTrigger.
        InputReader.instance.input += Move;
        InputReader.instance.noInput += StopMoving;
        FinishTrigger.instance.gameFinished += DisableControls;
    }

    // Called when the script is disabled.
    private void OnDisable()
    {
        // Unsubscribe from the input events from the InputReader.
        InputReader.instance.input -= Move;
        InputReader.instance.noInput -= StopMoving;
    }

    // Method called when the player needs to stop moving.
    private void StopMoving()
    {
        // If the game object is not active in the hierarchy, return and do nothing.
        if (gameObject.activeInHierarchy == false) return;

        // Call the base class Stop method to stop the player's movement.
        base.Stop();

        // Invoke the onStopMoving event to notify subscribers that the player has stopped moving.
        onStopMoving?.Invoke();
    }

    // Method called when the player needs to move based on input.
    private void Move(float zDirection, float xDirection)
    {
        // If the game object is not active in the hierarchy, return and do nothing.
        if (gameObject.activeInHierarchy == false) return;

        // Call the base class Resume method to resume the player's movement.
        base.Resume();

        // Invoke the onMoving event to notify subscribers that the player is moving.
        onMoving?.Invoke();

        // Calculate the movement offset based on the input direction.
        Vector3 offset = (Vector3.right * xDirection + Vector3.forward * zDirection);

        // Set the destination for the player to move towards, based on the current position and the offset.
        base.SetDestination(transform.position, offset);
    }

    // Method called to disable the player's movement controls (e.g., when the game is finished).
    private void DisableControls()
    {
        // Disable this script to prevent further player movement.
        this.enabled = false;
    }
}

