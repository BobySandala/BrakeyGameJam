using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class npc_text_lines : MonoBehaviour
{
    public string[] messages;
    public TextMeshPro guiText;
    public GameObject textBubble;

    private int current_letter = 0;
    private string fullMessage;

    private float timer;
    private int letterIndex;
    public float pause = 0.1f;
    private float nextLetterTime = 0f;

    private int current_message;
    private bool writing_message = false;
    private bool vede_player = false;

    // Start is called before the first frame update
    void Start()
    {
        textBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

    public void StartTyping()//reset writing area
    {
        if (messages.Length == 0) return;
        current_message = Random.Range(0, messages.Length);

        fullMessage = messages[current_message];
        guiText.text = "";
        letterIndex = 0;
        writing_message = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("npc vede playerul");
            textBubble.SetActive(true);
            StartTyping();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("npc nu mai vede player");
            textBubble.SetActive(false);
        }
    }
}
