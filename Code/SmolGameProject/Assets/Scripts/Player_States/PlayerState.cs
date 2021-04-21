using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    /**
     * Interface for PlayerStates (Player Finite State Machine).
     * 
     * StateUpdate : Code to be executed at each frame.
     * StateFixedUpdate : same as StateUpdate but for rigidbody manipulations.
     * 
     * OnEnterState : Code to be executed only when entering the state.
     * 
     * GetNext : check for transitions to another state (or return itself if the player stays in the same state).
    **/

    public abstract PlayerState GetNext();

    public abstract void OnEnterState();

    public abstract void StateUpdate();

    public abstract void StateFixedUpdate();
}
