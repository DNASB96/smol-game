using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneArea : MonoBehaviour
{
    /**
     * Must be attached to : a cutscene area
     * 
     * Description : This class is used to create a cutscene. 
     * When the player enters the area, the cutscene is triggered, the player goes to a designated point.
     * Dialogs follows and if the area contains an enemy, the player has to kill it.
     **/

    // Position the player must be put at before triggering the dialog.
    [SerializeField] private GameObject _positionToReachPlayer = null;
    [SerializeField] private GameObject _positionToReachCamera = null;

    #region Dialog
    // Reference to the dialogUI which contains the rectangle box and the lines that will be displayed upon interaction.
    private DialogUIScript _dialogUI;

    // Collection of dialog lines that will appear when the panel is interacted with.
    //public string[] sentences;

    // Index to keep track of the current displayed sentence.
    //private int sentences_index = 0;
    #endregion

    private bool _hasBeenTriggered = false;

    // Reference to player to tell it how to behave during the cutscene
    private Player _player;

    // Reference to camera to tell it how to behave during the cutscene
    private CameraController _cameraController;

    // Property to tell the player the cutscene / dialog is finished and the game can resume playing
    private bool _isFinished = false;

    public bool IsFinished { get { return _isFinished; } }

    private void Start()
    {
        _dialogUI = DialogUIScript.Instance;
        _player = Player.Instance;
        _cameraController = CameraController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player entered the cutscene area
        if (!_hasBeenTriggered)
        {
            _hasBeenTriggered = true;
            _player.TriggerCutscene(_positionToReachPlayer.transform);

            // trigger camera change
            //_cameraController.ChangeToCutsceneMode(_positionToReachCamera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Collision exit");
    }
}
