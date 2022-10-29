using UnityEngine;

public interface IWeapon
{
    public Transform Subject();
    public WeaponType WeaponType();
    public float AttackRange();
}
