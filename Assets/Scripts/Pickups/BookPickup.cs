using UnityEngine;

public class BookPickup : Pickup
{
    [SerializeField] private int _levelAmount;
    public override void CollisionEffect(GameObject other)
    {
        gameObject.SetActive(false);
        other.GetComponent<Level>().GainLevel(_levelAmount);
    }
}