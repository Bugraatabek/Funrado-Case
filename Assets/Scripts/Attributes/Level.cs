using System;
using UnityEngine;

// This script manages the level of a game object, allowing it to gain levels and notifying listeners when the level changes.

public class Level : MonoBehaviour 
{
    // Starting level value for this game object.
    [SerializeField] private int _startingLevel;

    // Current level value of this game object.
    private int _currentLevel;

    // Event invoked when this game object gains a level.
    public event Action onLevelGained;

    // Called when the script instance is being loaded.
    private void Awake() 
    {
        // Initialize the current level to the starting level when the object is created.
        _currentLevel = _startingLevel;    
    }
    
    // Method to handle gaining levels.
    public void GainLevel(int amount)
    {
        // Increase the current level by the given amount.
        _currentLevel += amount;

        // Invoke the onLevelGained event, notifying any listeners about the level change.
        onLevelGained?.Invoke();
    }

    // Method to get the current level.
    public int GetLevel()
    {
        return _currentLevel;
    }
}
