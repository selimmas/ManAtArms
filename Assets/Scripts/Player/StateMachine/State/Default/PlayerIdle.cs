using UnityEngine;

public class PlayerIdle : IdleStanceState
{
    private WeaponManager _weaponManager;

    public PlayerIdle(StateMachine stateMachine, CharacterData data) : base(stateMachine, data) 
    {
        _weaponManager = new WeaponManager(data);
    }

    public override bool CheckTransitions()
    {
        if(playerInput.MoveDirection.magnitude != 0)
        {
            _stateMachine.TransitionToState(new PlayerWalking(_stateMachine, _data));

            return true;
        }

        return false;
    }

    public override void OnStateEnter()
    {
        foreach (IWeapon weapon in _data.weapons)
        {
            _weaponManager.Sheath(weapon, _data.Gears);
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
        if(CheckTransitions())
        {
            return;
        }
    }
}
