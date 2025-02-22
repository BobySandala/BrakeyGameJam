using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameRate;
    private int frameCounter = 0;
    private float frameCounterTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= frameCounterTimer + frameRate)
        {
            frameCounterTimer = Time.time;
            
            GetComponent<Image>().sprite = sprites[frameCounter];
            frameCounter++;
            if (frameCounter >= sprites.Length)
            {
                frameCounter = 0;
            }
        }
    }
}
