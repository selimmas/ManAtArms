using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardWalking : ShieldGuardStanceState
{
    public ShieldGuardWalking(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
    }

    public override bool CheckTransitions()
    {
        if (playerInput.MoveDirection.magnitude == 0)
        {
            _stateMachine.TransitionToState(new ShieldGuardIdle(_stateMachine, _data));

            return true;
        }

        return false;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateFixedUpdate()
    {
        _data.RigidBody.AddForce((playerInput.MoveDirection * _data.combatWalkingSpeed - _data.RigidBody.velocity) / Time.fixedDeltaTime);
    }
}
