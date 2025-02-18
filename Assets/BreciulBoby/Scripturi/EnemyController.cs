using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = 9.81f;
    private float timer = 0f;
    public float speed = 4.0f;
    public float patrolAreaSize = 15f;
    public float waitTime = 2f;

    private BoxCollider boxCollider;

    private Vector3 targetPosition;

    [SerializeField]
    protected float viatza;

    // Start is called before the first frame update
    protected void Start()
    {
        controller = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);


    }



    private void OnTriggerEnter(Collider other)
    {
        
        //speed = speed * -1;
        if (other.CompareTag("Armament"))
        {
            print("Lovitura");
        }
        
    }

    public void moveEnemy(Vector3 direction)
    {
        //direction.y = 0f;
        //print(direction);
        //controller.Move(direction * speed * Time.deltaTime);

        if (controller != null)
        {
            direction.y = 0f;
            controller.Move(direction * speed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("No controller attached" + gameObject.name);
        }
    }

    public void Patrol()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        moveEnemy(direction);

        Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosXZ = new Vector2(targetPosition.x, targetPosition.z);

        if (Vector2.Distance(positionXZ, targetPosXZ) < 0.2f)
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
}
