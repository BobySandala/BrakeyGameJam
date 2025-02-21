using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject guiCanvas;
    [SerializeField]
    private int gainiMoarte = 0;
    [SerializeField]
    private int lupiMorti = 0;
    private int totalLupi = 2;
    public KeyCode anfriz;
    public int nrGainiInfectate = 7;
    public int COUNTER_REAL = 0;
    [SerializeField]
    private int HP = 3;
    public GameObject[] gaini;
    public bool frezzeAll = false;

    private int gainiDezinfectate = 0;

    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private bool isGamePassed = false;
    private int numarGainiMoarte = 0;
    public MovementController player;
    

    // Start is called before the first frame update
    void Start()
    {
        HP = player.uInt_HP;
        totalLupi = GameObject.FindGameObjectsWithTag("Lup").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalLupi > GameObject.FindGameObjectsWithTag("Lup").Length)
        {
            totalLupi = GameObject.FindGameObjectsWithTag("Lup").Length;
            LupMort();
        }

        if (Input.GetKeyDown(anfriz))
        {
            print("111---");
            if (guiCanvas.GetComponent<UI_Controller_LV1>().TaceSlapnut())
            {
                print("111--+");
                if (isGameOver)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (isGamePassed)
                {
                    frezzeAll = true;
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int previousIndex = currentIndex - 1;

                    if (previousIndex >= 0) // Ensure it's not out of bounds
                    {
                        SceneManager.LoadScene(previousIndex);
                    }
                    else
                    {
                        Debug.LogWarning("No previous scene to load!");
                    }
                }
                else
                {
                    Time.timeScale = 1;
                    InfecteazaGainaRendam();
                    guiCanvas.GetComponent<UI_Controller_LV1>().NextPhase();
                    frezzeAll = false;
                }
            }
        }

        if (player != null)
        {
            if (player.uInt_HP < HP) 
            {
                playerDamage();
            }
        }



        if (HP <= 0 || numarGainiMoarte >= 3)
        {
            //game over
            isGameOver = true;
            frezzeAll = true;
            guiCanvas.GetComponent<UI_Controller_LV1>().GameOver();
        }
        if (gainiDezinfectate >= 5)
        {
            //game passed
            frezzeAll = true;
            isGamePassed = true;
            guiCanvas.GetComponent<UI_Controller_LV1>().GamePassed();
        }
        player.b_Freeze = frezzeAll;
        //cautaGaini();
    }

    public void MuceaLovit()
    {
        gainiDezinfectate++;
        guiCanvas.GetComponent<UI_Controller_LV1>().MuceaDied();
    }

    public void cautaGaini()
    {
        gaini = GameObject.FindGameObjectsWithTag("Gaina");
        print(gaini.Length);
    }

    public void playerDamage()
    {
        HP--;
        guiCanvas.GetComponent<UI_Controller_LV1>().InimaDied();
    }

    public void GainaMoarta()
    {
        guiCanvas.GetComponent<UI_Controller_LV1>().GainaDied();
        print("toggle - game manager");
        numarGainiMoarte++;
        if (numarGainiMoarte >= 3)
        {
            isGameOver = true;
        }
    }

    public void LupMort()
    {
        lupiMorti++;
        guiCanvas.GetComponent<UI_Controller_LV1>().LupDied();
        if (lupiMorti >= 2)
        {
            guiCanvas.GetComponent<UI_Controller_LV1>().NextPhase();
            frezzeAll = true;
            //Time.timeScale = 0;
        }
    }
    
    public void InfecteazaGainaRendam()
    {
        int nrGainiNormale = 0;
        cautaGaini();
        if (nrGainiInfectate <= 0) return;
        GainaController gaina = new GainaController();

        foreach (GameObject g in gaini)
        {
            if (!g.GetComponent<GainaController>().isInfected)
            {
                nrGainiNormale++;
            }
        }
        
        if(nrGainiNormale <= 0) return;
        
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
