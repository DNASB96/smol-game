﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    private Player player;
    private InputPreferencesScript inputPreferences;
    private const string idleAnimation = "Player_idle";

    public override PlayerState GetNext()
    {
        if (Input.GetKey(inputPreferences.jumpKey) || !player.isGrounded) return player.jumpState;
        if (Input.GetKey(inputPreferences.rightKey) || Input.GetKey(inputPreferences.leftKey)) return player.runState;
        return this;
    }
    
    public override void OnEnterState()
    {
        player.PlayAnim(idleAnimation);
    }

    public override void StateFixedUpdate()
    {
        if(player.isGrounded)
        {
            player.FreezePosition();
        }
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateIdle(Player p)
    {
        player = p;
        inputPreferences = InputPreferencesScript.Instance;
    }
}