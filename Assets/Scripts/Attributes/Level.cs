using System;
using UnityEngine;

public class Level : MonoBehaviour 
{
    [SerializeField] private int _startingLevel;
    private int _currentLevel;
    public event Action onLevelGained;

    private void Awake() 
    {
        _currentLevel = _startingLevel;    
    }
    
    public void GainLevel(int amount)
    {
        _currentLevel += amount;
        onLevelGained?.Invoke();
    }

    public int GetLevel()
    {
        return _currentLevel;
    }
    
}