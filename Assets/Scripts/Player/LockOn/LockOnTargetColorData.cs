using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Lock On Target Colors")]
public class LockOnTargetColorData : ScriptableObject
{
    public Color detectedColor;
    public Color atRangeColor;
}

