using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentSounds : MonoBehaviour
{
    public AudioClip swooshSound;
    public AudioClip chargeBow;
    public AudioClip shootArrow;
    public AudioClip changeWeapon;
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

    public void v_SwooshSound()
    {
        audioSource.PlayOneShot(swooshSound);
    }
    public void v_ChargeBow()
    {
        audioSource.PlayOneShot(chargeBow);
    }
    public void v_ShootArrow()
    {
        audioSource.PlayOneShot(shootArrow);
        print("sageata sunet");
    }
    public void v_StopSound()
    {
        audioSource.Stop();
    }
    public void v_ChangeWeapon()
    {
        audioSource.PlayOneShot(changeWeapon);
    }
}
