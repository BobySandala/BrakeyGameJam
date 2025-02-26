using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ambiental;
    public GameObject musica;

    public GameObject guiCanvas;
    [SerializeField]
    private int gainiMoarte = 0;
    [SerializeField]
    private int lupiMorti = 0;
    private int totalLupi = 2;
    public KeyCode anfriz;
    public int COUNTER_REAL = 0;
    [SerializeField]
    private int HP = 3;
    public GameObject[] gaini;
    public bool frezzeAll = false;

    [SerializeField]
    private int gainiDezinfectate = 0;

    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private bool isGamePassed = false;
    private int numarGainiMoarte = 0;
    public MovementController player;
    [SerializeField]
    private int i_CurrentState;

    private int i_lupiVi = 2;
    public lv1SoudController SoundController;

    // Start is called before the first frame update
    void Start()
    {
        i_CurrentState = 0;
        HP = player.uInt_HP;
        totalLupi = GameObject.FindGameObjectsWithTag("Lup").Length;
        
        //SoundController.v_FirstPhase();
    }

    // Update is called once per frame
    void Update()
    {
        switch (i_CurrentState)
        {
            case 0:
                v_phase1();
                break;
            case 1:
                v_phase2();
                break;
            case 2:
                v_phase3();
                break;
            case 3:
                v_Phase4();
                break;
            case 4:
                break;
            default:
                break;
        }
        //cautaGaini();
    }

    private void v_phase1()
    {
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
            i_CurrentState = 3;
        }

        i_lupiVi = GameObject.FindGameObjectsWithTag("Lup").Length;
        if (totalLupi > GameObject.FindGameObjectsWithTag("Lup").Length)
        {
            for (int i = 0; i < totalLupi - i_lupiVi; i++)
            {
                LupMort();
            }
            totalLupi = GameObject.FindGameObjectsWithTag("Lup").Length;
        }
        if (i_lupiVi <= 0)
        {
            guiCanvas.GetComponent<UI_Controller_LV1>().v_SetPhase2();
            frezzeAll = true;
            ambiental.SetActive(false);
            musica.SetActive(false);
            player.b_Freeze = frezzeAll;
            i_CurrentState++;
        }
        if (numarGainiMoarte >= 3)
        {
            //game over

            isGameOver = true;
            frezzeAll = true;
            player.b_Freeze = frezzeAll;
            guiCanvas.GetComponent<UI_Controller_LV1>().GameOver();
            i_CurrentState++;
        } 
    }
    private void v_phase2()
    {
        if (Input.GetKeyDown(anfriz))
        {
            print("111---");
            if (guiCanvas.GetComponent<UI_Controller_LV1>().TaceSlapnut())
            {
                ambiental.SetActive(true);
                musica.SetActive(true);
                SoundController.v_SecondPhase();
                i_CurrentState++;
                guiCanvas.GetComponent<UI_Controller_LV1>().v_SetPhase3();
                for (int i = 0; i < 3 - HP; i++)
                {
                    guiCanvas.GetComponent<UI_Controller_LV1>().InimaDied();
                }
                for (int i = 0; i < numarGainiMoarte; i++)
                {
                    guiCanvas.GetComponent<UI_Controller_LV1>().GainaDied();
                }
                InfecteazaGainaRendam();
                frezzeAll = false;
                player.b_Freeze = frezzeAll;
                v_FreezeChickens();
            }
        }
    }
        
    
    private void v_phase3()
    {
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
            i_CurrentState = 3;
        }
        if (gainiDezinfectate >= 5)
        {
            //game passed
            frezzeAll = true;
            isGamePassed = true;
            guiCanvas.GetComponent<UI_Controller_LV1>().GamePassed();
            i_CurrentState = 3;
        }
        player.b_Freeze = frezzeAll;
        v_FreezeChickens();
    }

    public void v_Phase4()
    {
        if (!guiCanvas.GetComponent<UI_Controller_LV1>().TaceSlapnut()) { return; }
        if (Input.GetKeyDown(anfriz))
        {
            print("111--+");
            if (isGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (isGamePassed)
            {
                frezzeAll = true;
                v_FreezeChickens();
                player.b_Freeze = frezzeAll;
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
    }

    public void MuceaLovit()
    {
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
        /*if (numarGainiMoarte >= 3)
        {
            isGameOver = true;
        }*/
    }
    private void v_FreezeChickens()
    {
        foreach (GameObject g in gaini)
        {
            if (g != null)
            {
                g.GetComponent<GainaController>().freeze = frezzeAll;
            }
        }
    }
    public void LupMort()
    {
        lupiMorti++;
        guiCanvas.GetComponent<UI_Controller_LV1>().LupDied();
    }
    
    public void v_GainaVindecata()
    {
        gainiDezinfectate++;
        if (gainiDezinfectate > 5) { isGamePassed = true; return; }
        MuceaLovit();
        if (COUNTER_REAL < 5) { InfecteazaGainaRendam(); }
    }

    public void InfecteazaGainaRendam()
    {
        int nrGainiNormale = 0;
        cautaGaini();
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
        
        COUNTER_REAL++;

        if (gaina != null)
        {
            gaina.iaSalmonela();
        }
    }
}
