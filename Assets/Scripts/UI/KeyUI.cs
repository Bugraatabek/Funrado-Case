using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField] Image blueKeyTick, redKeyTick;

    private void Start() 
    {
        Inventory.instance.onCollectKey += UpdateUI;
    }

    private void UpdateUI(EKey keyColor)
    {
        switch(keyColor)
        {
            case EKey.Blue:
            CheckBlue();
            break;
            case EKey.Red:
            CheckRed();
            break;
        }
    }

    private void CheckBlue()
    {
        blueKeyTick.enabled = true;
    }

    private void CheckRed()
    {
        redKeyTick.enabled = true;
    }
}
