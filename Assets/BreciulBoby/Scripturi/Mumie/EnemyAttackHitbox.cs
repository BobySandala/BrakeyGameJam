using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public int uInt_DmgAmount = 1;
    public float f_AttackDelay = 1f;

    private float f_SeePlayerTime;
    private float f_AttackOnTime;
    [SerializeField]
    private bool b_ChargingAttack;
    public MummyController m_Controller;

    private BoxCollider bc_Collider;
    // Start is called before the first frame update
    void Start()
    {
        b_ChargingAttack = false;
        bc_Collider = GetComponent<BoxCollider>();
        bc_Collider.enabled = false;
        f_AttackOnTime = f_AttackDelay / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_ChargingAttack)
        {
            if (Time.time > f_SeePlayerTime + f_AttackDelay)
            {
                //poate ataca
                v_Attack();
                GetComponent<AudioSource>().Play();
            } else if (Time.time > f_SeePlayerTime + f_AttackOnTime)
            {
                DecativateHitbox();
            }
        } else
        {
            DecativateHitbox();
        }
    }

    private void v_Attack()
    {
        bc_Collider.enabled = true;
        f_SeePlayerTime = Time.time;
    }

    public void DecativateHitbox()
    {
        bc_Collider.enabled = false;
        print("hitbox dezactivat");
    }
    public void HandleExitBeforeDestroy()
    {
        print("gaina a iesit din collider (handled before destroy)");
        SetChargingAttack(false);
    }
    public void SetChargingAttack(bool b) { b_ChargingAttack = b; f_SeePlayerTime = Time.time; }
    public bool GetChargingAttack() {  return b_ChargingAttack; }
}

