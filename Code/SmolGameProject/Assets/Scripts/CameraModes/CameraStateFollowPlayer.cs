using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateFollowPlayer : CameraState
{
    private readonly CameraController _cameraController;
    private readonly GameObject _objectToFollow;

    // Hardcoded x limits
    private float _leftLim = -2f;
    private float _rightLim = 17.8f;
    private float _Xpos;

    public override void StateUpdate()
    {
        // update the position of the camera depending on the object position.
        if (_Xpos < 0)
        {
            _Xpos = Mathf.Max(_leftLim, _objectToFollow.transform.position.x);
        }
        else
        {
            _Xpos = Mathf.Min(_rightLim, _objectToFollow.transform.position.x);
        }
        _cameraController.transform.position = new Vector3(_Xpos, _cameraController.transform.position.y, -10);
    }

    public CameraStateFollowPlayer(CameraController c, GameObject o)
    {
        _cameraController = c;
        _objectToFollow = o;
    }
}
