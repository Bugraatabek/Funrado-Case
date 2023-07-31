using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles collisions with other objects and triggers collision effects using IDealWithCollision interface.

public class CollisionHandler : MonoBehaviour
{
    // Reference to the IDealWithCollision interface to call collision effects.
    IDealWithCollision iDealWithCollision = null;

    // Called when the script instance is being loaded.
    private void Awake() 
    {
        // Get the IDealWithCollision component attached to this game object (if exists).
        iDealWithCollision = GetComponent<IDealWithCollision>();
    }

    // Called when another object with a collider enters the trigger collider attached to this game object.
    public virtual void OnTriggerEnter(Collider other) 
    {
        // Trigger the collision effect using the IDealWithCollision interface.
        // Pass the other game object involved in the collision as an argument.
        iDealWithCollision.CollisionEffect(other.gameObject);
    }
}

