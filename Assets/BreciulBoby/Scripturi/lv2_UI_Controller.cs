using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class lv2_UI_Controller : MonoBehaviour
{

    public lv2_UI talkingStage;
    public lv2_UI gameOver;
    public lv2_UI gamePassed;

    public GameObject GO_talkingStage;
    public GameObject GO_gameOver;
    public GameObject GO_gamePassed;

    private int currentStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TalkingStage()
    {
        talkingStage.gameObject.SetActive(true);
        gamePassed.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);

        talkingStage.AFostSetatActiv();
        currentStage = 0;
    }

    public void GameEnded()
    {
        talkingStage.gameObject.SetActive(false);
        gamePassed.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);

        gameOver.AFostSetatActiv();
        currentStage = 1;
    }
    public void GamePassed()
    {
        talkingStage.gameObject.SetActive(false);
        gamePassed.gameObject.SetActive(true);
        gameOver.gameObject.SetActive(false);

        gamePassed.AFostSetatActiv();
        currentStage = 2;
    }
    public bool TaceSlapnut()
    {
        if (currentStage == 0)
        {
            return talkingStage.taceSlapnut;
        } else if (currentStage == 1)
        {
            return gameOver.taceSlapnut;
        }else
        {
            return gamePassed.taceSlapnut;
        }
    }

    public void SetInstructionText(string text)
    {
        if (currentStage == 0)
        {
            talkingStage.SetInstructionText(text);
        }
        else if (currentStage == 1)
        {
            gameOver.SetInstructionText(text);
        }
        else
        {
            gamePassed.SetInstructionText(text);
        }
    }

    public void NextLine()
    {
        if (currentStage == 0)
        {
            talkingStage.NextMessage();
        }
        else if (currentStage == 1)
        {
            gameOver.NextMessage();
        }
        else
        {
            gamePassed.NextMessage();
        }
    }
}
