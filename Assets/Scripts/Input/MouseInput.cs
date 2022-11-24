using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput
{
    const float HOLD_DURATION = .2f;

    bool leftButtonPressed;
    bool leftButtonHold;
    float leftActivationTime;

    bool rightButtonPressed;
    bool rightButtonHold;
    float rightActivationTime;

    public bool CheckforLeftClick()
    {
        return leftButtonPressed;
    }

    public bool CheckforLeftHold()
    {
        return leftButtonHold;
    }

    public bool CheckforRightClick()
    {
        return rightButtonPressed;
    }

    public bool CheckforRightHold()
    {
        return rightButtonHold;
    }

    public bool actionStarted()
    {
        return leftActivationTime != 0 && !leftButtonPressed || rightActivationTime != 0 && !rightButtonPressed;
    }

    public void CheckForInputs()
    {
        leftButtonPressed = false;
        leftButtonHold = false;
        rightButtonPressed = false;
        rightButtonHold = false;

        if (Input.GetMouseButtonDown(0))
        {
            leftActivationTime = Time.time;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightActivationTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (leftActivationTime != 0 && Time.time - leftActivationTime > HOLD_DURATION)
            {
                leftButtonHold = true;
                
            }
            else
            {
                leftButtonPressed = true;
            }

            leftActivationTime = 0;
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (rightActivationTime != 0 && Time.time - rightActivationTime > HOLD_DURATION)
            {
                rightButtonHold = true;
            }
            else
            {
                rightButtonPressed = true;
            }

            rightActivationTime = 0;
        }
    }

    
}
