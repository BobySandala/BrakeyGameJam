using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class slepNath_Pause : MonoBehaviour
{
    public bool taceSlapnut;

    private string pressSpaceText = "Press Space";
    private bool spacePressed;

    public TextMeshProUGUI PressSpace;
    public GameObject textBubble;
    public Image textBubbleImage;
    public GameObject pauseImage;
    public TextMeshProUGUI textBubbleText;
    public Image SlepNathSprite;
    public string[] slepNath_Lines;
    public lv1SoudController SoundController;
    private bool b_SlepNatheTheme = false;
    //for write speed
    public float pause = 0.1f;
    //for frame rate
    public float frameRate = 1f;

    //in ordinea apaitiei
    public Sprite[] slepNath_Sprites_paused;
    public Sprite[] slepNath_Sprite_dialogue;

    private int currentSprite_paused = 0;
    private int currentSprite_dialogue = 0;

    //state 0 - joc pe pauza, state 1 - dialog
    private int state = 0;
    private float timer;
    private float timer_paused_frames = 0f;

    private int current_message;
    private int letterIndex;
    private string fullMessage;
    private bool writing_message;

    // Start is called before the first frame update
    void Start()
    {
        textBubbleText.text = "";
        textBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == 0)
        {
            part1();
        } else
        {
            part2();
        }
    }

    void part1()
    {
        PressSpace.text = "";
        if (Time.time > timer_paused_frames + frameRate)
        {
            if (currentSprite_paused < slepNath_Sprites_paused.Length)
            {
                SlepNathSprite.color = Color.white;
                SlepNathSprite.sprite = slepNath_Sprites_paused[currentSprite_paused];
                currentSprite_paused++;
                timer_paused_frames = Time.time;
            } else
            {
                state = state + 1;
                textBubble.SetActive(true);
                pauseImage.SetActive(false);
            }
        }
    }

    void part2()
    {
        print("UI: a intrat in part2");
        if (!b_SlepNatheTheme)
        {
            SoundController.gameObject.SetActive(true);
            SoundController.v_SlepNathTheme();
            b_SlepNatheTheme = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (!writing_message)
            {
                if (current_message < slepNath_Lines.Length)
                {
                    NextMessage();
                    spacePressed = false;
                    if (currentSprite_dialogue < slepNath_Sprite_dialogue.Length)
                    {
                        SlepNathSprite.sprite = slepNath_Sprite_dialogue[currentSprite_dialogue];
                        currentSprite_dialogue++;
                    } else
                    {
                        currentSprite_dialogue = 0;
                        SlepNathSprite.sprite = slepNath_Sprite_dialogue[currentSprite_dialogue];
                    }
                }
                else
                {
                    DialogueEnding();
                }
            }
        }

        if (!writing_message)
        {
            if (spacePressed)
            {
                pressSpaceText = "i meant enter";
            }
            else
            {
                pressSpaceText = "press space";
            }
        }
        else
        {
            pressSpaceText = "";
        }
        PressSpace.text = pressSpaceText;

        if (writing_message)
        {
            timer += Time.deltaTime;
            if (timer >= pause)
            {
                timer = 0;
                if (letterIndex < fullMessage.Length)
                {
                    textBubbleText.text += fullMessage[letterIndex];
                    letterIndex++;
                }
                else
                {
                    writing_message = false; // Typing finished
                }
            }
        }
    }

    void NextMessage()
    {
        current_message++;
        if (current_message >= slepNath_Lines.Length)
        {
            taceSlapnut = true;
            return;
        }
        textBubbleText.text = "";
        //StartCoroutine(TypeLetters());
        StartTyping();
    }
    public void StartTyping()
    {
        if (slepNath_Lines.Length == 0 || current_message >= slepNath_Lines.Length) return;

        fullMessage = slepNath_Lines[current_message];
        textBubbleText.text = "";
        letterIndex = 0;
        writing_message = true;
    }
    void DialogueEnding()
    {

    }
    public void AFostSetatActiv()
    {
        print("a fost setat activ");
        textBubbleText.text = "";
        //StartCoroutine(TypeLetters());
        StartTyping();
        timer_paused_frames = Time.time;
        pauseImage.SetActive(true);
    }
}
