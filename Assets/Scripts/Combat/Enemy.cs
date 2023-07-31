using UnityEngine;

public class Enemy : Target, IDealWithCollision 
{
    public void CollisionEffect(GameObject other)
    {
        CombatManager.instance.StartCombat(other.GetComponent<Target>(), this);
    }
}