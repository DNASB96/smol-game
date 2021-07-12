using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAirborne : FSMState
{
    private Player _player;
    private InputManager _inputManager;

    public override FSMState GetNext()
    {
        if (_player.TriggeredCutscene && _player.IsGrounded) return _player.CutsceneState;
        if (!_player.IsGrounded) return this;
        if (_inputManager.GetInput(_inputManager.RightKey) || _inputManager.GetInput(_inputManager.LeftKey)) return _player.RunState;
        return _player.IdleState;
    }

    public override void OnEnterState()
    {
        _player.PlayAnim(Player.airborneAnimation);
    }

    public override void StateFixedUpdate()
    {
        if (_inputManager.GetInput(_inputManager.RightKey)) _player.Run(true);
        else if (_inputManager.GetInput(_inputManager.LeftKey)) _player.Run(false);
        else _player.FreezeHorizontalMovement();
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateAirborne(Player p)
    {
        _player = p;
        _inputManager = InputManager.Instance;
    }
}
