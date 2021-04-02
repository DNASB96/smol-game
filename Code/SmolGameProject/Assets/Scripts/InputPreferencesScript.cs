using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPreferencesScript : MonoBehaviour
{
    /**
     * Must be attached to : a game object in charge of the inputs.
     * 
     * Description : Singleton instance that manages which input must be used.
     * All game elements that need player input must reference this object to get the right key.
    **/

    // This object must remain unique throughout the run, therefore it is a singleton.
    private static InputPreferencesScript _instance;
    public static InputPreferencesScript Instance { get { return _instance; } }


    // Controls are Read-Only.
    public KeyCode leftKey { get; } = KeyCode.LeftArrow;
    public KeyCode rightKey { get; } = KeyCode.RightArrow;
    public KeyCode upKey { get; } = KeyCode.UpArrow;
    public KeyCode downKey { get; } = KeyCode.DownArrow;
    public KeyCode jumpKey { get; } = KeyCode.Space;
    public KeyCode menuKey { get; } = KeyCode.Escape;
    public KeyCode interactKey { get; } = KeyCode.Z;
    public KeyCode attackKey { get; } = KeyCode.X;
    public KeyCode attackSpeKey { get; } = KeyCode.C;

    private void Awake()
    {
        // Ensure uniqueness by destroying duplicates.
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
}
