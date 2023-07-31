using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static InputReader instance;

    [SerializeField] private FixedJoystick _fixedJoystick;
    public event Action<float, float> input;
    public event Action noInput;

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
        noInput?.Invoke();
        if(_fixedJoystick.Vertical != 0 || _fixedJoystick.Horizontal != 0)
        {
            input?.Invoke(_fixedJoystick.Vertical, _fixedJoystick.Horizontal);
        } 
    }
}
