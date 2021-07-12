using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateCutscene : CameraState
{
    private readonly CameraController _cameraController;
    private Vector3 _positionToReach;
    private Vector3 _cameraVelocity = Vector3.zero;

    public override void StateUpdate()
    {
        _cameraController.transform.position = Vector3.SmoothDamp(_cameraController.transform.position, _positionToReach, ref _cameraVelocity, 0.4f);
    }

    public CameraStateCutscene(CameraController c)
    {
        _cameraController = c;
    }

    public void SetPositionToReach(Transform t)
    {
        _positionToReach = new Vector3(t.position.x, t.position.y, -10);
    }
}
