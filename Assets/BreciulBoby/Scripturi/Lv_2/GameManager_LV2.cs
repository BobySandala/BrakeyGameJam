using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class GameManager_LV2 : MonoBehaviour
{
    public MovementController player;
    public GameObject quickSilver;
    public lv2_UI_Controller UI_Controller;
    public AudioSource wind;

    [SerializeField]
    private Vector3 playerPositionAtContact;
    public float graity_in_sand = 0.1f;

    [SerializeField]
    private bool QS;

    public int numberOfKeyPresses = 4;
    public KeyCode[] KeyNames;

    private int keyIndex = 0;
    private int keyPresses = 0;

    [SerializeField]
    private bool gameEnded;
    [SerializeField]
    private bool fellOff;

    public float voidDepth = 0.5f;

    AsyncOperation async;

    

    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) GameEnded();
        if (fellOff) FellOff();


        if (QS)
        {
            wind.volume = 0.3f;
            player.transform.position += new Vector3(0, -graity_in_sand, 0);
            if (keyIndex < KeyNames.Length)
            {
                UI_Controller.SetInstructionText("key to press:" + KeyNames[keyIndex].ToString());
                //print("key to press:" +  KeyNames[keyIndex].ToString());
                //print("apasat de: " + keyPresses + " ori");
                if (Input.GetKeyDown(KeyNames[keyIndex]))
                {
                    print("apasat" + KeyNames[keyIndex]);
                    player.transform.position = (playerPositionAtContact + player.transform.position) / 2;
                    //UI_Controller.NextLine();
                    keyPresses++;
                    if (keyPresses >= numberOfKeyPresses)
                    {
                        keyIndex++;
                        keyPresses = 0;
                    }
                }
            }
            else
            {
                gameEnded = true;
                //UI_Controller.SetInstructionText("congrulations");
                //UI_Controller.GamePassed();
                QS = false;
            }
        }

        if (player.transform.position.y <= -voidDepth && !gameEnded)
        {
            if (!fellOff)
            {
                UI_Controller.GameEnded();
            }
            QS = false;
            fellOff = true;
        }

        if (fellOff && UI_Controller.TaceSlapnut() && !gameEnded)
        {
            UI_Controller.SetInstructionText("press space to retry");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (gameEnded && !fellOff)
        {
            load_Lv_2();
        }
    }

    private void load_Lv_2()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int previousIndex = currentIndex + 1;

        if (previousIndex >= 0) // Ensure it's not out of bounds
        {
            async.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("No previous scene to load!");
        }
    }

    private void GameEnded()
    {
        print("congrulation");
        //graity_in_sand = 0;
        player.transform.position += new Vector3(0, -graity_in_sand, 0);
    }

    private void FellOff()
    {
        print("felloff");
        graity_in_sand = 0;
    }

    public void QS_Activated()
    {
        print("QS");
        QS = true;
        quickSilver.GetComponent<MeshCollider>().enabled = false;
        player.GetComponent<MovementController>().gravity = 0;
        player.GetComponent<CharacterController>().enabled = false;
        playerPositionAtContact = player.transform.position;
        UI_Controller.TalkingStage();
    }
}
