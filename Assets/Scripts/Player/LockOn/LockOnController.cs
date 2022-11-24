using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnController : ILockOn
{
    private float _attackRange;
    private List<ILockOnTarget> enemies;

    Vector3 lockOnTarget;
    Vector3 lockOn;

    float minDistance;

    bool lookAtConfig;

    public LockOnController(float attackRange)
    {
        enemies = new List<ILockOnTarget>();
        _attackRange = attackRange;
        minDistance = attackRange;
    }

    public void CheckForEnemies(CharacterData _data)
    {
        HandleListedEnemies(_data);
        HandleNewEnemies(_data);

        lockOnTarget = Vector3.zero;
        lockOn = Vector3.zero;

        float distances = 0;
        Vector3 positions = Vector3.zero;

        foreach (ILockOnTarget target in enemies)
        {
            float distance = Vector3.Distance(target.Subject().position, _data.Subject.position);
            if (distance < minDistance) minDistance = distance;

            float weight = Mathf.Pow(1 / distance, _attackRange);
            positions += target.Subject().position * weight;
            distances += weight;
        }

        if (positions.magnitude > 0)
        {
            lockOnTarget = positions / distances;
            lockOn = lockOnTarget - _data.Subject.position;
        }

        _data.lookAtEnabled = lockOn == Vector3.zero;

        Vector3 newDirection = Vector3.RotateTowards(_data.Subject.forward, lockOn.normalized, _data.lockOnRotationSpeed * Time.deltaTime, 0);

        _data.Subject.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Disable(CharacterData data)
    {
        data.lookAtEnabled = true;

        foreach(LockOnTarget target in enemies)
        {
            target.DisableTargeting();
        }
    }

    private void HandleNewEnemies(CharacterData _data)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(_data.Subject.position, _data.detectionRange, _data.enemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            LockOnTarget target = enemy.GetComponent<LockOnTarget>();

            if(target != null)
            {
                target.EnableTargeting();
                //target.ILookAt(_data.Subject.position);
                //target.IMoveTowards(_data.Subject.position, 2f);

                float distance = Vector3.Distance(enemy.gameObject.transform.position, _data.Subject.position);

                SetTargetingMode(target, distance);

                enemies.Add(target);
            }
        }
    }

    

    public void HandleListedEnemies(CharacterData _data)
    {
        List<ILockOnTarget> enemiesIndexToDelete = new List<ILockOnTarget>();

        for (int i = 0; i < enemies.Count; i++)
        {
            ILockOnTarget target = enemies[i];

            float distance = Vector3.Distance(target.Subject().position, _data.Subject.position);

            if (distance > _data.detectionRange)
            {
                target.DisableTargeting();

                enemiesIndexToDelete.Add(target);

                continue;
            }



            target.ILookAt(_data.Subject.position);
            SetTargetingMode(target, distance);
        }

        foreach(ILockOnTarget target in enemiesIndexToDelete)
        {
            enemies.Remove(target);
        }
    }

    public void DrawGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lockOnTarget, 0.2f);
    }

    private void SetTargetingMode(ILockOnTarget target, float distance)
    {
        if (distance < _attackRange)
        {
            target.setMode(ILockOnTarget.TARGET_MODE_AT_RANGE);
        }
        else
        {
            target.setMode(ILockOnTarget.TARGET_MODE_DETECTED);
        }
    }
}
