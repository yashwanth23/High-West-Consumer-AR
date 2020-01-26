using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************************************************************************************************
 * This script is to lock the camera view to a particular object in the scene
 *************************************************************************************************************************************/

public class Camera_lookAT : MonoBehaviour
{
    public GameObject target;
    
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
