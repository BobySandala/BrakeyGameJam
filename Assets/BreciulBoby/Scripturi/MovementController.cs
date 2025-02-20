using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public GameManagerScript gameManager;

    private bool CanAttack = true;

    public float lupAttackDelay = 1f;
    private float lupAttackTimeStart = 0f;
    private bool lupAttacking = false;

    private Animator animator;
    // 0 - Left; 1 - Right; 2 - Fwd; 3 - Bwd
    public GameObject[] ArmamentSwoosh;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (gameManager != null) 
        { 
            if (gameManager.frezzeAll) 
            {
                animator.SetFloat("anim_speed", 0);
                return; 
            } else
            {
                animator.SetFloat("anim_speed", 0.5f);
            }
        }
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKeyDown(moveLeft) && CanAttack) { animator.SetTrigger("stanga"); GetComponent<SpriteRenderer>().flipX = false; }
        if (Input.GetKeyDown(moveRight) && CanAttack) { animator.SetTrigger("dreapta"); GetComponent<SpriteRenderer>().flipX = false; }
       
        if (Input.GetKey(moveLeft)) { moveX = -1f; lastPressedKey = moveLeft; }
        if (Input.GetKey(moveRight)) { moveX = 1f; lastPressedKey = moveRight; }
        if (Input.GetKey(moveForward)) { moveZ = 1f; lastPressedKey = moveForward; }
        if (Input.GetKey(moveBackward)) { moveZ = -1f; lastPressedKey = moveBackward; }
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

        if (lupAttacking)
        {
            print("time: " + Time.time);
            print("lupattack time start: " + lupAttackTimeStart);
            if (Time.time >= lupAttackTimeStart + lupAttackDelay)
            {
                //iti iei dmg
                print("te-a muscat lupul de buci");
                lupAttackTimeStart = Time.time;
                gameManager.playerDamage();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lup"))
        {
            lupAttackTimeStart = Time.time;
            lupAttacking = true;
        }
        if (other.CompareTag("Ou"))
        {
            Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosXZ = new Vector2(other.transform.position.x, other.transform.position.z);
            float distance = Vector2.Distance(positionXZ, targetPosXZ);

            print("distanta player pu lup:" + distance);
            if (distance < 10f)
            {
                //incaseaza dmg
                gameManager.playerDamage();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lup"))
        {
            lupAttacking = false;
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        //Debug.Log("Attack performed! Last pressed key: " + lastPressedKey);
        CanAttack = false;

        float mouseX = Input.mousePosition.x - Screen.width / 2;
        float mouseY = Input.mousePosition.y - Screen.height / 2;

        if (mouseX > 0)
        {
            //dreapta ecraului
            if (mouseY > mouseX)
            {
                //dreapta sus deci ataca in sus
                ArmamentSwoosh[2].SetActive(true);
                //GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (mouseY > -mouseX)
            {
                //dreapta mijloc deci ataca la dreapta
                ArmamentSwoosh[1].SetActive(true);
                GetComponent<SpriteRenderer>().flipX = false;
            } else
            {
                //dreapta jos deci ataca in jos
                ArmamentSwoosh[3].SetActive(true);
                //GetComponent<SpriteRenderer>().flipX = false;
            }
        } else
        {
            //stanga ecranului
            if (mouseY < mouseX)
            {
                //stanga jos deci ataca in sus
                ArmamentSwoosh[3].SetActive(true);
                //GetComponent<SpriteRenderer>().flipX = false;
            } else if (mouseY < -mouseX)
            {
                //stanga mijloc deci ataca in stanga
                ArmamentSwoosh[0].SetActive(true);
                GetComponent<SpriteRenderer>().flipX = true;
            } else
            {
                //staga sus deci ataca in jos
                ArmamentSwoosh[2].SetActive(true);
                //GetComponent<SpriteRenderer>().flipX = false;
            }
        }


        StartCoroutine(DelayAndSwooshInactive(SwooshOnTime));
        
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
