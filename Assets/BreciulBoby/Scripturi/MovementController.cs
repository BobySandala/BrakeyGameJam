using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    public KeyCode changeWeaponKey = KeyCode.Q;
    private CharacterController controller;
    private Vector3 velocity;
    private KeyCode lastPressedKey;
    public float SwooshOnTime = 0.2f;

    //public GameManagerScript gameManager;

    private bool CanAttack = true;

    public float lupAttackDelay = 1f;
    private float lupAttackTimeStart = 0f;
    private bool lupAttacking = false;

    public bool b_Freeze = false;

    public int uInt_HP;
    private Animator animator;
    // 0 - Left; 1 - Right; 2 - Fwd; 3 - Bwd
    public GameObject[] ArmamentSwoosh;
    //0 topor, 1 arc
    public int i_EquippedWeapon;

    private int i_BowFramesLength = 6;
    private bool b_AttackHeld;
    private float f_ChargeStartTime;
    public float f_BowFullCharge = 1;
    private int i_ChargeFrame;

    public Sprite[] SpritesBowUp;
    public Sprite[] SpritesBowDown;
    public Sprite[] SpritesBowLeft;
    public Sprite[] SpritesBowRight;

    public Sajatha sajatha;

    public bool b_BouEquipped;
    private bool b_Ded = false;
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
        if (b_Ded) { return; }
        if (!b_BouEquipped) { i_EquippedWeapon = 0; }
        if (b_Freeze)
        {
            animator.SetFloat("anim_speed", 0);
            return;
        } else
        {
            animator.SetFloat("anim_speed", 0.5f);
        }

        if (uInt_HP <= 0 && !b_Ded)
        {
            animator.SetTrigger("moarte");
            b_Ded = true;
            return;
        }
        if (b_Ded && uInt_HP > 0)
        {
            b_Ded = false;
            animator.SetTrigger("invie");
        }

        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKeyDown(moveLeft) && CanAttack) { animator.SetTrigger("stanga"); GetComponent<SpriteRenderer>().flipX = false; }
        if (Input.GetKeyDown(moveRight) && CanAttack) { animator.SetTrigger("dreapta"); GetComponent<SpriteRenderer>().flipX = false; }
        if (Input.GetKeyDown(moveForward) && CanAttack) { animator.SetTrigger("sus"); GetComponent<SpriteRenderer>().flipX = false; }
        if (Input.GetKeyDown(moveBackward) && CanAttack) { animator.SetTrigger("jos"); GetComponent<SpriteRenderer>().flipX = false; }
       
        if (Input.GetKeyDown(changeWeaponKey)) { i_EquippedWeapon = (i_EquippedWeapon + 1) % 2; }
        if (Input.GetKey(moveLeft)) { moveX = -1f; lastPressedKey = moveLeft; }
        if (Input.GetKey(moveRight)) { moveX = 1f; lastPressedKey = moveRight; }
        if (Input.GetKey(moveForward)) { moveZ = 1f; lastPressedKey = moveForward; }
        if (Input.GetKey(moveBackward)) { moveZ = -1f; lastPressedKey = moveBackward; }
        if (Input.GetKeyDown(attackKey) && CanAttack) { Attack(); b_AttackHeld = true; f_ChargeStartTime = Time.time; }
        if (Input.GetKeyUp(attackKey)) { b_AttackHeld = false; SpawnSajatha();  GetComponent<Animator>().enabled = true; }

        if (b_AttackHeld) { v_ChargeBowle(); }

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
                //gameManager.playerDamage();
            }
        }

    }
    public void v_SetDed(bool b)
    {
        b_Ded = b;
    }
    private void SpawnSajatha()
    {
        if (!b_BouEquipped) { return; }
        if (i_EquippedWeapon != 1) { return; }
        if (i_ChargeFrame < 5) { return; }

        float mouseX = Input.mousePosition.x - Screen.width / 2;
        float mouseY = Input.mousePosition.y - Screen.height / 2;

        //complete here to determine f_Angle
        float f_Angle = Mathf.Atan2(mouseX, mouseY) * Mathf.Rad2Deg;

        Quaternion v3_Direction = Quaternion.Euler(0, f_Angle, 0);
        Vector3 v3_StartPosition = transform.position;
        Sajatha sajathaInstance = Instantiate(sajatha, v3_StartPosition, v3_Direction);
        if (sajathaInstance != null)
        print("sajatha spaunata");
    }
    public void TakeDamage(GameObject GO_source)
    {
        if (uInt_HP <= 0) { return; }
        print("player a luat damage");
        EnemyAttackHitbox hitbox = GO_source.GetComponent<EnemyAttackHitbox>();
        hitbox.DecativateHitbox();
        uInt_HP -= hitbox.uInt_DmgAmount;
    }

    public void TakeDamage()
    {
        if (uInt_HP <= 0) { return; }
        uInt_HP--;
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
                //gameManager.playerDamage();
                TakeDamage();
            }
        }

        if(other.CompareTag("EnemyDamage"))
        {
            print("enemy damage");
            TakeDamage(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lup"))
        {
            lupAttacking = false;
        }
    }

    private void v_ChargeBowle()
    {
        if (!b_BouEquipped) { return; }
        if  (i_EquippedWeapon != 1) { return; }

        GetComponent<Animator>().enabled = false;
        float f_ChargingTime = Time.time - f_ChargeStartTime;
        float f_ChargingPercent = f_ChargingTime / f_BowFullCharge;
        
        if (f_ChargingPercent >= 1) { f_ChargingPercent = 0.99f; }
        i_ChargeFrame = Mathf.FloorToInt(f_ChargingPercent * 6);
        print("charge boule " + i_ChargeFrame);

        switch(i_MouseCadran())
        {
            case 0:
                //stanga
                GetComponent<SpriteRenderer>().sprite = SpritesBowLeft[i_ChargeFrame];
                break;
            case 1:
                //dreapta
                GetComponent<SpriteRenderer>().sprite = SpritesBowRight[i_ChargeFrame];
                break;
            case 2:
                //sus
                GetComponent<SpriteRenderer>().sprite = SpritesBowUp[i_ChargeFrame];
                break;
            case 3:
                //jos
                GetComponent<SpriteRenderer>().sprite = SpritesBowDown[i_ChargeFrame];
                break;
            default:
                break;
        }
    }

    //0 stanga | 1 dreapta | 2 sus | 3 jos
    private int i_MouseCadran()
    {
        float mouseX = Input.mousePosition.x - Screen.width / 2;
        float mouseY = Input.mousePosition.y - Screen.height / 2;

        if (mouseX > 0)
        {
            //dreapta ecraului
            if (mouseY > mouseX) { return 2; }
            else if (mouseY > -mouseX) { return 1; }
            else { return 3; }
        }
        else
        {
            if (mouseY < mouseX) { return 3; }
            else if (mouseY < -mouseX) { return 0; }
            else { return 2; }
        }
    }

    void Attack()
    {
        if (i_EquippedWeapon == 0)
        {
            animator.SetTrigger("attack");
            //Debug.Log("Attack performed! Last pressed key: " + lastPressedKey);
            CanAttack = false;
            ArmamentSwoosh[i_MouseCadran()].SetActive(true);
            ArmamentSwoosh[i_MouseCadran()].GetComponent<BoxCollider>().enabled = true;
            if (i_MouseCadran() == 0) { GetComponent<SpriteRenderer>().flipX = true; }
            Physics.SyncTransforms(); // Force Unity to recognize the new collider

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
