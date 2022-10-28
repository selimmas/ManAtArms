using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardIdle : ShieldGuardStanceState
{
    private PlayerInput playerInput;
    private WeaponManager _weaponManager;

    public ShieldGuardIdle(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
        playerInput = new PlayerInput();
        _weaponManager = new WeaponManager(_data);
    }

    public override bool CheckTransitions()
    {
        playerInput.CheckInputs(_data);

        if (playerInput.MoveDirection.magnitude != 0)
        {
            _stateMachine.TransitionToState(new ShieldGuardWalking(_stateMachine, _data));

            return true;
        }

        return false;
    }

    public override void OnStateEnter()
    {
        foreach(IWeapon weapon in _data.weapons)
        {
            _weaponManager.Draw(weapon, _data.Gears);
        }
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateUpdate()
    {
        if (CheckTransitions())
        {
            return;
        }
    }
}
