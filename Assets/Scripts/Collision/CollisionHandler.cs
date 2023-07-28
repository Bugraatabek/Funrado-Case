using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    IDealWithCollision iDealWithCollision = null;

    private void Awake() 
    {
        iDealWithCollision = GetComponent<IDealWithCollision>();
    }

    public virtual void OnTriggerEnter(Collider other) 
    {
        iDealWithCollision.CollisionEffect(other.gameObject);
    }
}
