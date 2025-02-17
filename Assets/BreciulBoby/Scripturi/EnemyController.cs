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
    void Start()
    {
        controller = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
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

    private void FollowPlayer(Vector3 playerPosition)
    {
        Vector3 direction = transform.position - playerPosition;
        direction.y = 0;
        print(direction.normalized);
        controller.Move(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player in viziune");
            FollowPlayer(other.transform.position);
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
}
