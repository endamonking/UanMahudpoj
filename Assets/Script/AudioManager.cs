using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Sorce")]
    public AudioSource sfxSrc;

    [Header("Clips")]
    public AudioClip fliping; 
    public AudioClip scored,hurt,gameOver; 

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }

}
