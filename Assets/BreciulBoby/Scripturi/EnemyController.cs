using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    public float gravity = 9.81f;
    public float speed = 4.0f;

    private BoxCollider boxCollider;

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

    public virtual void SeePlayer(Vector3 playerPosition)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player in viziune");
            SeePlayer(other.transform.position);
        }
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
}
