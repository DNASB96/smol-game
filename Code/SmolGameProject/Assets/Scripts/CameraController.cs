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

    #region Singleton Instance
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }
    #endregion

    #region Camera Modes
    public CameraState currentMode { get; private set; }
    public CameraStateFollowPlayer followPlayerMode { get; private set; }
    #endregion

    private void Awake()
    {
        // Singleton verification
        if (_instance != null && _instance != this)
        {
            Debug.Log("Camera duplicate destroyed");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        followPlayerMode = new CameraStateFollowPlayer(this, objectToFollow);
        currentMode = followPlayerMode;
    }

    private void Update()
    {
        currentMode.StateUpdate();
    }
}
