using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************************************************************************************************
 * This script is for highlighting the character when mobile phone is pointed at it in AR
 * Shader material is overlaid on top of the actual material when the phone is pointed at it
 ****************************************************************************************************************************************************************/
public class Character_highlight : MonoBehaviour
{

    public GameObject Main_scene;

    public Material person, animal, highlight_person, highlight_animal;

    private GameObject char1, char2, char3, char4, RR_person;

    private GameObject HitObject;

    private bool playCheck;
    public static int selection;


    void Start()
    {
        //Find the characters from the scene or the prefab and store them as gameobjects
        char1 = Main_scene.transform.Find("Characters/Character - Campfire").gameObject;
        char2 = Main_scene.transform.Find("Characters/Character - AmericanPrairie").gameObject;
        char3 = Main_scene.transform.Find("Characters/Character - RendezvousRye").gameObject;
        char4 = Main_scene.transform.Find("Characters/Character - DoubleRye/Character - DoubleRye").gameObject;

        //The character is rigged to a horse part, so we have to find manually and store the gameobject
        //Find using tag doesn't work if the gameobject is a grandchild
        RR_person = char3.transform.Find("Horse_Rig_SHJntGrp/Horse_Rig_ROOTSHJnt/Horse_Rig_Spine_01SHJnt/Horse_Rig_Spine_02SHJnt/Character_Cowboy_01").gameObject;

        //Initially we do not want the script to go into else statement hence we use this flag to see if the raycast has hit any character 
        playCheck = true;
        selection = 0; //To find which character the raycast has hit
    }


    void Update()
    {
        RaycastHit hit_view;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Ray ray_view = new Ray(transform.position, forward); // Cast a ray from the camera in the forward direction

        if (Physics.Raycast(ray_view, out hit_view))
        {
            //If raycast hits the object store it as gameobject
            HitObject = hit_view.transform.gameObject;


            if (HitObject.tag == "CF")
            {
                //If raycast hits the character change the material to highlight material
                HitObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlight_person;
                selection = 1; // This value is to trigger the sound from the character on touch

                if (playCheck)
                {
                    playCheck = false;
                }

            }
            else if (HitObject.tag == "AP")
            {
                HitObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlight_animal;
                selection = 2;

                if (playCheck)
                {
                    playCheck = false;
                }

            }
            else if (HitObject.tag == "RR")
            {
                RR_person.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlight_person;

                HitObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlight_animal;
                selection = 3;

                if (playCheck)
                {
                    playCheck = false;
                }

            }
            else if (HitObject.tag == "DR")
            {
                HitObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = highlight_person;
                selection = 4;

                if (playCheck)
                {
                    playCheck = false;
                }

            }
            else
            {
                //If the raycast hits the character atleast once then playcheck is deactivated
                if (!playCheck)
                {
                    //The selection value is stored when the raycast immediately hits a character. When the raycast moves away the object that was hit previously will restore its material back to original
                    switch (selection)
                    {
                        case 1:
                            char1.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = person;
                            break;
                        case 2:
                            char2.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = animal;
                            break;
                        case 3:
                            char3.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = animal;
                            RR_person.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = person;
                            break;
                        case 4:
                            char4.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = person;
                            break;
                        default:
                            break;
                    }

                    selection = 0; // Default the selection to zero 

                }
            }

            Debug.Log(selection);
        }
    }
}
