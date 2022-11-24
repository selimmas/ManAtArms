using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IWeaponManager
{
    CharacterData _data;

    public WeaponManager(CharacterData data)
    {
        _data = data;
    }

    public void Equip(IWeapon weapon)
    {
        _data.weapons.Add(weapon);
    }

    public List<IWeapon> GetEquiped()
    {
        return _data.weapons;
    }

    public void UnEquip(IWeapon weapon)
    {
        _data.weapons.Remove(weapon);
    }

    public void Initialize()
    {
        _data.weapons = new List<IWeapon>();
    }

    public void EquipAll(Gears gears)
    {
        foreach (GameObject weaponPrefab in gears.weaponPerfabs)
        {
            IWeapon weapon = weaponPrefab.GetComponent<Weapon>();

            foreach (Transform weaponHolderTransform in gears.weaponHolders)
            {
                IWeaponHolder weaponHolder = weaponHolderTransform.GetComponent<WeaponHolder>();

                if (weapon.WeaponType() == weaponHolder.WeaponType())
                {
                    GameObject weaponObject = gears.InstanciateWeapon(weaponPrefab, weaponHolderTransform);

                    HitResponder hitResponder = weaponObject.GetComponent<HitResponder>();

                    hitResponder.owner = gears.gameObject;

                    Equip(weaponObject.GetComponent<Weapon>());
                }
            }
        }
    }

    public void Draw(IWeapon weapon, Gears gears, float delay = 0)
    {
        Transform holder = gears.GetHolder(weapon.WeaponType(), true);
        SetWeaponHolder(weapon, holder);
    }

    public void Sheath(IWeapon weapon, Gears gears, float delay = 0)
    {
        Transform holder = gears.GetHolder(weapon.WeaponType(), false);
        SetWeaponHolder(weapon, holder);
    }

    private void SetWeaponHolder(IWeapon weapon, Transform holder)
    {
        weapon.Subject().SetParent(holder, false);
        weapon.Subject().localPosition = Vector3.zero;
        weapon.Subject().localRotation = Quaternion.identity;
    }
}
