using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stepSound : MonoBehaviour
{
    private bool b_Start;
    public List<AudioClip> audioClips;
    public float f_Delay;
    public float f_Start;

    // Update is called once per frame
    void Update()
    {
        if (!b_Start) { return; }
        if (Time.time >= f_Start + f_Delay)
        {
            f_Start = Time.time;
            GetComponent<AudioSource>().PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
        }
    }

    public void v_SoundOn()
    {
        b_Start = true;
    }
    public void v_SoundOff()
    {
        b_Start = false;
    }
}
