using UnityEngine;

public interface ILockOnTarget
{
    const int TARGET_MODE_DETECTED = 0;
    const int TARGET_MODE_AT_RANGE = 1;
    public void ILookAt(Vector3 position);
    public void IMoveTowards(Vector3 position, float speed);
    public void EnableTargeting();
    public void DisableTargeting();
    public void setMode(int mode);
    public Transform Subject();
}