using UnityEngine;

public class KeyPickup : Pickup
{
    [SerializeField] private EKey keyColor;
    [SerializeField] private int _amountOfKeys;
    
    public override void CollisionEffect(GameObject other)
    {
        gameObject.SetActive(false);
        Inventory.instance.AddKey(this, _amountOfKeys);
    }

    public EKey GetColor()
    {
        return keyColor;
    }
}