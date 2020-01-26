using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{

    public float ZoomSpeed = 0.05f;

    void Update()
    {
        //Check if two fingers are on the screen simulataneously
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //Store the position of the two finger touches in the previous frame as a vector
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; // deltaPosition gives the change in position from the previous frame
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //Distance of the two fingers in the previous frame
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude; //Distance of the two fingers in the current frame

            // Find the difference between the two distances in order to see if the user is pinching in or out
            float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag; 

            //Store the current scale of the AR Session
            Vector3 OScale = transform.localScale;

            //Update the scale based on the deltachange change of the finger movement 
            Vector3 NScale = OScale + new Vector3(deltaMagnitudediff * ZoomSpeed, deltaMagnitudediff * ZoomSpeed, deltaMagnitudediff * ZoomSpeed);

            //Make sure the size doesn't go into negative or zero 
            NScale = Vector3.Max(NScale, new Vector3(0.1f, 0.1f, 0.1f));

            //Apply the changes back to the local scale
            transform.localScale = NScale;

        }
    }
}
