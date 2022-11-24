using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionEffect
{
    SLASH,
    STAB,
    BLOCK,
    PUSH,
    ESQUIVE,
}

[CreateAssetMenu(menuName = "Scriptables/Action DATA")]
public class ActionData : ScriptableObject
{
    public string trigger;
    public float duration;
    public ActionEffect effect;
}

