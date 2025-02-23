using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSemnExcalamare : MonoBehaviour
{
    public float Amplitude;
    public float Frequency;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += Mathf.Cos(Time.time * Frequency) * Amplitude;
        transform.position = pos;
    }
}
