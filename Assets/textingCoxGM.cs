using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class textingCoxGM : MonoBehaviour
{
    public bool trig_salmonela;
    private bool lastState;
    public GainaController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastState ^ trig_salmonela)
        {
            if (!lastState)
            {
                controller.iaSalmonela();
            }
            else
            {
                controller.vindecaSalmonela();
            }
        }
        lastState = trig_salmonela;
    }


}
