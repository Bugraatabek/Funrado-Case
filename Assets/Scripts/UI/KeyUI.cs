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
        if(keyColor == EKey.Blue)
        {
            CheckBlue();
        }
        if(keyColor == EKey.Red)
        {
            CheckRed();
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
