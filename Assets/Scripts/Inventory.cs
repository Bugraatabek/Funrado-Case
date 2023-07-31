using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    public static Inventory instance;

    public event Action<EKey> onCollectKey;
    
    private List<KeyPickup> keys = new List<KeyPickup>();
    

    private void Awake() 
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddKey(KeyPickup key, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            onCollectKey?.Invoke(key.GetColor());
            keys.Add(key);
        }
    }

    public bool IHaveTheKey(EKey keyColor)
    {
        foreach (var key in keys)
        {
            if(key.GetColor() == keyColor)
            {
                return true;
            }
        }
        return false;
    }
}