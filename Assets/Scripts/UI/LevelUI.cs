using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    private Level _level;
    private void Awake() 
    {
        _level = GetComponentInParent<Level>();
    }

    private void Start() 
    {
        _level.onLevelGained += UpdateUI;
        UpdateUI();
    }

    private void OnDisable() 
    {
        _level.onLevelGained -= UpdateUI;
    }

    void UpdateUI()
    {
        levelText.text = $"Level:{_level.GetLevel()}";
    }

    
}
