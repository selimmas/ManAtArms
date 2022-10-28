using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : ISpring
{
    public void CheckForGround(CharacterData _data)
    {
        float averageDistance;
        float sum = 0;

        foreach (Transform raycastSource in _data.GroundRaycastSources)
        {
            RaycastHit hit;

            if (Physics.Raycast(raycastSource.position, -Vector3.up, out hit, Mathf.Infinity, _data.groundMask))
            {
                sum += hit.distance;

                if(_data.debugMode)
                {
                    Debug.DrawRay(raycastSource.position, -Vector3.up * hit.distance, Color.red);
                }
            }
            else
            {
                if (_data.debugMode)
                {
                    Debug.DrawRay(raycastSource.position, -Vector3.up * _data.rideHeight, Color.black);
                }
            }
        }

        averageDistance = sum / _data.GroundRaycastSources.Count;

        _data.RigidBody.AddForce(Vector3.up * (_data.rideHeight - averageDistance) * _data.springForce - _data.RigidBody.velocity * _data.dampForce, ForceMode.Acceleration);
    }
}
