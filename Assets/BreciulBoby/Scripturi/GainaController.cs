using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainaController : EnemyController
{
    public float fugeGaina = 5f;
    public GameObject glont;
    public float speedOu = 5f;
    public float lifeSpan = 10f;
    public float attackSpeed = 1f;
    public float nextAttackSpeed = 0f;
    
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

    public override void SeePlayer(Vector3 playerPosition)
    {
        base.SeePlayer(playerPosition);
        float distance = Vector3.Distance(transform.position, playerPosition);
        if (distance < fugeGaina)
        {
            base.moveEnemy(-(playerPosition - transform.position).normalized);
            
        }
        else if (Time.time >= nextAttackSpeed)
        {
            Vector3 direction = (playerPosition - transform.position).normalized;
            Vector3 startPosition = transform.position;
            GameObject ouInstance = Instantiate(glont, startPosition, Quaternion.identity);
            OuController ou = ouInstance.GetComponent<OuController>();
            if (ou != null)
            {
                ou.Initialize(startPosition, direction, speedOu, lifeSpan);
            }

            nextAttackSpeed = Time.time + attackSpeed;
        }
    }
}
