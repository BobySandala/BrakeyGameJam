using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSounds : MonoBehaviour
{
    public AudioClip AttackSound;
    public AudioClip TakeDamageSound;
    public AudioClip DeathSound;
    public AudioClip BubblePop;
    public AudioClip Revive;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void v_AttackSound()
    {
        audioSource.PlayOneShot(AttackSound);
    }
    public void v_TakeDamageSound()
    {
        audioSource.PlayOneShot(TakeDamageSound);
    }
    public void v_DeathSound()
    {
        audioSource.PlayOneShot(DeathSound);
    }
    public void v_BubblePopSound()
    {
        audioSource.PlayOneShot(BubblePop);
    }
    public void v_Revive()
    {
        audioSource.PlayOneShot(Revive);
    }
}
