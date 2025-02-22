using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv1SoudController : MonoBehaviour
{
    public AudioClip MelodiePhase1;
    public AudioClip MelodiePhase2;
    public AudioClip SlepNathTheme;

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

    public void v_FirstPhase()
    {
        audioSource.loop = true;
        audioSource.clip = MelodiePhase1;
        audioSource.Play();
        print("muzica ph1");
    }
    public void v_SecondPhase()
    {
        audioSource.loop = true;
        audioSource.clip = MelodiePhase2;
        audioSource.Play();
        print("muzica ph2");
    }
    public void v_SlepNathTheme()
    {
        audioSource.loop = true;
        audioSource.clip = SlepNathTheme;
        audioSource.Play();
        print("muzica ph2");
    }
}
