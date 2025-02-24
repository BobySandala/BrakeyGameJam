using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public MummyController MC_controller;
    public GainaController GainaController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Armament"))
        {
            if (MC_controller != null) { MC_controller.v_TakeDamage(); }
            
        }
        if (GainaController != null) 
        { 
            if (other.CompareTag("EnemyDamage"))
            {
                print("gaina a luat damage");
                other.gameObject.GetComponent<EnemyAttackHitbox>().HandleExitBeforeDestroy();
                GainaController.v_TakeDamage();
            }
        }

        if (other.CompareTag("Sajatha"))
        {
            if (MC_controller != null) { MC_controller.v_TakeSajathaDamage(); }
            print("sajatha damage puternic");
        }
    }
}