using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MummySpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float f_SpawnTimer;
    private float f_SpawnStartTime;
    private bool b_Spawning = false;

    public int i_SpawnMaxCount = 10;
    private int i_SpawnCount = 0;

    public MummyController mummy;
    void Start()
    {
        
    }
    public void v_StartSpawning()
    {
        f_SpawnStartTime = Time.time;
        b_Spawning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!b_Spawning) { return; }
        if (i_SpawnCount >= i_SpawnMaxCount) { return; }
        if (Time.time > f_SpawnStartTime + f_SpawnTimer)
        {
            i_SpawnCount++;
            Instantiate(mummy, transform.position, Quaternion.identity);
            f_SpawnStartTime = Time.time;

        }
    }
}
