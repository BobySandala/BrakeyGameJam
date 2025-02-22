using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        base.oneShotSounds.v_TakeDamageSound();
        if (b_Shielded && i_HP == 2) { return; }
        print("mumia a fost atinsa");
        base.i_HP--;
        if (base.i_HP <= 0)
        {
            base.oneShotSounds.v_DeathSound();
            base.f_DeathTime = Time.time;
        }
    }

    public void v_TakeSajathaDamage()
    { 
        if (b_Shielded && i_HP == 2) { base.i_HP = 1; base.oneShotSounds.v_BubblePopSound(); }
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
                    base.oneShotSounds.v_AttackSound();
                    base.enemyAttackHitbox.SetSeePlayerTime(Time.time);
                    base.enemyAttackHitbox.SetChargingAttack(true);
                }
            }
            print("player a intrat in collider");
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    bool b = false;
    //    foreach (string s in base.s_TargetTags)
    //    {
    //        if (other.CompareTag(s))
    //        {
    //            if (base.enemyAttackHitbox != null)
    //            {
    //                b = true;
    //                //base.enemyAttackHitbox.SetSeePlayerTime(Time.time);
    //                /*base.enemyAttackHitbox.SetChargingAttack(true);*/
    //            }
    //        }
    //        print("player a intrat in collider");
    //    }
    //    print("lupu vede pe cineva");
    //    base.enemyAttackHitbox.SetChargingAttack(b);
    //}

    private void OnTriggerExit(Collider other)
    {
        foreach (string s in base.s_TargetTags)
        {
            if (other.CompareTag(s))
            {
                if (base.enemyAttackHitbox != null)
                {
                    base.enemyAttackHitbox.SetChargingAttack(false);
                }
            }
            print("player a iesit din collider");
        }
    }
}
