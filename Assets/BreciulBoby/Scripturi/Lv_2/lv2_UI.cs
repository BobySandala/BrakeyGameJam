using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lv2_UI : MonoBehaviour
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

    private int currentSprite;
    public Sprite[] SlepNuth_Sprites;
    public Image SlepNuth_Sprite;
    public float f_delayTime = 1;

    public bool b_LineByLine = false;
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

    public void NextMessage()
    {
        if (currentSprite < SlepNuth_Sprites.Length)
        {
            SlepNuth_Sprite.sprite = SlepNuth_Sprites[currentSprite++];
        }else
        {
            currentSprite = 0;
        }
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

    public void SetInstructionText(string text)
    {
        PressSpace.text = text;
    }

    void DialogueEnding()
    {

    }

    void Awake() { Debug.Log("Awake called on " + gameObject.name); }
    //void Start() { Debug.Log("Start called on " + gameObject.name); }
    void OnEnable() { Debug.Log("OnEnable called on " + gameObject.name); }

    private IEnumerator WaitBeforeNextMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextMessage();
    }
    // Update is called once per frame
    void Update()
    {

        //print("coxare maxima");
        
        //PressSpace.text = pressSpaceText;

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
                    if (current_message == messages.Length - 1)
                    {
                        taceSlapnut = true;
                    } else
                    if (b_LineByLine)
                    {
                        StartCoroutine(WaitBeforeNextMessage(f_delayTime));
                        //NextMessage(); 
                    }
                }
            }
        }
    }
}
