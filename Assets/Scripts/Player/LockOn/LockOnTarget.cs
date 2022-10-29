using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour, ILockOnTarget
{
    [SerializeField] LockOnTargetColorData _colors;
    [SerializeField] GameObject _targetHelper;

    private Transform _subject;

    // Start is called before the first frame update
    void Start()
    {
        _subject = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableTargeting()
    {
        _targetHelper.SetActive(false);
    }

    public void EnableTargeting()
    {
        _targetHelper.SetActive(true);
    }

    public void ILookAt(Vector3 position)
    {
        transform.LookAt(position);
    }

    public void setMode(int mode)
    {
        switch(mode)
        {
            case ILockOnTarget.TARGET_MODE_DETECTED:_targetHelper.GetComponentInChildren<Renderer>().material.color = _colors.detectedColor; break;
            case ILockOnTarget.TARGET_MODE_AT_RANGE: _targetHelper.GetComponentInChildren<Renderer>().material.color = _colors.atRangeColor; break;
        }
    }

    public Transform Subject()
    {
        return _subject;
    }
}
