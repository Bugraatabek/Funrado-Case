using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerWithTags : CollisionHandler
{
    [SerializeField] private List<string> tagsToCheck = new List<string>();
    
    public override void OnTriggerEnter(Collider other)
    {
        if(tagsToCheck.Contains(other.gameObject.tag))
        {
            base.OnTriggerEnter(other);
        }
    }
}
