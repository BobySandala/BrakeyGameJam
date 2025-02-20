using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class toggleDedPng : MonoBehaviour
{
    public Image[] images;
    public Sprite[] alive;
    public Sprite[] ded;

    private int[] frame_index;

    private int number_of_ded;
    private int number_of_imagini;

    private float frameRate = 0.1f;
    private float timer = 0f;

    private bool[] dedOralIve;
    // Start is called before the first frame update
    void Start()
    {
        number_of_imagini = images.Length;
        dedOralIve = new bool[number_of_imagini]; //fals insemneaza viu
        frame_index = new int[number_of_imagini];
        for (int i = 0; i < number_of_imagini; i++)
        {
            dedOralIve[i] = false;
            frame_index[i] = 0;
        }
        
    }

    public void Died()
    {
        
        if (number_of_ded < number_of_imagini)
        {
            if (images[number_of_ded] != null)
            {
                dedOralIve[number_of_ded] = true;
                frame_index[number_of_ded] = 0;
                print("toggleDedPng");
            }
            number_of_ded++;
        }
    }

    // Update is called once per frame
    void Update() 
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            for (int i = 0; i < number_of_imagini; i++)
            {
                if (dedOralIve[i])
                {
                    //inseamna ca imaginea i este ded
                    if (frame_index[i] < ded.Length)
                    {
                        images[i].sprite = ded[frame_index[i]];
                        frame_index[i]++;
                    }
                    else
                    {
                        frame_index[i] = 0;
                        images[i].sprite = ded[frame_index[i]];
                    }
                }
                else
                {
                    //inseamna ca nu
                    if (frame_index[i] < alive.Length)
                    {
                        images[i].sprite = alive[frame_index[i]];
                        frame_index[i]++;
                    }
                    else
                    {
                        frame_index[i] = 0;
                        images[i].sprite = alive[frame_index[i]];
                    }
                }
            }
        }

    }
}
