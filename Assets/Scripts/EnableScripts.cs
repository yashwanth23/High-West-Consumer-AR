using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScripts : MonoBehaviour
{
    public GameObject ARCamera;

    //Enable the scripts on the AR Camera as soon as the scene is instantiated so as to avoid null object references from the scripts
    void Start()
    {
        ARCamera.GetComponent<Character_highlight>().enabled = true;
        ARCamera.GetComponent<OnTouch_play>().enabled = true;
        ARCamera.GetComponent<Barrel_pop>().enabled = true;
        ARCamera.GetComponent<Barrel_highlight>().enabled = true;
    }

}
