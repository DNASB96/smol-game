using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateCutscene : FSMState
{
    private Player player;
    private GameObject positionToReach = null;
    private bool positionIsReached = false;

    private const string idleAnimation = "Player_idle";
    private const string runAnimation = "Player_run";

    public override FSMState GetNext()
    {
        return this;
        // Todo : return to other state when cutscene ends
    }

    public override void OnEnterState()
    {
        player.PlayAnim(runAnimation);
    }

    public override void StateFixedUpdate()
    {
        // run movement utilities
        if (!positionIsReached)
        {
            if (player.transform.position.x < positionToReach.transform.position.x)
                player.Run(1);
            else
                player.Run(-1);

            // Check if the player is close enough (2 pixels) to the position
            if (Mathf.Abs(player.transform.position.x - positionToReach.transform.position.x) < 0.02)
            {
                positionIsReached = true;
                player.FreezeHorizontalMovement();
                player.PlayAnim(idleAnimation);
            }
        }
    }

    public override void StateUpdate()
    {

    }

    public void SetPositionToReach(GameObject o)
    {
        positionToReach = o;
    }

    public PlayerStateCutscene(Player p)
    {
        player = p;
    }
}