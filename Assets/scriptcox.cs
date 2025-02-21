using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptcox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"Collidedcox with: {other.gameObject.name}, Tag: {other.gameObject.tag}");
    }
}
