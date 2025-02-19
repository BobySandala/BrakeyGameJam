using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GainaController : EnemyController
{
    private int capDePerete = 0;
    
    public float fugeGaina = 5f;
    public GameObject glont;
    public float speedOu = 5f;
    public float lifeSpan = 10f;
    public float attackSpeed = 1f;
    public float nextAttackSpeed = 0f;

    public float patrolSpeed = 3f;
    public float fujeSpeed = 30f;
    [SerializeField]
    private bool isPatrolling = true;
    private float attackRadiusLup = 6.5f;
    public GameManagerScript gameManager;
    public bool isInfected = false;
    public Sprite gainaNormala;
    public Sprite gainaNebuna;

    // Start is called before the first frame update
    void Start()
    {
        base.patrolAreaSize = 25f;
        base.Start();
        base.setPatrolPointInRange();
        viatza = 10f;


        if (gameManager != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            //gameManager.adaugareGaina(this);
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = gainaNormala;
        }
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
        else
        {
            base.speed = fujeSpeed;
        }
        
        spriteRenderer.sprite = (isInfected) ? gainaNebuna : gainaNormala;

        if (capDePerete != 0)
        {
            print("gaina a dat cu capul de perete");
            //base.reversePatrolPoint();
        }
    }

    public void iaSalmonela()
    {
        isInfected = true;
        Debug.Log("gaina a luat salmonela!!!");
        this.patrolSpeed = 3.5f;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = gainaNebuna;
        }
    }
    
    public void SeePlayer(Vector3 playerPosition, string playerTag)
    {
        Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosXZ = new Vector2(playerPosition.x, playerPosition.z);
        
        float distance = Vector2.Distance(positionXZ, targetPosXZ);

        if (distance < attackRadiusLup && (playerTag == "Lup" || playerTag == "Armament"))
        {
            if (gameManager != null)
            {
                gameManager.GainaMoarta();
            }
            
            Destroy(gameObject);
        }
        print("distanta: " + distance);
        isPatrolling = true;
        if (distance < fugeGaina)
        {
            //int range1 = -4;
            //int range2 = 4;
            //Vector3 rangomTweak = new Vector3(Random.Range(range1, range2), 0, Random.Range(range1, range2));
            isPatrolling = false;
            base.moveEnemy(-(playerPosition - transform.position/* + rangomTweak*/).normalized);
            print("fuge gaina");
        }
        else if (Time.time >= nextAttackSpeed && isInfected)
        {
            Vector3 direction = (playerPosition - transform.position).normalized;
            Vector3 startPosition = transform.position;
            GameObject ouInstance = Instantiate(glont, startPosition, Quaternion.identity);
            OuController ou = ouInstance.GetComponent<OuController>();
            if (ou != null)
            {
                ou.Initialize(startPosition, direction, speedOu, lifeSpan);
                ou.transform.localScale = new Vector3(7f, 7f, 7f);
            }

            nextAttackSpeed = Time.time + attackSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPatrolling = true;
            base.setPatrolPointInRange();
        }

        if (other.CompareTag("Wall"))
        {
            capDePerete--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Lup"))
        {
            
            SeePlayer(other.transform.position, other.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Armament"))
        {
            Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosXZ = new Vector2(other.transform.position.x, other.transform.position.z);
            float distance = Vector2.Distance(positionXZ, targetPosXZ);
            print("distanta gaina " + distance);
            if (distance < 6.5f)
            {
                if (isInfected)
                {
                    isInfected = false;
                    gameManager.InfecteazaGainaRendam();
                    gameManager.InfecteazaGainaRendam();
                    spriteRenderer.sprite = gainaNormala;
                    gameManager.MuceaLovit();
                }
                else
                {

                    Object.Destroy(gameObject);
                }
            }
        }
        if (other.CompareTag("Wall"))
        {
            capDePerete++;
            base.setPatrolPointInRange();
        }
    }
    
}
