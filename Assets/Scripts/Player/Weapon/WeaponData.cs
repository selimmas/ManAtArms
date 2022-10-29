using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Weapon DATA")]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;

    public float drawDelay;
    public float sheathDelay;

    public float attackRange;
}
