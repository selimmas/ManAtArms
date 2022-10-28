using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardStance : IStance
{
    public State InitialState()
    {
        return State.ShieldGuardIdle;
    }

    public bool isFightingStance()
    {
        return true;
    }
}
