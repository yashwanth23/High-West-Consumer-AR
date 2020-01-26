using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************************************************************************************************
 * This script is to detect if the screen is touched when the camera is pointed at highlightable character game object
 * Plays sound when the respective character is touched 
 **************************************************************************************************************************************************************************/

public class OnTouch_play : MonoBehaviour
{
    AudioSource playSound;
    GameObject HitObject;

    public AudioClip CF_voice, AP_voice, DR_voice, RR_voice;
    private bool touch_check; // To see if the object is touched atleast once

    void Start()
    {
        touch_check = false;
        playSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        //If any character is highlighted then look for touch raycast 
        if (Character_highlight.selection != 0)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                RaycastHit hit_touch;
                Ray ray_touch = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray_touch, out hit_touch) && !touch_check)
                {
                    HitObject = hit_touch.transform.gameObject;
                    if (HitObject.tag == "CF")
                    {
                        //If the character is touched once the sound starts plays and will not enter the if condition again 
                        playSound.clip = CF_voice;
                        playSound.Play();
                        touch_check = true;
                    }
                    else if (HitObject.tag == "AP")
                    {
                        playSound.clip = AP_voice;
                        playSound.Play();
                        touch_check = true;
                    }
                    else if (HitObject.tag == "RR")
                    {
                        playSound.clip = RR_voice;
                        playSound.Play();
                        touch_check = true;
                    }
                    else if (HitObject.tag == "DR")
                    {
                        playSound.clip = DR_voice;
                        playSound.Play();
                        touch_check = true;
                    }
                }
            }
        }
        else
        {
            //If the character is not highlighted the existing sound will stop playing
            playSound.Stop();
            touch_check = false;
            Debug.Log("Stopping play");
        }

    }
}
