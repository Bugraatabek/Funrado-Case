using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDealWithCollision
{
    [SerializeField] EKey requiredKeyColor;
    public void CollisionEffect(GameObject other)
    {
        if(Inventory.instance.IHaveTheKey(requiredKeyColor))
        {
            gameObject.SetActive(false);
        }
    }
}
