using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : EnemyMovementController
{
    // Start is called before the first frame update
    public bool b_Freeze;
    
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_Freeze) { A_SpriteAnimator.SetFloat("anim_speed", 0); return; }
        base.Update();
    }

    public void v_TakeDamage()
    {
        if (b_Shielded && i_HP == 2) { return; }
        print("mumia a fost atinsa");
        base.i_HP--;
        if (base.i_HP <= 0)
        {
            base.f_DeathTime = Time.time;
        }
    }

    public void v_TakeSajathaDamage()
    { 
        if (b_Shielded && i_HP == 2) { base.i_HP = 1; }
        print("sajathadamage");
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string s in base.s_TargetTags)
        {
            if (other.CompareTag(s))
            {
                if (base.enemyAttackHitbox != null)
                {
                    base.enemyAttackHitbox.SetSeePlayerTime(Time.time);
                    base.enemyAttackHitbox.SetChargingAttack(true);
                }
            }
            print("player a intrat in collider");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (base.enemyAttackHitbox != null)
            {
                //enemyAttackHitbox.SetSeePlayerTime(Time.time);
                base.enemyAttackHitbox.SetChargingAttack(false);
            }
        }
    }
}
