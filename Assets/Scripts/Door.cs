using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IDealWithCollision
{
    [SerializeField] EKey requiredKeyColor;
    [SerializeField] GameObject door;

    public void CollisionEffect(GameObject other)
    {
        if(Inventory.instance.IHaveTheKey(requiredKeyColor))
        {
            door.GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
