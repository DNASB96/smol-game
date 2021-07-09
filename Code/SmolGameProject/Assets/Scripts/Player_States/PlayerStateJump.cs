using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : FSMState
{
    private Player _player;
    private InputManager _inputManager;

    public override FSMState GetNext()
    {
        if (_player.IsFalling) return _player.AirborneState;
        return this;
    }

    public override void OnEnterState()
    {
        if (_player.IsGrounded) _player.PlayAnim(Player.jumpAscentAnimation);
    }

    public override void StateFixedUpdate()
    {
        // If player is not grounded => double jump?
        if (_player.IsGrounded) _player.Jump();

        // Airborne control
        if (_inputManager.GetInput(_inputManager.RightKey)) _player.Run(1);
        else if (_inputManager.GetInput(_inputManager.LeftKey)) _player.Run(-1);
        else _player.FreezeHorizontalMovement();
    }

    public override void StateUpdate()
    {

    }

    public PlayerStateJump(Player p)
    {
        _player = p;
        _inputManager = InputManager.Instance;
    }
}
