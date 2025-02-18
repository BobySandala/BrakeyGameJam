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
        guiCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(anfriz))
        {
            Time.timeScale = 1;
            guiCanvas.SetActive(false);
        }
        
        //cautaGaini();
    }

    public void cautaGaini()
    {
        gaini = GameObject.FindGameObjectsWithTag("Gaina");
        print(gaini.Length);
    }

    /*public void GainaMoarta()
    {
        gainiMoarte++;
        if (gainiMoarte == 3)
        {
            ToggleCanvas();
            Time.timeScale = 0;
        }
    }*/

    public void LupMort()
    {
        lupiMorti++;
        if (lupiMorti == 2)
        {
            InfecteazaGainaRendam();
            ToggleCanvas();
            Time.timeScale = 0;
        }
    }
    
    void ToggleCanvas()
    {
        if (guiCanvas != null)
        {
            guiCanvas.SetActive(!guiCanvas.activeSelf);
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
