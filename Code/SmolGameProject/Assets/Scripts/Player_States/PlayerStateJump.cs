﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : FSMState
{
    private Player player;
    private InputPreferencesScript inputPreferences;

    public override FSMState GetNext()
    {
        if (player.isFalling) return player.airborneState;
        return this;
    }

    public override void OnEnterState()
    {
        if (player.isGrounded) player.PlayAnim(Player.jumpAscentAnimation);
    }

    public override void StateFixedUpdate()
    {
        // If player is not grounded => double jump?
        if (player.isGrounded) player.Jump();

        // Airborne control
        if (Input.GetKey(inputPreferences.rightKey)) player.Run(1);
        else if (Input.GetKey(inputPreferences.leftKey)) player.Run(-1);
        else player.FreezeHorizontalMovement();
    }

    public override void StateUpdate()
    {

    }

    public PlayerStateJump(Player p)
    {
        player = p;
        inputPreferences = InputPreferencesScript.Instance;
    }
}
