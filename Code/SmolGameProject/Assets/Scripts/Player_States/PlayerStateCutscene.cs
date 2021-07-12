using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCutscene : FSMState
{
    private Player _player;
    private GameObject _positionToReach = null;
    private bool _positionIsReached = false;

    public override FSMState GetNext()
    {
        return this;
        // Todo : return to other state when cutscene ends
    }

    public override void OnEnterState()
    {
        _player.PlayAnim(Player.runAnimation);
        Debug.Log("Woaw");
        // TODO : What if player is mid jumping? => wait for change of states
    }

    public override void StateFixedUpdate()
    {
        // Make the player run to his cutscene position
        if (!_positionIsReached)
        {
            if (_player.transform.position.x < _positionToReach.transform.position.x)
                _player.Run(true);
            else
                _player.Run(false);

            // Check if the player is close enough (2 pixels) to the position
            if (Mathf.Abs(_player.transform.position.x - _positionToReach.transform.position.x) < 0.02)
            {
                // Turn the player towards the npc and make him idle
                _positionIsReached = true;
                _player.FreezeHorizontalMovement();
                _player.PlayAnim(Player.idleAnimation);
                _player.Turn(true);
            }
        }
    }

    public override void StateUpdate()
    {
        
    }

    public void SetPositionToReach(GameObject o)
    {
        _positionToReach = o;
    }

    public PlayerStateCutscene(Player p)
    {
        _player = p;
    }
}
