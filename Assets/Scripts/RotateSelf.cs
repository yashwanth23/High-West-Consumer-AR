using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************************************************************************************************
 * This script is to make the game object spin around its own axis
 ************************************************************************************************************************************/

public class RotateSelf : MonoBehaviour
{
    public float speed = 0.5f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, speed*Time.deltaTime, Space.Self);
    }
}
