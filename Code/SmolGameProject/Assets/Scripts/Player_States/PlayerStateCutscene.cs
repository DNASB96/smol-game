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
        // TODO : What if player is mid jumping? => wait for change of states
    }

    public override void StateFixedUpdate()
    {
        // run movement utilities
        if (!_positionIsReached)
        {
            if (_player.transform.position.x < _positionToReach.transform.position.x)
                _player.Run(1);
            else
                _player.Run(-1);

            // Check if the player is close enough (2 pixels) to the position
            if (Mathf.Abs(_player.transform.position.x - _positionToReach.transform.position.x) < 0.02)
            {
                _positionIsReached = true;
                _player.FreezeHorizontalMovement();
                _player.PlayAnim(Player.idleAnimation);
            }
        }
    }

    public override void StateUpdate()
    {
        // Turn the player towards the npc
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
