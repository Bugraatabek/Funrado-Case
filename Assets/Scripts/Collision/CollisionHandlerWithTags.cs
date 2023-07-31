using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script extends the CollisionHandler class and adds functionality to filter collisions based on tags.

public class CollisionHandlerWithTags : CollisionHandler
{
    // List of tags to check for filtering collisions.
    [SerializeField] private List<string> tagsToCheck = new List<string>();

    // Called when another object with a collider enters the trigger collider attached to this game object.
    public override void OnTriggerEnter(Collider other)
    {
        // Check if the tag of the other game object is present in the tagsToCheck list.
        if (tagsToCheck.Contains(other.gameObject.tag))
        {
            // If the tag is found in the list, trigger the collision effect using the base class's method (inherited from CollisionHandler).
            base.OnTriggerEnter(other);
        }
    }
}
