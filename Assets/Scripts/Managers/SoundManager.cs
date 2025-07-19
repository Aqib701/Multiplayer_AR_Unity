using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    // ** Deals with Sound and it's related settings **
    
    public static AudioSource audioSource;
    public bool isMute = false;
    public bool isMuteMusic= false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       

    }
    
    public static void PlaySound(AudioClip Clip)
    {
        audioSource.clip = Clip;
        audioSource.Play();
        
        
    }
    
    public void ToggleMusic()
    {
        isMuteMusic = !isMuteMusic;

    }
    

    public void ToggleAudio()
    {

        isMute = !isMute;


        if (isMute)
        {

            audioSource.volume = 0;

        }
        else
        {
            audioSource.volume = 1;
        }




    }
    
    


    

    
    
    
}
