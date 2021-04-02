using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /**
     * Must be attached to : the main camera object that renders all the non-UI elements.
     * 
     * Description : Controller that for the camera to decide which element to follow, etc.
    **/

    // The object to follow.
    [SerializeField] GameObject objectToFollow = null;


    // Temporary x limits
    private float leftLim = -2.1f;
    private float rightLim = 2.1f;
    private float Xpos;

    private void Update()
    {
        // update the position of the camera depending on the object position.
        if(Xpos < 0)
        {
            Xpos = Mathf.Max(leftLim, objectToFollow.transform.position.x);
        } else {
            Xpos = Mathf.Min(rightLim, objectToFollow.transform.position.x);
        }
        transform.position = new Vector3(Xpos, transform.position.y, -10);
    }
}
