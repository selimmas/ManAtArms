using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStance : IStance
{
    public State InitialState()
    {
        return State.Idle;
    }

    public bool isFightingStance()
    {
        return false;
    }
}
