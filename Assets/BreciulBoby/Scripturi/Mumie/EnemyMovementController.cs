using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyAttackHitbox enemyAttackHitbox;
    private GameObject GO_player;
    protected bool b_PlayerClose = false;
    protected float f_PlayerEnterTime = 0;
    public Animator A_SpriteAnimator;
    public Animator A_ShieldAnimator;

    public float f_gravity = 9.81f;
    public float f_WalkSpeed = 4f;
    public float f_AnimSpeed = 0.5f;
    [SerializeField]
    protected int i_HP = 2;
    public bool b_Shielded;

    [SerializeField]
    private Vector3 v3_MovingDirection;
    private Vector3 v3_LastPosition;
    private Vector3 v3_velocity;
    private CharacterController CC_controller;

    //cheastii pentru animator
    [SerializeField]
    private int i_CurrentAnimState;
    private int i_IdleState = 0;
    private int i_WalkLeftState = 1;
    private int i_WalkRightState = 2;
    private int i_AttackLeftState = 3;
    private int i_AttackRightState = 4;
    private int i_DieState = 5;

    public float f_StopRange = 5f;

    protected float f_DeathTime;
    public float f_DeathLengthTime;

    public string[] s_TargetTags;

    private float f_Retargeting;
    public float f_RetargetingTime = 1;

    protected void Start()
    {
        v3_LastPosition = transform.position;
        CC_controller = GetComponent<CharacterController>();
        v_ChooseTarget();
        //animator = GetComponent<Animator>();
    }

    private void v_ChooseTarget()
    {
        int i_Index = 0;
        int i_Index2 = 0;
        float f_Dist = 1000000;
        List<GameObject> g = new List<GameObject>();
        foreach (string s in s_TargetTags)
        {
            GameObject[] gg = GameObject.FindGameObjectsWithTag(s);
            foreach (GameObject ggg in gg)
            {
                g.Add(ggg);
                if (Vector3.Distance(transform.position, ggg.transform.position) > f_Dist)
                {
                    f_Dist = Vector3.Distance(transform.position,ggg.transform.position);
                    i_Index = i_Index2;
                }
                i_Index2++;
            }
        }
        GO_player = g[i_Index];
    }

    // Update is called once per frame
    protected void Update()
    {
        if (A_SpriteAnimator != null)
        {
            A_SpriteAnimator.SetFloat("anim_speed", f_AnimSpeed);
        }
        if (Time.time > f_Retargeting)
        {
            f_Retargeting = Time.time + f_RetargetingTime;
            v_ChooseTarget();
        }

        if (GO_player != null)
        {
            print("am gasit playerul la pozitia: " + GO_player.transform.position);
        }
        v_CalculateDirection();
        v_AnimController();
        v_FollowPlayer();

        if (i_HP <= 0)
        {
            if (Time.time >= f_DeathTime + f_DeathLengthTime)
            {
                Destroy(gameObject);
            }
        }

        if (i_HP == 1 && b_Shielded)
        {
            if (A_ShieldAnimator != null)
            {
                A_ShieldAnimator.SetFloat("animSpeed", 1);
            }
        }
    }

    private void OnDestroy()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameManagerLv2_Pestera gm = go.GetComponent<GameManagerLv2_Pestera>();
        if (gm != null) { gm.v_MummyDed(); }
    }

    private void v_FollowPlayer()
    {
        if (GO_player == null) { return; }

        if (!CC_controller.isGrounded)
        {
            v3_velocity.y -= f_gravity * Time.deltaTime;
        }
        else
        {
            v3_velocity.y = -2f;
        }
        CC_controller.Move(v3_velocity * Time.deltaTime);

        float f_Distance = Vector3.Distance(transform.position, GO_player.transform.position);
        Vector3 v3_PlayerDirection = GO_player.transform.position - transform.position;
        v3_PlayerDirection.Normalize();

        print("distanta: " + f_Distance);
        if (f_Distance > f_StopRange) { MoveCharacter(v3_PlayerDirection); }
    }

    private void v_AnimController()
    {
        if (A_SpriteAnimator == null) { return; }
        if (i_CurrentAnimState == i_DieState) { return; }

        if (i_HP <= 0 && i_CurrentAnimState != i_DieState)
        {
            i_CurrentAnimState = i_DieState;
            A_SpriteAnimator.SetTrigger("moare");
            return;
        }

        if (enemyAttackHitbox.GetChargingAttack())
        {
            float f_PlayerLRPos = GO_player.transform.position.x - transform.position.x;
            print("partea incare e playerul: " + f_PlayerLRPos);
            if (f_PlayerLRPos < 0 && i_CurrentAnimState != i_AttackLeftState)
            {
                //attack in stanga
                i_CurrentAnimState = i_AttackLeftState;
                A_SpriteAnimator.SetTrigger("ataca_st");
            }
            else if (f_PlayerLRPos >= 0 && i_CurrentAnimState != i_AttackRightState)
            {
                //attack in dreapta
                i_CurrentAnimState = i_AttackRightState;
                A_SpriteAnimator.SetTrigger("ataca_dr");
            }
            return;
        }
        
        
        if (v3_MovingDirection == Vector3.zero && i_CurrentAnimState != i_IdleState)
        {
            i_CurrentAnimState = i_IdleState;
            A_SpriteAnimator.SetTrigger("idle");
            return;
        }
        else
        {
            if (v3_MovingDirection.x > 0)
            {
                if (i_CurrentAnimState != i_WalkLeftState)
                {
                    i_CurrentAnimState = i_WalkLeftState;
                    A_SpriteAnimator.SetTrigger("merge_st");
                }
            }
            else if (v3_MovingDirection.x < 0)
            {
                if (i_CurrentAnimState != i_WalkRightState)
                {
                    i_CurrentAnimState = i_WalkRightState;
                    A_SpriteAnimator.SetTrigger("merge_dr");
                }
            }
            return;
        }
    }

    private void v_CalculateDirection()
    {
        v3_MovingDirection = v3_LastPosition - transform.position;
        v3_LastPosition = transform.position;
    }

    protected void MoveCharacter(Vector3 v3_direction_norm)
    {
        if (i_HP <= 0) { return; }   
        CC_controller.Move(v3_direction_norm * f_WalkSpeed * Time.deltaTime);
    }
}
