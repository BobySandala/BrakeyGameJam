using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupController : EnemyController
{
    public float fugeLupu = 5f;

    public float patrolSpeed = 3f;

    private bool isPatrolling = true;
    
    // Start is called before the first frame update
    void Start()
    {
        base.speed = fugeLupu;
        base.Start();
        base.setPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isPatrolling)
        {
            base.speed = patrolSpeed;
            base.Patrol();
        } else
        {
            base.speed = fugeLupu;
        }
    }

    public void seeGaina(Vector3 gainaPosition)
    {
        float distance = Vector3.Distance(transform.position, gainaPosition);

        if (distance < 1f)
        {
            isPatrolling = false;
            base.moveEnemy((gainaPosition - transform.position).normalized);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gaina") || other.CompareTag("Player"))
        {
            isPatrolling = true;
            base.setPatrolPoint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Gaina"))
        {
            //follow target
            isPatrolling = false;
            SeeTarget(other.transform.position);
        }
    }

    private void SeeTarget(Vector3 targetPosition)
    {
        //float distance = Vector3.Distance(transform.position, targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;
        base.moveEnemy(direction);
    }
}
