using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdleStanceState : BaseState
{
    protected IdleStanceState(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
    }

    public override IStance Stance()
    {
        return new IdleStance();
    }
}
