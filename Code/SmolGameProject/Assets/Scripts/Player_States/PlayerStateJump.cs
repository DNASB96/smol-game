using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : PlayerState
{
    private Player player;
    private InputPreferencesScript inputPreferences;
    private const string jumpAscentAnimation = "Player_jump_ascent";
    private const string jumpDescentAnimation = "Player_jump_descent";

    public override PlayerState GetNext()
    {
        if (Input.GetKey(inputPreferences.jumpKey) || !player.isGrounded) return this;
        if (Input.GetKey(inputPreferences.rightKey) || Input.GetKey(inputPreferences.leftKey)) return player.runState;
        return player.idleState;
    }

    public override void OnEnterState()
    {
        if (player.isGrounded) player.PlayAnim(jumpAscentAnimation);
    }

    public override void StateFixedUpdate()
    {
        if (player.isGrounded) player.Jump();

        // Airborne control
        if (Input.GetKey(inputPreferences.rightKey)) player.Run(1);
        else if (Input.GetKey(inputPreferences.leftKey)) player.Run(-1);
        else player.FreezeHorizontalMovement();
    }

    public override void StateUpdate()
    {
        if (player.isFalling) player.PlayAnim(jumpDescentAnimation);
    }

    public PlayerStateJump(Player p)
    {
        player = p;
        inputPreferences = InputPreferencesScript.Instance;
    }
}
