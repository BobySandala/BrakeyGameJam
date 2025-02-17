using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuController : Proiectil
{
    public OuController(){}

    public void Initialize(Vector3 startPosition, Vector3 direction, float speed, float lifeSpan) 
    {
        base.Initialize(startPosition, direction, speed, lifeSpan);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
