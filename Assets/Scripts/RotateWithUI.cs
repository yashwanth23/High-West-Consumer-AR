using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithUI : MonoBehaviour
{

    private float value = 0;

    void Update()
    {
        //Rotate the game object based on the slider translation value 
        // When we assign rotation to any object we need to convert the vector to Quaternion.Euler and assign the rotation
        transform.rotation = Quaternion.Euler(0, -45 + value * 360, 0);
    }

    //This function will be accessed from the slider gameobject in canvas
    public void RotateObject(float sliderValue)
    {
        value = sliderValue; 
    }
}
