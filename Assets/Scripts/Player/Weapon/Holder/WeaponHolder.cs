using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour, IWeaponHolder
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private bool isActive;

    public WeaponType WeaponType()
    {
        return weaponType;
    }

    bool IWeaponHolder.IsActive()
    {
        return isActive;
    }
}
