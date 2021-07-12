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
    [SerializeField] GameObject _objectToFollow = null;

    #region Singleton Instance
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }
    #endregion

    #region Camera Modes
    private CameraState _currentMode;
    private CameraStateFollowPlayer _followPlayerMode;
    private CameraStateCutscene _cutsceneMode;

    public CameraState CurrentMode { get { return _currentMode; } }
    public CameraStateFollowPlayer FollowPlayerMode { get { return _followPlayerMode; } }
    public CameraStateCutscene CutsceneMode { get { return _cutsceneMode; } }
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

        _followPlayerMode = new CameraStateFollowPlayer(this, _objectToFollow);
        _cutsceneMode = new CameraStateCutscene(this);
        _currentMode = _followPlayerMode;
    }

    private void Update()
    {
        _currentMode.StateUpdate();
    }

    public void ChangeToCutsceneMode(Transform cameraPostion)
    {
        _cutsceneMode.SetPositionToReach(cameraPostion);
        _currentMode = _cutsceneMode;
    }
}
