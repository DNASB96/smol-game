using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateFollowPlayer : CameraState
{
    private CameraController cameraController;
    private GameObject objectToFollow;

    // Hardcoded x limits
    private float leftLim = -2f;
    private float rightLim = 17.8f;
    private float Xpos;

    public override void StateUpdate()
    {
        // update the position of the camera depending on the object position.
        if (Xpos < 0)
        {
            Xpos = Mathf.Max(leftLim, objectToFollow.transform.position.x);
        }
        else
        {
            Xpos = Mathf.Min(rightLim, objectToFollow.transform.position.x);
        }
        cameraController.transform.position = new Vector3(Xpos, cameraController.transform.position.y, -10);
    }

    public CameraStateFollowPlayer(CameraController c, GameObject o)
    {
        cameraController = c;
        objectToFollow = o;
    }
}
