using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamiaStateIdle : FSMState
{
    private EnnemyLamia lamia;

    public override FSMState GetNext()
    {
        return this;
        // Todo : transition to other states
    }

    public override void OnEnterState()
    {
        // Todo : play idle animation
    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateUpdate()
    {

    }

    public LamiaStateIdle(EnnemyLamia l)
    {
        lamia = l;
    }
}
