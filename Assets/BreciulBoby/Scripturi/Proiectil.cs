using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiectil : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 direction;
    private float speed;
    private float lifeSpan;

    public Proiectil(){}
    
    public void Initialize(Vector3 startPosition, Vector3 direction, float speed, float lifeSpan)
    {
        this.startPosition = startPosition;
        this.direction = direction;
        this.speed = speed;
        this.lifeSpan = lifeSpan;
        transform.position = startPosition;
        
        Destroy(gameObject, lifeSpan);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
