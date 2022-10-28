using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : ILookAt
{
    Vector3 target;
    Vector3 lookAt;
    
    public void CheckForTarget(CharacterData _data)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _data.groundMask))
        {
            target = hit.point;
            target.y = _data.Subject.position.y;

            lookAt = (target - _data.Subject.position).normalized;

            if(_data.debugMode)
            {
                RayDebug.DrawRay(_data.Subject.position, lookAt, Color.blue);
            }

            Vector3 newDirection = Vector3.RotateTowards(_data.Subject.forward, lookAt.normalized, _data.rotationSpeed * Time.deltaTime, 0);

            _data.Subject.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void DrawGizmo()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(target, 0.2f);
    }
}
