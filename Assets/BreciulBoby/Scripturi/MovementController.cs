using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = 9.81f;
    public float slopeLimit = 45.5f;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode attackKey = KeyCode.Space;
    private CharacterController controller;
    private Vector3 velocity;
    private KeyCode lastPressedKey;
    public float SwooshOnTime = 0.2f;

    private bool CanAttack = true;

    // 0 - Left; 1 - Right; 2 - Fwd; 3 - Bwd
    public GameObject[] ArmamentSwoosh;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        controller.slopeLimit = slopeLimit;
        lastPressedKey = KeyCode.None;

        if (ArmamentSwoosh != null)
        {
            foreach(GameObject ar in ArmamentSwoosh)
            {
                ar.SetActive(false);
            }
        }
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(moveLeft) && CanAttack) { moveX = -1f; lastPressedKey = moveLeft; }
        if (Input.GetKey(moveRight) && CanAttack) { moveX = 1f; lastPressedKey = moveRight; }
        if (Input.GetKey(moveForward) && CanAttack) { moveZ = 1f; lastPressedKey = moveForward; }
        if (Input.GetKey(moveBackward) && CanAttack) { moveZ = -1f; lastPressedKey = moveBackward; }
        if (Input.GetKeyDown(attackKey) && CanAttack) { Attack(); }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move.normalized * speed * Time.deltaTime);

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

    void Attack()
    {
        //Debug.Log("Attack performed! Last pressed key: " + lastPressedKey);
        CanAttack = false;
        if (lastPressedKey != KeyCode.None)
        {
            if (lastPressedKey == moveLeft)
            {
                ArmamentSwoosh[0].SetActive(true);
            }
            else if (lastPressedKey == moveRight)
            {
                ArmamentSwoosh[1].SetActive(true);
            }
            else if (lastPressedKey == moveForward)
            {
                ArmamentSwoosh[2].SetActive(true);
            }
            else if (lastPressedKey == moveBackward)
            {
                ArmamentSwoosh[3].SetActive(true);
            }

            StartCoroutine(DelayAndSwooshInactive(SwooshOnTime));
        }
    }

    IEnumerator DelayAndSwooshInactive(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ArmamentSwoosh != null)
        {
            foreach (GameObject ar in ArmamentSwoosh)
            {
                ar.SetActive(false);
            }
        }
        CanAttack = true;
        print("yeye");
    }
}
