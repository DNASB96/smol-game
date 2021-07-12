using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKey
{
    /**
     * Description : Stores information for a single input key.
     * Cached attributes specify the last state (wether is was pressed or not).
     * Those are lazily evaluated.
    **/

    private KeyCode _preferredKey;
    private bool _cachedWasDown;

    public KeyCode PrefferedKey { get { return _preferredKey; } }
    public bool CachedWasDown { get { return _cachedWasDown; } }

    public InputKey(KeyCode c)
    {
        _preferredKey = c;
        _cachedWasDown = false;
    }

    public void ChangePreferredKey(KeyCode c)
    {
        // Todo : go through property set instead and make verification that keycode isnt used in another setting
        _preferredKey = c;
    }

    public void CacheInput()
    {
        _cachedWasDown = Input.GetKey(_preferredKey);
    }
}
