using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private Vector3 moveDirection;
    private bool stanceToggle;

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }

    public void CheckInputs(CharacterData _data)
    {
        Vector3 playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 combinedInput = playerInput.x * Camera.main.transform.right + playerInput.z * Camera.main.transform.forward;

        MoveDirection = new Vector3((combinedInput).normalized.x, 0, (combinedInput).normalized.z).normalized;

        if(_data.debugMode)
        {
            RayDebug.DrawRay(_data.Subject.position, MoveDirection, Color.green);
        }
    }

    public bool CheckForStanceChange()
    {
        return Input.GetButtonUp("Fire3");
    }
}
