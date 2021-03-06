using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /**
     * Must be attached to : a game object in charge of the inputs.
     * 
     * Description : Singleton instance that manages which input must be used.
     * All game elements that need player input must reference this object to get the right key.
    **/

    // This object must remain unique throughout the run, therefore it is a singleton.
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }
    
    #region Preferences    
    private readonly InputKey _leftKey = new InputKey(KeyCode.LeftArrow);
    private readonly InputKey _rightKey = new InputKey(KeyCode.RightArrow);
    private readonly InputKey _upKey = new InputKey(KeyCode.UpArrow);
    private readonly InputKey _downKey = new InputKey(KeyCode.DownArrow);
    private readonly InputKey _jumpKey = new InputKey(KeyCode.Space);
    private readonly InputKey _menuKey = new InputKey(KeyCode.Escape);
    private readonly InputKey _interactKey = new InputKey(KeyCode.Z);
    private readonly InputKey _attackKey = new InputKey(KeyCode.X);
    private readonly InputKey _attackSpeKey = new InputKey(KeyCode.C);

    public InputKey LeftKey { get { return _leftKey; } }
    public InputKey RightKey { get { return _rightKey; } }
    public InputKey UpKey { get { return _upKey; } }
    public InputKey DownKey { get { return _downKey; } }
    public InputKey JumpKey { get { return _jumpKey; } }
    public InputKey MenuKey { get { return _menuKey; } }
    public InputKey InteractKey { get { return _interactKey; } }
    public InputKey AttackKey { get { return _attackKey; } }
    public InputKey AttackSpeKey { get { return _attackSpeKey; } }

    private List<InputKey> _controls;
    #endregion

    #region Lock input
    private bool _inputIsLocked = false;
    #endregion

    private void Awake()
    {
        // Ensure uniqueness by destroying duplicates.
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        _controls = new List<InputKey>();
        _controls.Add(_leftKey);
        _controls.Add(_rightKey);
        _controls.Add(_upKey);
        _controls.Add(_downKey);
        _controls.Add(_jumpKey);
        _controls.Add(_menuKey);
        _controls.Add(_interactKey);
        _controls.Add(_attackKey);
        _controls.Add(_attackSpeKey);
    }

    public bool GetInput(InputKey k)
    {
        if (_inputIsLocked)
        {
            return k.CachedWasDown;
        }
        else
        {
            return Input.GetKey(k.PrefferedKey);
        }
    }

    public bool GetInputDown(InputKey k)
    {
        if (_inputIsLocked)
        {
            return false;
        }
        else
        {
            return Input.GetKeyDown(k.PrefferedKey);
        }
    }

    public void ChangeInputKey(InputKey i, KeyCode k)
    {
        // Todo : change the key
    }

    public void LockInput()
    {
        _inputIsLocked = true;
        // Cache the input for each key ; used for completing jump
        foreach (InputKey k in _controls)
        {
            k.CacheInput();
        }
}

    public void UnlockInput()
    {
        _inputIsLocked = false;
    }
}
