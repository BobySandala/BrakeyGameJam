using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public lv2_UI reference;
    
    // Start is called before the first frame update
    void Start()
    {
        if (reference != null)
        {
            reference.AFostSetatActiv();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reference.taceSlapnut)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
