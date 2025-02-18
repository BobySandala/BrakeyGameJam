using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleDedPng : MonoBehaviour
{

    public Image[] images;
    public Sprite alive;
    public Sprite ded;

    private int number_of_ded;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image image in images)
        {
            if (image != null)
            {
                image.sprite = alive;
            }
        }
        number_of_ded = 0;
    }

    public void Died()
    {
        if (number_of_ded < images.Length)
        {
            if (images[number_of_ded] != null)
            {
                images[number_of_ded].sprite = ded;
            }
            number_of_ded++;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
