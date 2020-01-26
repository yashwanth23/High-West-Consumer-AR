using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private AudioSource ambient_sound;

    void Start()
    {
        //Initiate individual ambient sound when they spawn at the start
        //Play sound as soon as it is enabled
        ambient_sound = GetComponent<AudioSource>();
        ambient_sound.Play();
    }
    
}
