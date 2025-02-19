using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject guiCanvas;
    [SerializeField]
    private int gainiMoarte = 0;
    [SerializeField]
    private int lupiMorti = 0;
    public KeyCode anfriz;
    public int nrGainiInfectate = 7;
    public int COUNTER_REAL = 0;

    public GameObject[] gaini;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(anfriz))
        {
            if (guiCanvas.GetComponent<UI_Controller_LV1>().TaceSlapnut())
            {
                Time.timeScale = 1;
                InfecteazaGainaRendam();
                guiCanvas.GetComponent<UI_Controller_LV1>().NextPhase();
            }
        }
        
        //cautaGaini();
    }

    public void cautaGaini()
    {
        gaini = GameObject.FindGameObjectsWithTag("Gaina");
        print(gaini.Length);
    }

    public void playerDamage()
    {
        guiCanvas.GetComponent<UI_Controller_LV1>().InimaDied();
    }

    public void GainaMoarta()
    {
        guiCanvas.GetComponent<UI_Controller_LV1>().GainaDied();
    }

    public void LupMort()
    {
        lupiMorti++;
        guiCanvas.GetComponent<UI_Controller_LV1>().LupDied();
        if (lupiMorti == 3)
        {
            guiCanvas.GetComponent<UI_Controller_LV1>().NextPhase();
            //Time.timeScale = 0;
        }
    }
    
    public void InfecteazaGainaRendam()
    {
        cautaGaini();
        if (nrGainiInfectate <= 0) return;
        GainaController gaina = new GainaController();
        do
        {
            gaina = gaini[Random.Range(0, gaini.Length)].GetComponent<GainaController>();
        } while (gaina.isInfected || gaina == null);
        
        nrGainiInfectate--;
        COUNTER_REAL++;

        if (gaina != null)
        {
            gaina.iaSalmonela();
        }
    }
}
