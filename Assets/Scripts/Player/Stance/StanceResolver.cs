using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceResolver : IStanceResolver
{
    public IStance FindStance(List<IWeapon> weapons)
    {
        bool hasSword = false;
        bool hasShield = false;

        foreach (IWeapon weapon in weapons)
        {
            if (weapon.WeaponType() == WeaponType.SWORD)
            {
                hasSword = true;
            }

            if (weapon.WeaponType() == WeaponType.SHIELD)
            {
                hasShield = true;
            }
        }

        if(hasSword && hasShield)
        {
            return new ShieldGuardStance();
        }

        return new IdleStance();
    }
}
