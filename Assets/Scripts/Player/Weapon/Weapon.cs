using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponData _data;

    private Transform subject;

    public WeaponType WeaponType()
    {
        return _data.weaponType;
    }

    // Start is called before the first frame update
    void Awake()
    {
        subject = GetComponent<Transform>();
    }

    public Transform Subject()
    {
        return subject;
    }

    public float AttackRange()
    {
        return _data.attackRange;
    }
}
