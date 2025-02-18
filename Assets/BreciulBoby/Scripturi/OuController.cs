using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuController : Proiectil
{
    public SpriteRenderer spriteRenderer;
    public Sprite ouMisto;
    public Sprite ouSpart;
    private bool saSpart = false;
    
    public OuController(){}

    public void Initialize(Vector3 startPosition, Vector3 direction, float speed, float lifeSpan) 
    {
        base.Initialize(startPosition, direction, speed, lifeSpan);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = ouMisto;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!saSpart)
        {
            base.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Lup"))
        {
            spargeOul();
        }
    }
    
    public void spargeOul()
    {
        if (spriteRenderer != null && ouSpart != null)
        {
            spriteRenderer.sprite = ouSpart;
            saSpart = true;
        }

        StartCoroutine(sparjeOu(0.5f));
    }

    private IEnumerator sparjeOu(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
