using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAirborne : FSMState
{
    private Player player;
    private InputPreferencesScript inputPreferences;

    public override FSMState GetNext()
    {
        if (!player.isGrounded) return this;
        if (Input.GetKey(inputPreferences.rightKey) || Input.GetKey(inputPreferences.leftKey)) return player.runState;
        return player.idleState;
    }

    public override void OnEnterState()
    {
        player.PlayAnim(Player.airborneAnimation);
    }

    public override void StateFixedUpdate()
    {
        // Airborne control
        if (Input.GetKey(inputPreferences.rightKey)) player.Run(1);
        else if (Input.GetKey(inputPreferences.leftKey)) player.Run(-1);
        else player.FreezeHorizontalMovement();
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateAirborne(Player p)
    {
        player = p;
        inputPreferences = InputPreferencesScript.Instance;
    }
}
