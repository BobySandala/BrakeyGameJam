using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpeed : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("animSpeed", Random.Range(0.3f, 0.7f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
