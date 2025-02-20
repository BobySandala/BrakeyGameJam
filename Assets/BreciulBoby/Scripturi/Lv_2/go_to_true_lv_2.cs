using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go_to_true_lv_2 : MonoBehaviour
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
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int previousIndex = currentIndex + 1;

        if (previousIndex >= 0) // Ensure it's not out of bounds
        {
            SceneManager.LoadScene(previousIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene to load!");
        }
    }
}
