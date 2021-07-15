using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCutscene : FSMState
{
    private Player _player;
    private Transform _positionToReach = null;
    private bool _positionIsReached = false;

    public bool PositionIsReached { get { return _positionIsReached; } }

    public override FSMState GetNext()
    {
        if (!_player.TriggeredCutscene) return _player.IdleState;
        return this;
    }

    public override void OnEnterState()
    {
        _player.PlayAnim(Player.runAnimation);
    }

    public override void StateFixedUpdate()
    {
        // Make the player run to his cutscene position
        if (!_positionIsReached)
        {
            if (_player.transform.position.x < _positionToReach.position.x)
                _player.Run(true);
            else
                _player.Run(false);

            // Check if the player is close enough (2 pixels) to the position
            if (Mathf.Abs(_player.transform.position.x - _positionToReach.position.x) < 0.02)
            {
                // Turn the player towards the npc and make him idle
                _positionIsReached = true;
                _player.PrepareForDialog();
            }
        }
    }

    public override void StateUpdate()
    {
        
    }

    public void SetPositionToReach(Transform t)
    {
        _positionToReach = t;
    }

    public PlayerStateCutscene(Player p)
    {
        _player = p;
    }
}
