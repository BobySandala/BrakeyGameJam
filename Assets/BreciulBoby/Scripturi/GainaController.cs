using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GainaController : EnemyMovementController
{
    public Animator animatorGainaNeagra;
    public float f_FrightRadius = 3;
    public float f_ShootRadius = 5;
    [SerializeField]
    private bool b_IsFright = false;
    public bool isInfected;
    public string[] s_EnemiesTags = { "Player", "Lup" };
    public float f_PerlinNoiseSpeed;
    public OuController ouPrefab;

    private float noiseOffsetX;
    private float noiseOffsetZ;

    public float f_OuSpeed;
    public float f_OuLifeSpan;

    //attack speed ou
    public float f_ShootAttackSpeed;
    //momentulincare a aruncat ultimul ou
    private float f_ShootAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        f_ShootAttackTime = Time.time;
        // Random offsets to make movement unique per object
        noiseOffsetX = Random.Range(0f, 100f);
        noiseOffsetZ = Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Walk();
        v_ShootEgg();
    } 


    public void iaSalmonela()
    {
        if (isInfected) { return; }
        isInfected = true;
        animatorGainaNeagra.gameObject.SetActive(true);
        A_SpriteAnimator.gameObject.SetActive(false);
        //A_ShieldAnimator = animatorGainaNeagra;
    }
    public void vindecaSalmonela()
    {
        if (!isInfected) { return; }
        isInfected = false;
        animatorGainaNeagra.gameObject.SetActive(false);
        A_SpriteAnimator.gameObject.SetActive(true);
    }

    private void v_ShootEgg()
    {
        if (!isInfected) { return; }
        if (Time.time < f_ShootAttackTime + f_ShootAttackSpeed) { return; }
        f_ShootAttackTime = Time.time;
        GameObject go = go_SearchPlayerInsideShootRadius();
        if (go == null) { return; }

        Vector3 dir = (go.transform.position - transform.position).normalized;
        OuController ou = Instantiate(ouPrefab, transform);
        if (ou != null)
        {
            ou.Initialize(transform.position, dir, f_OuSpeed, f_OuLifeSpan);
        }

        //print("shootEgg motherfuker");
    }
    private void Walk()
    {
        GameObject[] enemies = go_SearchAllEnemiesInsideFleeRadius();

        if (enemies.Length > 0)
        {
            b_IsFright = true;
            Vector3 fleeDirection = Vector3.zero;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                float weight = 1f - (distance / f_FrightRadius); // Closer enemies have higher weight

                Vector3 awayFromEnemy = (transform.position - enemy.transform.position).normalized * weight;
                awayFromEnemy.y = 0;
                fleeDirection += awayFromEnemy;
            }
            fleeDirection.Normalize(); // Ensure movement is smooth
            MoveCharacter(fleeDirection);
        }
        else
        {
            b_IsFright = false;
            if (!b_IsFright)
            {
                float time = Time.time * f_PerlinNoiseSpeed;

                // Generate Perlin noise for X and Z directions, mapped to [-1,1]
                float noiseX = (Mathf.PerlinNoise(time + noiseOffsetX, 0f) * 2f) - 1f;
                float noiseZ = (Mathf.PerlinNoise(time + noiseOffsetZ, 100f) * 2f) - 1f;

                // Create a normalized movement vector
                Vector3 movementDirection = new Vector3(noiseX, 0, noiseZ).normalized;

                // Call the inherited MoveCharacter method
                base.MoveCharacter(movementDirection);
            }
        }
    }

    //returneaza array cu toate target-urile de care se sperie gaina si sunt in raza de speriat
    private GameObject[] go_SearchAllEnemiesInsideFleeRadius()
    {
        List<GameObject> enemiesInRadius = new List<GameObject>();

        foreach (string s in s_EnemiesTags)
        {
            GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag(s);

            foreach (GameObject enemy in foundEnemies)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(enemy.transform.position, transform.position);
                    if (distance < f_FrightRadius)
                    {
                        enemiesInRadius.Add(enemy);
                    }
                }
            }
        }
        return enemiesInRadius.ToArray(); // Convert List back to array
    }
    //returneaza GameObject player daca este in raza de tras dar in afara razei de fugit
    private GameObject go_SearchPlayerInsideShootRadius()
    {
        GameObject enemiesInRadius = null;
        GameObject enemy = GameObject.FindGameObjectWithTag("Player");

        if (enemy != null)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance > f_FrightRadius && distance < f_ShootRadius)
            {
                enemiesInRadius = enemy;
            }
        }
        
        return enemiesInRadius; // Convert List back to array
    }

}
