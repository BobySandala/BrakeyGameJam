using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupController : EnemyController
{
    public float fugeLupu = 5f;
    public float patrolSpeed = 3f;
    private bool isPatrolling = true;
    public GameManagerScript gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        base.setPatrolPoint();
        viatza = 20f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isPatrolling)
        {
            base.speed = patrolSpeed;
            base.Patrol();
        }

        if (viatza <= 0)
        {
            if (gameManager != null)
            {
                gameManager.LupMort();
            }
            
            Destroy(this.gameObject);
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
        else if (other.CompareTag("Ou"))
        {
            Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosXZ = new Vector2(other.transform.position.x, other.transform.position.z);
            float distantaOO = Vector2.Distance(positionXZ, targetPosXZ);
            if (distantaOO < 1.5f)
            {
                Destroy(other.gameObject);
                viatza -= 5f;
                print("am looveet lupuy");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Armament"))
        {
            Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosXZ = new Vector2(other.transform.position.x, other.transform.position.z);
            float distantaOO = Vector2.Distance(positionXZ, targetPosXZ);
            print(distantaOO);
            if (distantaOO < 7f)
            {
                viatza -= 10f;
                print("am looveet lupuy");
            }
        }
    }

    private void SeeTarget(Vector3 targetPosition)
    {
        //float distance = Vector3.Distance(transform.position, targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;
        base.moveEnemy(direction);
    }
}
