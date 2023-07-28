using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    private float maxCombatDistance = 3f;
    public event Action combat;
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

    public void StartCombat(Target instigator, Target target)
    {
        StartCoroutine(Combat(instigator, target));
    }

    private IEnumerator Combat(Target instigator, Target target)
    {
        int instigatorLevel = instigator.GetLevel();
        int targetLevel = target.GetLevel();
        while(true)
        {
            if(instigator.IsDead() || target.IsDead() || Vector3.Distance(instigator.transform.position, target.transform.position) > maxCombatDistance) yield break;
            combat?.Invoke();
            
            if(instigatorLevel < targetLevel)
            {
                instigator.TakeDamage(targetLevel - instigatorLevel);
            }
            else
            {
                target.TakeDamage(targetLevel - instigatorLevel);
            } 
            yield return new WaitForSeconds(1);
        }
    }
}
