using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Weapon DATA")]
public class WeaponData : ScriptableObject
{
    [Header("GENERAL")]
    public WeaponType weaponType;
    public ActionSide actionSide;
    public float attackRange;

    [Header("DELAY")]
    public float drawDelay;
    public float sheathDelay;

    [Header("ACTIONS")]
    public List<ActionData> simpleActions;
    public List<ActionData> holdActions;
}

