using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class phase_2_UI : MonoBehaviour
{
    public TextMeshProUGUI PressSpace;

    private string pressSpaceText;
    private bool spacePressed;
    public string[] messages;
    private int current_message;
    public TextMeshProUGUI guiText;
    private bool writing_message = false;
    public bool taceSlapnut = false;

    private int current_letter = 0;
    private string fullMessage;

    private float timer;
    private int letterIndex;
    public float pause = 0.1f;
    private float nextLetterTime = 0f;
    void Start()
    {
        Debug.Log("Start called on " + gameObject.name);
        spacePressed = false;
        PressSpace.text = "";
        //message = guiText.text;
        current_message = 0;
        guiText.text = ""; // Clear the GUI text
        //StartCoroutine(TypeLetters());
        //ScriemLitere();
    }

    public void AFostSetatActiv()
    {
        print("a fost setat activ");
        //StartCoroutine(TypeLetters());
        StartTyping();
    }

    public void StartTyping()
    {
        if (messages.Length == 0 || current_message >= messages.Length) return;

        fullMessage = messages[current_message];
        guiText.text = "";
        letterIndex = 0;
        writing_message = true;
    }

    private IEnumerator TypeLetters()
    {
        writing_message = true;

        // Validate messages array and index
        if (messages == null || messages.Length == 0 || current_message >= messages.Length)
        {
            Debug.LogError("Invalid messages array or index out of bounds!");
            yield break;
        }

        // Ensure guiText is assigned
        if (guiText == null)
        {
            Debug.LogError("guiText is not assigned!");
            yield break;
        }

        // Ensure pause is valid
        if (pause <= 0)
        {
            Debug.LogError("Pause time must be greater than zero!");
            yield break;
        }

        string fullMessage = messages[current_message];
        guiText.text = ""; // Clear previous text

        Debug.Log($"Starting to type message: {fullMessage}");

        // Iterate over each letter and display it one by one
        foreach (char letter in fullMessage)
        {
            Debug.Log($"Typing letter: {letter}");
            guiText.text += letter;
        }
            yield return new WaitForSeconds(pause);

        Debug.Log("Finished typing message.");
        writing_message = false;
    }

    void NextMessage()
    {
        current_message++;
        if (current_message >= messages.Length)
        {
            taceSlapnut = true;
            return;
        }
        guiText.text = "";
        //StartCoroutine(TypeLetters());
        StartTyping();
    }

    void DialogueEnding()
    {

    }

    void Awake() { Debug.Log("Awake called on " + gameObject.name); }
    //void Start() { Debug.Log("Start called on " + gameObject.name); }
    void OnEnable() { Debug.Log("OnEnable called on " + gameObject.name); }


    // Update is called once per frame
    void Update()
    {

        print("coxare maxima");
        
        PressSpace.text = pressSpaceText;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        if (!writing_message)
        {
            if (spacePressed)
            {
                pressSpaceText = "i meant enter";
            } else
            {
                pressSpaceText = "press space";
            }
        } else
        {
            PressSpace.text = "";
        }



        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (!writing_message)
            {
                if (current_message < messages.Length)
                {
                    NextMessage();
                    spacePressed = false;
                } else
                {
                    DialogueEnding();
                }
            }
        }

        if (writing_message)
        {
            timer += Time.deltaTime;
            if (timer >= pause)
            {
                timer = 0;
                if (letterIndex < fullMessage.Length)
                {
                    guiText.text += fullMessage[letterIndex];
                    letterIndex++;
                }
                else
                {
                    writing_message = false; // Typing finished
                }
            }
        }
    }
}
