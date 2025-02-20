using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quicksand_activation : MonoBehaviour
{
    public GameManager_LV2 game;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            game.QS_Activated();
        }
    }
}
