using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class village_UI : MonoBehaviour
{
    private bool activateText = false;

    public GameObject TextObject;
    public TextMeshProUGUI textUI;
    public string[] messages;

    private int messageIndex = 0;

    public float delay = 1f;
    private float activatedTime;
    // Start is called before the first frame update
    void Start()
    {
        TextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activateText)
        {
            if (Time.time > activatedTime + delay)
            {
                messageIndex++;
                if (messageIndex < messages.Length)
                {
                    textUI.text = messages[messageIndex];
                    activatedTime = Time.time;
                }
            }
        } else
        {
            messageIndex = 0;
        }
    }

    public void ActivateText()
    {
        activateText = true;
        activatedTime = Time.time;
        textUI.text = messages[messageIndex];
        TextObject.SetActive(true);
    }

    public void DeactivateText()
    {
        activateText = false;
        TextObject.SetActive(false);
    }

    public bool CanEnterLv1()
    {
        return messageIndex == messages.Length - 1;
    }
}
