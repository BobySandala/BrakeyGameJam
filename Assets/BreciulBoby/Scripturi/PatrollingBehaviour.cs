using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviour : MonoBehaviour
{
    public float areaSize = 25f;
    public float speed = 3f;
    public float waitTime = 2f;
    private float timer = 0f;
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GainaController>().enabled = false;
        StartPatrolling();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                StartPatrolling();
                timer = 0f;
            }
        }
    }

    public void StartPatrolling()
    {
        float randomX = Random.Range(-areaSize, areaSize);
        float randomZ = Random.Range(-areaSize, areaSize);
        targetPosition = new Vector3(transform.position.x + randomX, 0.3f, transform.position.z + randomZ);
    }
}
