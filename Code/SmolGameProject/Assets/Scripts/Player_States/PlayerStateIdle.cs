using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : FSMState
{
    private Player player;
    private InputPreferencesScript inputPreferences;
    private const string idleAnimation = "Player_idle";

    public override FSMState GetNext()
    {
        if (Input.GetKey(inputPreferences.jumpKey)) return player.jumpState;
        if (!player.isGrounded) return player.airborneState;
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
