using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : FSMState
{
    private Player _player;
    private InputManager _inputManager;

    public override FSMState GetNext()
    {
        if (_inputManager.GetInput(_inputManager.JumpKey)) return _player.JumpState;
        if (!_player.IsGrounded) return _player.AirborneState;
        if (_inputManager.GetInput(_inputManager.RightKey) || _inputManager.GetInput(_inputManager.LeftKey)) return _player.RunState;
        return this;
    }
    
    public override void OnEnterState()
    {
        _player.PlayAnim(Player.idleAnimation);
    }

    public override void StateFixedUpdate()
    {
        if(_player.IsGrounded)
        {
            _player.FreezePosition();
        }
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateIdle(Player p)
    {
        _player = p;
        _inputManager = InputManager.Instance;
    }
}
