using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour, IDealWithCollision
{
    public static FinishTrigger instance;


    public event Action gameFinished;

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

    public void CollisionEffect(GameObject other)
    {
        gameFinished?.Invoke();
    }
}
