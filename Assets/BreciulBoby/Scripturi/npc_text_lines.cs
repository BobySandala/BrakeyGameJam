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

    private int current_message = -1;
    private bool writing_message = false;
    private bool vede_player = false;

    private Animator animator;

    public bool linesInOrder;

    // Start is called before the first frame update
    void Start()
    {
        textBubble.SetActive(false);
        animator = GetComponent<Animator>();
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
        } else
        {
        }
    }

    public void StartTyping()//reset writing area
    {
        if (messages.Length == 0) return;
        if (!linesInOrder)
        {
            current_message = Random.Range(0, messages.Length);
        }
        else
        {
            current_message++;
            if (current_message >= messages.Length)
            {
                current_message = 0;
            }
        }

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
            animator.SetTrigger("vorbeste");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("npc nu mai vede player");
            textBubble.SetActive(false);
            animator.SetTrigger("idle");
        }
    }
}
