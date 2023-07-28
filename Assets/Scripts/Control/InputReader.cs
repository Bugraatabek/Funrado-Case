using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static InputReader instance;

    [SerializeField] private FloatingJoystick _floatingJoystick;
    public event Action<float> moveVertical;
    public event Action<float> moveHorizontal;
    public event Action idle;

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

    private void Update() 
    {
        idle?.Invoke();
        if(_floatingJoystick.Vertical != 0)
        {
            moveVertical?.Invoke(_floatingJoystick.Vertical);
        }

        if(_floatingJoystick.Horizontal != 0)
        {
            moveHorizontal?.Invoke(_floatingJoystick.Horizontal);
        }
        
    }
}
