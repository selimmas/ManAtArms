using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : IdleStanceState
{
    PlayerInput playerInput;

    public PlayerWalking(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
        playerInput = new PlayerInput();
    }

    public override bool CheckTransitions()
    {
        playerInput.CheckInputs(_data);

        if (playerInput.MoveDirection.magnitude == 0)
        {
            _stateMachine.TransitionToState(new PlayerIdle(_stateMachine, _data));

            return true;
        }

        return false;
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void OnStateFixedUpdate()
    {
        _data.RigidBody.AddForce((playerInput.MoveDirection * _data.walkingSpeed - _data.RigidBody.velocity)/Time.fixedDeltaTime);
    }

    public override void OnStateUpdate()
    {
        if (CheckTransitions())
        {
            return;
        }
    }
}
