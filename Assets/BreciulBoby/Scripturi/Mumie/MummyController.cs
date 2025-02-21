using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : EnemyMovementController
{
    // Start is called before the first frame update
    


    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void v_TakeDamage()
    {
        print("mumia a fost atinsa");
        base.i_HP--;
        if (base.i_HP <= 0)
        {
            base.f_DeathTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (base.enemyAttackHitbox != null)
            {
                base.enemyAttackHitbox.SetSeePlayerTime(Time.time);
                base.enemyAttackHitbox.SetChargingAttack(true);
            }
        }
        print("player a intrat in collider");
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
