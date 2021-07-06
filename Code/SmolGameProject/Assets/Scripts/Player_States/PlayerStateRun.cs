using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : FSMState
{
    private Player player;
    private InputPreferencesScript inputPreferences;
    private const string runAnimation = "Player_run";

    public override FSMState GetNext()
    {
        if (Input.GetKey(inputPreferences.jumpKey)) return player.jumpState;
        if (!player.isGrounded) return player.airborneState;
        if (Input.GetKey(inputPreferences.rightKey) || Input.GetKey(inputPreferences.leftKey)) return this;
        return player.idleState;
    }

    public override void OnEnterState()
    {
        player.PlayAnim(runAnimation);
    }

    public override void StateFixedUpdate()
    {
        if (Input.GetKey(inputPreferences.rightKey)) player.Run(1);
        if (Input.GetKey(inputPreferences.leftKey)) player.Run(-1);
    }

    public override void StateUpdate()
    {
        
    }

    public PlayerStateRun(Player p)
    {
        player = p;
        inputPreferences = InputPreferencesScript.Instance;
    }
}
