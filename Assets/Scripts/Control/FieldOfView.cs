using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public bool CanSeeTarget {get {return canSeeTarget;} }
    public GameObject Target {get { return target; }}
    public float Angle { get { return angle; }}
    public float Radius { get { return radius; }}


    [SerializeField] private float radius;
    [Range(0,360)] [SerializeField] private float angle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    private bool canSeeTarget;
    private GameObject target;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return delay;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) <= angle/2 )
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeTarget = true;
                }
                else
                {
                    canSeeTarget = false;
                }
            }
            else
            {
                canSeeTarget = false;
            }
        }
        else if(canSeeTarget)
        {
            canSeeTarget = false;
        }
    }
}
