using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public float attackRadiusLup = 6.5f;
    public GameManagerScript gameManager;
    public bool isInfected = false;
    public Sprite gainaNormala;
    public Sprite gainaNebuna;
    [SerializeField]
    private bool b_Speriata;
    private bool b_GainaMoarta;

    [SerializeField]
    private Vector3 DirectiaInCareFuge;

    private float f_DestroyTimer;
    private float f_TeleportTimer;
    private bool b_Teleport = false;
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
        if (b_GainaMoarta)
        {
            if (b_Teleport)
            {
                if (Time.time > f_DestroyTimer + 1)
                {
                    Destroy(gameObject);
                }
            } else if (Time.time > f_TeleportTimer + 0.1f)
            {
                transform.position = new Vector3(1000, 1000, 1000);
                b_Teleport = true;
                f_DestroyTimer = Time.time;
            }
            return;
        }
        v_SearchAllEnemies();
        if (!b_Speriata)
        {
            //base.speed = patrolSpeed;
            base.Patrol();
        }
        else
        {
            //base.speed = fujeSpeed;
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

        GetComponent<AudioSource>().Play();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = gainaNebuna;
        }
    }

    private void v_SearchAllEnemies()
    {
        bool anyLoop = false;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        GameObject[] l = GameObject.FindGameObjectsWithTag("Lup");
        Vector3 dir = Vector3.zero;
        foreach (GameObject p2 in l)
        {
            float dist = Vector3.Distance(transform.position, p2.transform.position);
            if (dist <= fugeGaina)
            {
                anyLoop = true;
                
                dir.x += (p2.transform.position - transform.position).x;
                dir.z += (p2.transform.position - transform.position).z;
            }
        }
        if (Vector3.Distance(transform.position, p.transform.position) < fugeGaina)
        {
            anyLoop = true;
            dir.x += p.transform.position.x;
            dir.z += p.transform.position.z;
        }
        else if (Time.time >= nextAttackSpeed && isInfected)
        {
            Vector3 direction = (p.transform.position - transform.position).normalized;
            Vector3 startPosition = transform.position;
            GameObject ouInstance = Instantiate(glont, startPosition, Quaternion.identity);
            OuController ou = ouInstance.GetComponent<OuController>();
            if (ou != null && !gameManager.frezzeAll)
            {
                ou.Initialize(startPosition, direction, speedOu, lifeSpan);
                ou.transform.localScale = new Vector3(7f, 7f, 7f);
            }

            nextAttackSpeed = Time.time + attackSpeed;
        }
        
        dir *= -1;
        DirectiaInCareFuge = dir;
        b_Speriata = anyLoop;
        if (b_Speriata)
        {
            moveEnemy(dir.normalized);
        }
    }
    
    public void SeePlayer(Vector3 playerPosition, string playerTag)
    {
        Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPosXZ = new Vector2(playerPosition.x, playerPosition.z);
        
        float distance = Vector2.Distance(positionXZ, targetPosXZ);

        if (distance < attackRadiusLup && (playerTag == "Lup" || playerTag == "Armament" || playerTag == "EnemyDamage"))
        {
            if (gameManager != null)
            {
                print("toggle - gaina controller");
                //gameManager.GainaMoarta();
            }
            print("toggle - am omorat gaina");
            DestroyObject();
            //GetComponent<BoxCollider>().enabled = false;
            //GetComponent<CharacterController>().enabled = false;
            //gameObject.SetActive(false);
        }
        //print("distanta: " + distance);
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
            if (ou != null && !gameManager.frezzeAll)
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
            
            //SeePlayer(other.transform.position, other.tag);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Armament") || other.CompareTag("EnemyDamage"))
        {
            Vector2 positionXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosXZ = new Vector2(other.transform.position.x, other.transform.position.z);
            float distance = Vector2.Distance(positionXZ, targetPosXZ);
            print("distanta gaina " + distance);
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
                if (gameManager != null)
                {
                    //print("toggle - gaina controller");
                    gameManager.GainaMoarta();
                }
                //DestroyObject();
                //GetComponent<BoxCollider>().enabled = false;
                //GetComponent<CharacterController>().enabled = false;
                //gameObject.SetActive(false);
                //Object.Destroy(gameObject);
            }
            
        }
        if (other.CompareTag("Wall"))
        {
            capDePerete++;
            base.setPatrolPointInRange();
        }
    }

    void DestroyObject()
    {
        //transform.position = new Vector3(1000, 1000, 1000);
        b_GainaMoarta = true;
        f_TeleportTimer = Time.time;
        //Destroy(gameObject);
    }

}
