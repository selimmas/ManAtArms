using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : IAction
{
    private ActionType type;
    private ActionSide side;

    public ActionType Type { get => type; set => type = value; }
    public ActionSide Side { get => side; set => side = value; }

    public float Duration(CharacterData data)
    {
        foreach (IWeapon weapon in data.weapons)
        {
            if (weapon.Side == side)
            {
                return weapon.ActionDuration(type);
            }
        }

        return 0;
    }

    public void Execute(CharacterData data)
    {
        foreach(IWeapon weapon in data.weapons)
        {
            if(weapon.Side == side)
            {
                data.Animator.SetTrigger(weapon.Trigger(type));
            }
        }
    }

    public override string ToString()
    {
        return side.ToString().Substring(0,1) + type.ToString().Substring(0,1);
    }
}
