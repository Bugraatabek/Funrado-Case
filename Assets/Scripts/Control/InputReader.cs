using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script reads the input from a FixedJoystick and provides events for notifying other scripts about the player's input.

public class InputReader : MonoBehaviour
{
    // Singleton instance of the InputReader.
    public static InputReader instance;

    // Reference to the FixedJoystick for reading player input.
    [SerializeField] private FloatingJoystick _floatingJoystick;

    // Events for notifying other scripts about player input.
    public event Action<float, float> input; // Event for receiving vertical and horizontal input values.
    public event Action noInput; // Event for notifying when no input is detected.

    // Called when the script instance is being loaded.
    private void Awake()
    {
        // Ensure only one instance of the InputReader exists in the scene.
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Called on a fixed time interval (physics update).
    private void FixedUpdate()
    {
        // Invoke the noInput event to notify subscribers that no input is detected.
        noInput?.Invoke();

        // Check if the joystick input has non-zero vertical or horizontal values.
        if (_floatingJoystick.Vertical != 0 || _floatingJoystick.Horizontal != 0)
        {
            // Invoke the input event with the current vertical and horizontal input values.
            input?.Invoke(_floatingJoystick.Vertical, _floatingJoystick.Horizontal);
        }
    }
}

