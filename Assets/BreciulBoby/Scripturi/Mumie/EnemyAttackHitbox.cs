using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public int uInt_DmgAmount = 1;
    public float f_AttackDelay = 1f;

    private float f_SeePlayerTime;
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
            }
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
    }

    public void SetSeePlayerTime(float time) { f_SeePlayerTime = time; }
    public void SetChargingAttack(bool b) { b_ChargingAttack = b;}
    public bool GetChargingAttack() {  return b_ChargingAttack; }
}

