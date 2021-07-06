using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraState
{
    /**
     * Abstract class for different camera modes.
     * 
     * StateUpdate : Code to be executed at each frame.
    **/

    public abstract void StateUpdate();
}
