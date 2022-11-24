using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardAction : ShieldGuardWalking
{
    private IAction _action;
    private float actionEndTime;
    private float actionActivationTime;

    public ShieldGuardAction(StateMachine stateMachine, CharacterData data) : base(stateMachine, data)
    {
    }

    public override bool CheckTransitions()
    {
        if (_data.actions.Count == 0 && Time.time > actionEndTime && _stateMachine.actionController.CheckForActions() == false)
        {
            _stateMachine.TransitionToState(new ShieldGuardIdle(_stateMachine, _data));

            return true;
        }

        return false;
    }

    public override void OnStateUpdate()
    {
        if (_stateMachine.actionController.CheckForActions() && _stateMachine.actionController.Action != null)
        {
            _data.actions.Add(_stateMachine.actionController.Action);
        }

        if (CheckTransitions())
        {
            return;
        }

        if (_data.lockOnEnabled)
        {
            lockOn.CheckForEnemies(_data);
        }

        if (_action == null && _data.actions.Count > 0)
        {
            _action = _data.actions[0];

            _action.Execute(_data);
            _action.Enable(_data);

            actionEndTime = Time.time + _action.Duration(_data);
            actionActivationTime = Time.time + (_action.Duration(_data) * .3f);
        }

        //if (_action != null && Time.time > actionActivationTime)
        //{
        //    _action.Enable(_data);
        //}

        if (_action != null && Time.time > actionEndTime)
        {
            _action.Disable(_data);
                
            _action = null;
            _data.actions.RemoveAt(0);
        }

        if (_data.debugMode && _data.debugText != null)
        {
            _data.debugText.text = "";

            foreach (IAction action in _data.actions)
            {
                if (action == _action)
                {
                    _data.debugText.text += ">";
                }

                _data.debugText.text += action.ToString() + " - ";
            }
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _data.actions.Clear();
    }
}
