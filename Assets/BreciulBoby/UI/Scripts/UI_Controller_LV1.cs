using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Controller_LV1 : MonoBehaviour
{
    public GameObject ph_1;
    public GameObject ph_2;
    public GameObject ph_3;
    public GameObject GameOverPhase;
    public GameObject GamePassedPhase;

    public Phase_1_Controller_LV_1 phase1;
    public slepNath_Pause phase_2;
    public Phase_1_Controller_LV_1 phase_3;

    public phase_2_UI phase_GO;
    public phase_2_UI phase_GP;
    private bool gameOver;
    private bool gamePassed;

    private int current_phase = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gamePassed = false;
        if (ph_1 != null && ph_2 != null && ph_3 != null)
        {
            SetAllActive(false); 
        }
        ph_1.SetActive(true);
    }

    private void SetAllActive(bool active)
    {
        ph_1.SetActive(active);
        ph_2.SetActive(active);
        ph_3.SetActive(active);
        GameOverPhase.SetActive(active);
        GamePassedPhase.SetActive(active);
    }

    public void NextPhase()
    {
        

        switch (current_phase) 
        {
            case 0:
                ph_1.SetActive(true);
                break;
            case 1:
                ph_2.SetActive(true);
                phase_2.AFostSetatActiv();
                break;
            case 2:
                ph_3.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void v_SetPhase1()
    {
        if (gameOver || gamePassed) return;
        print("current phase" + current_phase);
        SetAllActive(false);
        current_phase = 0;
        ph_1.SetActive(true);
    }
    public void v_SetPhase2()
    {
        if (gameOver || gamePassed) return;
        print("current phase" + current_phase);
        SetAllActive(false);
        current_phase = 1;
        ph_2.SetActive(true);
        phase_2.AFostSetatActiv();
    }
    public void v_SetPhase3()
    {
        if (gameOver || gamePassed) return;
        print("current phase" + current_phase);
        SetAllActive(false);
        current_phase = 2;
        ph_3.SetActive(true);
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        SetAllActive(false);
        GameOverPhase.SetActive(true);
        GameOverPhase.GetComponent<phase_2_UI>().enabled = true;
        //myObject.GetComponent<MyScript>().enabled = true;
        phase_GO.AFostSetatActiv();
    }

    public void GamePassed() 
    {
        if(gamePassed) return;
        gamePassed = true; 
        SetAllActive(false);
        GamePassedPhase.SetActive(true);
        phase_GP.AFostSetatActiv();
    }

    public void LupDied()
    {
        phase1.DedLup();
        
    }
    public void GainaDied()
    {
        phase1.DedGaina();
        print("toggle - UI controller");
        phase_3.DedGaina();
    }
    public void InimaDied()
    {
        phase1.DedInima();
        phase_3.DedInima();
    }
    public void MuceaDied()
    {
        phase_3.DedLup();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool TaceSlapnut()
    {
        bool taceSlapnut = false;
        if (current_phase == 1)
        {
            return phase_2.taceSlapnut;
        }
        else if (gameOver)
        {
            return phase_GO.taceSlapnut;
        }
        else if (gamePassed)
        {
            return phase_GP.taceSlapnut;
        }
        return taceSlapnut;
    }
}
