using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public MummyController MC_controller;

    private void OnTriggerEnter(Collider other)
    {
        //print("mumia ceva coliziune");
        //print($"Collided with: {other.gameObject.name}, Tag: {other.gameObject.tag}");

        if (other.CompareTag("Armament"))
        {
            //print("mumia este atinsa cred");
            if (MC_controller != null)
            {
                MC_controller.v_TakeDamage();
            }
        }
    }
}
