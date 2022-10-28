using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponManager
{
    public List<IWeapon> GetEquiped();

    public void EquipAll(Gears gears);

    public void Equip(IWeapon weapon);

    public void UnEquip(IWeapon weapon);

    public void Draw(IWeapon weapon, Gears gears, float delay = 0);

    public void Sheath(IWeapon weapon, Gears gears, float delay = 0);
}

public enum WeaponType
{
    SWORD,
    SHIELD,
    SPEAR,
    BOW
}
