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

    public float patrolSpeed = 3f;
    private bool isPatrolling = true;
    private float attackRadiusLup = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        base.patrolAreaSize = 25f;
        base.Start();
        base.setPatrolPoint();
        viatza = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isPatrolling)
        {
            base.speed = patrolSpeed;
            base.Patrol();
        }
    }
    
    public void SeePlayer(Vector3 playerPosition, string playerTag)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < attackRadiusLup && (playerTag == "Lup" || playerTag == "Armament"))
        {
            Destroy(gameObject);
        }
        
        if (distance < fugeGaina)
        {
            int range1 = -4;
            int range2 = 4;
            Vector3 rangomTweak = new Vector3(Random.Range(range1, range2), 0, Random.Range(range1, range2));
            isPatrolling = false;
            base.moveEnemy(-(playerPosition - transform.position + rangomTweak).normalized);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPatrolling = true;
            base.setPatrolPoint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Lup") || other.CompareTag("Armament"))
        {
            print("player in viziune");
            SeePlayer(other.transform.position, other.tag);
        }
        
        
    }
}
