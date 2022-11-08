using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShieldGuardStanceState : BaseState
{
    protected ILockOn lockOn;

    public ShieldGuardStanceState(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
        if(data.lockOnEnabled)
        {
            float attackRange = 0;

            foreach (IWeapon weapon in _data.weapons)
            {
                if (weapon.AttackRange() > attackRange)
                {
                    attackRange = weapon.AttackRange();
                }
            }

            lockOn = new LockOnController(attackRange);
        }
    }

    public override void OnStateUpdate()
    {
        if(_data.actions.Count == 0 && _stateMachine.actionController.CheckForActions())
        {
            _stateMachine.TransitionToState(new ShieldGuardAction(_stateMachine, _data));

            return;
        }

        if (CheckTransitions())
        {
            return;
        }

        if (_data.lockOnEnabled)
        {
            lockOn.CheckForEnemies(_data);
        }
        
    }

    public override void OnStateExit()
    {
        if (_data.lockOnEnabled)
        {
            lockOn.Disable(_data);
        }
    }

    public override IStance Stance()
    {
        return new ShieldGuardStance();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (_data.lockOnEnabled)
        {
            lockOn.DrawGizmo();
        }
        
    }
}
