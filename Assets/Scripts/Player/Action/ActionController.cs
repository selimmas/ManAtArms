using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : IActionController
{
    IAction action;

    MouseInput mouse;

    public ActionController()
    {
        mouse = new MouseInput();
        action = new Action();
    }

    public IAction Action { get => action; set => action = value; }

    public bool CheckForActions()
    {
        mouse.CheckForInputs();

        if (mouse.CheckforLeftClick())
        {
            CreateAction(ActionType.SIMPLE, ActionSide.LEFT);

            return true;
        }

        if (mouse.CheckforRightClick())
        {
            CreateAction(ActionType.SIMPLE, ActionSide.RIGHT);

            return true;
        }
        if (mouse.CheckforLeftHold())
        {
            CreateAction(ActionType.HOLD, ActionSide.LEFT);

            return true;
        }

        if (mouse.CheckforRightHold())
        {
            CreateAction(ActionType.HOLD, ActionSide.RIGHT);

            return true;
        }

        if(mouse.actionStarted())
        {
            action = null;
            return true;
        }

        return false;
    }

    private void CreateAction(ActionType type, ActionSide side)
    {
        action = new Action();

        action.Type = type;
        action.Side = side;
    }
}
