using UnityEngine;

public abstract class Pickup : MonoBehaviour, IDealWithCollision 
{
    public abstract void CollisionEffect(GameObject other);
}