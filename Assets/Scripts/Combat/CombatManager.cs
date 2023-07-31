using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public Dictionary<Target,bool> targetsInCombat;
    
    private float maxCombatDistance = 3f;

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
        TrackTargets();
    }

    public void TrackTargets()
    {
        targetsInCombat = new Dictionary<Target, bool>();
        foreach (Target target in FindObjectsOfType<Target>())
        {
            targetsInCombat[target] = false;
        }
    }

    public void StartCombat(Target instigator, Target target)
    {
        if(GetInCombatCount() >= 2) return;
        ChangeCombatState(instigator, true);
        ChangeCombatState(target, true);
        StartCoroutine(Combat(instigator, target));
    }

    private IEnumerator Combat(Target instigator, Target target)
    {
        int instigatorLevel = instigator.GetLevel();
        int targetLevel = target.GetLevel();
        while(true)
        {
            if(instigator.IsDead() || target.IsDead() || Vector3.Distance(instigator.transform.position, target.transform.position) > maxCombatDistance)
            {
                ChangeCombatState(target, false);
                ChangeCombatState(instigator, false);
                yield break;
            }
            
            if(instigatorLevel < targetLevel)
            {
                target.Attack(instigator);
                instigator.Defense();
            }
            else
            {
                if(instigatorLevel == targetLevel)
                {
                    instigatorLevel++;
                }
                instigator.Attack(target);
                target.Defense();
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void ChangeCombatState(Target target, bool stateToChange)
    {
        targetsInCombat[target] = stateToChange;
    }

    private int GetInCombatCount()
    {
        int inCombatCount = 0;
        foreach (Target target in targetsInCombat.Keys)
        {
            if(targetsInCombat[target] == true)
            {
                inCombatCount++;
            }
        }
        return inCombatCount;
    }

    public bool? GetCombatState(GameObject gameObject)
    {
        if(!gameObject.GetComponent<Target>())
        {
            return null;
        }
        else
        {
            return targetsInCombat[gameObject.GetComponent<Target>()];
        }
    }
}
