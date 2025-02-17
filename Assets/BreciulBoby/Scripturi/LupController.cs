using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupController : EnemyController
{
    public float fugeLupu = 5f;
    public float patrolAreaSize = 15f;
    public float patrolSpeed = 3f;
    private Vector3 targetPosition;
    private bool isPatrolling = true;
    
    public float waitTime = 2f;
    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        setPatrolPoint();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isPatrolling)
        {
            Patrol();
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

    public void Patrol()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        base.moveEnemy(direction);

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                setPatrolPoint();
                timer = 0f;
            }
        }
    }

    public void setPatrolPoint()
    {
        float randomX = Random.Range(-patrolAreaSize, patrolAreaSize);
        float randomZ = Random.Range(-patrolAreaSize, patrolAreaSize);
        targetPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gaina"))
        {
            isPatrolling = true;
            setPatrolPoint();
        }
    }
}
