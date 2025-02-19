using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float boxBounds_X1;
    public float boxBounds_X2;
    public float boxBounds_Z1;
    public float boxBounds_Z2;

    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = 9.81f;
    private float timer = 0f;
    public float speed = 4.0f;
    public float patrolAreaSize = 15f;
    public float waitTime = 2f;

    private bool isPatroling = true;
    private BoxCollider boxCollider;

    [SerializeField]
    private Vector3 targetPosition;
    
    public SpriteRenderer spriteRenderer;

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
    
    public void Flee()
    {
        isPatroling = false;
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

        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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
                setPatrolPointInRange();
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

    public void setPatrolPointInRange()
    {
        float randomX = Random.Range(boxBounds_X1, boxBounds_X2);
        float randomZ = Random.Range(boxBounds_Z1, boxBounds_Z2);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    public void reversePatrolPoint()
    {
        targetPosition *= -1;
    }
}
