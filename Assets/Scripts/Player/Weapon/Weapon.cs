using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponData _data;

    private Transform subject;
    private int simpleActionIndex;
    private int holdActionIndex;
    private ActionData currentAction;

    public ActionSide Side { get => _data.actionSide; set => _data.actionSide = value; }
    public ActionData CurrentAction { get => currentAction; set => currentAction = value; }

    HitResponder hitResponder;

    public WeaponType WeaponType()
    {
        return _data.weaponType;
    }

    // Start is called before the first frame update
    void Awake()
    {
        subject = GetComponent<Transform>();
        hitResponder = GetComponent<HitResponder>();
    }

    public Transform Subject()
    {
        return subject;
    }

    public float AttackRange()
    {
        return _data.attackRange;
    }

    public string Trigger(ActionType type)
    {
        switch (type)
        {
            case ActionType.SIMPLE:
                CurrentAction = GetSimpleAction();
                return CurrentAction.trigger;
            case ActionType.HOLD:
                CurrentAction = GetHoldAction();
                return CurrentAction.trigger ;
            default:
                return null;
        }
    }

    public float ActionDuration(ActionType type)
    {
        switch (type)
        {
            case ActionType.SIMPLE:
                return GetSimpleAction(false).duration;
            case ActionType.HOLD:
                return GetHoldAction(false).duration;
            default:
                return 0;
        }
    }

    private ActionData GetSimpleAction(bool increment = true)
    {
        ActionData simpleAction = null;

        if (_data.simpleActions.Count != 0)
        {
            if (simpleActionIndex >= _data.simpleActions.Count)
            {
                simpleActionIndex = 0;
            }

            simpleAction = _data.simpleActions[simpleActionIndex];

            if(increment)
                simpleActionIndex++;
        }

        return simpleAction;
    }

    private ActionData GetHoldAction(bool increment = true)
    {
        ActionData holdAction = null;

        if (_data.holdActions.Count != 0)
        {
            if (holdActionIndex >= _data.holdActions.Count)
            {
                holdActionIndex = 0;
            }

            holdAction = _data.holdActions[holdActionIndex];

            if(increment)
                holdActionIndex++;
        }

        return holdAction;
    }
}
