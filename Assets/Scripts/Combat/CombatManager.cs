using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages combat interactions between Target game objects within a certain distance.

public class CombatManager : MonoBehaviour
{
    // Static instance of the CombatManager to ensure it's a singleton.
    public static CombatManager instance;
    
    // Dictionary to track which Targets are in combat and their combat states.
    public Dictionary<Target, bool> targetsInCombat;

    // The maximum distance allowed for combat between Targets.
    private float maxCombatDistance = 3f;

    // Called when the script instance is being loaded.
    private void Awake()
    {
        // Ensure that only one instance of the CombatManager exists.
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // Initialize the dictionary to track Targets in combat.
        TrackTargets();
    }

    // Method to initialize and track Targets in the targetsInCombat dictionary.
    public void TrackTargets()
    {
        targetsInCombat = new Dictionary<Target, bool>();
        
        // Find all Target objects in the scene and add them to the targetsInCombat dictionary with their initial combat state set to false.
        foreach (Target target in FindObjectsOfType<Target>())
        {
            targetsInCombat[target] = false;
        }
    }

    // Method to start combat between two Targets.
    public void StartCombat(Target instigator, Target target)
    {
        // Check if there are already two Targets in combat, if so, return and do not start new combat.
        if (GetInCombatCount() >= 2) return;

        // Change the combat state of the instigator and the target to true.
        ChangeCombatState(instigator, true);
        ChangeCombatState(target, true);

        // Start the combat coroutine between the instigator and the target.
        StartCoroutine(Combat(instigator, target));
    }

    // Coroutine to handle the combat interactions between the instigator and the target.
    private IEnumerator Combat(Target instigator, Target target)
    {
        int instigatorLevel = instigator.GetLevel();
        int targetLevel = target.GetLevel();

        while (true)
        {
            // Check if combat should end (if instigator or target is dead, or they are too far apart).
            if (instigator.IsDead() || target.IsDead() || Vector3.Distance(instigator.transform.position, target.transform.position) > maxCombatDistance)
            {
                // Change the combat state of both the instigator and the target to false.
                ChangeCombatState(target, false);
                ChangeCombatState(instigator, false);
                yield break; // End the combat coroutine.
            }
            
            // Perform attack and defense actions based on the level of the instigator and the target.
            if (instigatorLevel < targetLevel)
            {
                target.Attack(instigator);
                instigator.Defense();
            }
            else
            {
                if (instigatorLevel == targetLevel)
                {
                    instigatorLevel++;
                }
                instigator.Attack(target);
                target.Defense();
            }

            // Wait for 1 second before the next combat action.
            yield return new WaitForSeconds(1);
        }
    }

    // Method to change the combat state of a target.
    private void ChangeCombatState(Target target, bool stateToChange)
    {
        targetsInCombat[target] = stateToChange;
    }

    // Method to get the number of Targets currently in combat.
    private int GetInCombatCount()
    {
        int inCombatCount = 0;
        foreach (Target target in targetsInCombat.Keys)
        {
            if (targetsInCombat[target] == true)
            {
                inCombatCount++;
            }
        }
        return inCombatCount;
    }

    // Method to get the combat state of a game object (if it has a Target component).
    public bool? GetCombatState(GameObject gameObject)
    {
        // Check if the game object has a Target component. If not, return null.
        if (!gameObject.GetComponent<Target>())
        {
            return null;
        }
        else
        {
            // Return the combat state of the Target component from the targetsInCombat dictionary.
            return targetsInCombat[gameObject.GetComponent<Target>()];
        }
    }
}

