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
    public float pause = 0.1f;
    public string[] messages;
    private int current_message;
    public TextMeshProUGUI guiText;
    private bool writing_message = false;
    public bool taceSlapnut = false;

    void Start()
    {
        spacePressed = false;
        PressSpace.text = "";
        //message = guiText.text;
        current_message = 0;
        guiText.text = ""; // Clear the GUI text
        StartCoroutine(TypeLetters());
    }

    IEnumerator TypeLetters()
    {
        writing_message = true;

        // Ensure the current message is valid
        if (messages == null || messages.Length == 0 || current_message >= messages.Length)
        {
            yield break; // Exit if messages array is empty or invalid
        }

        string currentText = messages[current_message]; // Get the full message
        print(currentText); // Print the entire message

        // Clear the text before typing
        guiText.text = "";

        // Iterate over each letter
        foreach (char letter in currentText)
        {
            print(letter); // Print each letter
            guiText.text += letter; // Add a single character to the GUI text

            yield return new WaitForSeconds(pause); // Wait before the next letter
        }

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
        StartCoroutine(TypeLetters());
    }

    void DialogueEnding()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
