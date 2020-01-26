using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{

    void Update()
    {
        //Rotate the object about its own axis (local axis of the object)
        transform.Rotate(5, 0, 0, Space.Self);
    }
}
