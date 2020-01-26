using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_highlight : MonoBehaviour
{
    GameObject HitObject;
    public GameObject Main_scene;

    public Material barrel_original, barrel_highlight;

    private string Barrel_name;
    private GameObject Barrel_reset;


    void Update()
    {
        RaycastHit hit_view;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10; 
        Ray ray_view = new Ray(transform.position, forward); //Casting a ray forward from the camera to see if the camera points at the object 

        if (Physics.Raycast(ray_view, out hit_view))
        {
            //If raycast hits the object store it as gameobject
            HitObject = hit_view.transform.gameObject;

            // Check if raycast hits any barrels which are tagged
            if (HitObject.tag == "Barrel_CF" || HitObject.tag == "Barrel_AP" || HitObject.tag == "Barrel_DR" || HitObject.tag == "Barrel_RR")
            {
                HitObject.GetComponent<Renderer>().material = barrel_highlight; //If raycast hits, highlight the object by changing the material 
                Barrel_name = hit_view.collider.gameObject.name;
                Barrel_reset = Main_scene.transform.Find("Scene_assets/Barrels/" + Barrel_name).gameObject; // Store the barrel which is hit by the raycast as gameobject
            }
            else if (Barrel_name != null && Barrel_reset.activeSelf)
            {
                //Check if the barrel is not destroyed, if not then change the material back to its original
                // If the barrel is destroyed then Barrel_reset.activeSelf would be false
                Barrel_reset.GetComponent<Renderer>().material = barrel_original;
                Barrel_name = null; // Reset the barrel name
            }

        }
    }
}

