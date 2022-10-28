using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShieldGuardStanceState : BaseState
{
    public ShieldGuardStanceState(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
    }

    public override IStance Stance()
    {
        return new ShieldGuardStance();
    }
}
