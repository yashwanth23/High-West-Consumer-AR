﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_pop : MonoBehaviour
{
    public GameObject dest_barrel;
    public ParticleSystem smokeFX;
    public ParticleSystem[] ingredients_CF;
    public ParticleSystem[] ingredients_AP;
    public ParticleSystem[] ingredients_RR;
    public ParticleSystem[] ingredients_DR;
    private ParticleSystem flavor;
    private ParticleSystem[] ingre;
    Ray ray;
    GameObject item;

    public GameObject MainScene;

    // Start is called before the first frame update
    void Start()
    {
        ingre = new ParticleSystem[4];
    }

    // Update is called once per frame
    void Update()
    {
        // Look for any touch 
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Check if the raycast touch hits any barrel object with tag
                if (hit.transform.gameObject.tag == "Barrel_CF")
                {
                    Destroy_barrel(ingredients_CF, hit);
                }

                else if (hit.transform.gameObject.tag == "Barrel_AP")
                {

                    Destroy_barrel(ingredients_AP, hit);
                }

                else if (hit.transform.gameObject.tag == "Barrel_RR")
                {

                    Destroy_barrel(ingredients_RR, hit);
                }

                else if (hit.transform.gameObject.tag == "Barrel_DR")
                {

                    Destroy_barrel(ingredients_DR, hit);
                }

            }
        }

        //Check if flavor particle system is initiated 
        if (flavor)
        {
            //If the flavor is dead then destroy the particle system in order to reduce the performance load
            if (!flavor.IsAlive())
            {
                Destroy(flavor.gameObject);
                print("smoke stopped");
            }
        }

        //Check if Ingredient particle systems are initiated 
        for(int i=0; i< 4; i++)
        {
            if (ingre[i])
            {
                //If the ingredient particle systems are dead then destroy them to reduce load on performance
                if (!ingre[i].IsAlive())
                {
                    Destroy(ingre[i].gameObject);
                }
            }
        }

    }

    public void Destroy_barrel(ParticleSystem[] Ingredients, RaycastHit hit)
    {
        //Get the height of the barrel in order to instantiate ingredient particle effects from inside the barrel
        float size_obj = hit.collider.gameObject.GetComponent<Renderer>().bounds.size.y;

        //Instantiate whiskey smoke as child of Main scene in order to make sure it stays along with the parent when we externally manipulate the scene (rotation or zoom)
        flavor = Instantiate(smokeFX, hit.collider.gameObject.transform.position, smokeFX.gameObject.transform.rotation, MainScene.transform);

        //Instantiate ingredient particle effects when the raycast touch hits the barrel as child of Main Scene
        for (int i = 0; i < 4; i++)
        {
            ingre[i] = Instantiate(Ingredients[i], hit.collider.gameObject.transform.position + new Vector3(0, size_obj / 2, 0), smokeFX.gameObject.transform.rotation, MainScene.transform);
        }

        //Destroy the existing barrel and place the breakable barrel asset at the exact location as the original barrel
        //This destructable barrel also plays sound through the audio source which is attached to it
        Instantiate(dest_barrel, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation, MainScene.transform);
        Destroy(hit.collider.gameObject);
    }
}
