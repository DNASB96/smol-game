using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : FSMState
{
    private readonly Player _player;
    private InputManager _inputManager;

    public override FSMState GetNext()
    {
        if (_player.TriggeredCutscene && _player.IsGrounded) return _player.CutsceneState;
        if (_inputManager.GetInput(_inputManager.JumpKey)) return _player.JumpState;
        if (!_player.IsGrounded) return _player.AirborneState;
        if (_inputManager.GetInput(_inputManager.RightKey) || _inputManager.GetInput(_inputManager.LeftKey)) return this;
        return _player.IdleState;
    }

    public override void OnEnterState()
    {
        _player.PlayAnim(Player.runAnimation);
    }

    public override void StateFixedUpdate()
    {
        if (_inputManager.GetInput(_inputManager.RightKey)) _player.Run(true);
        if (_inputManager.GetInput(_inputManager.LeftKey)) _player.Run(false);
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateRun(Player p)
    {
        _player = p;
        _inputManager = InputManager.Instance;
    }
}
