using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    [SerializeField] public List<GameObject> weaponPerfabs;
    [SerializeField] public List<Transform> weaponHolders;
    [SerializeField] public List<Transform> activeWeaponHolders;

    public GameObject InstanciateWeapon(GameObject weaponPrefab, Transform weaponHolder)
    {
        return Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
    }

    public Transform GetHolder(WeaponType weaponType, bool isActive)
    {
        List<Transform> holders = isActive ? activeWeaponHolders : weaponHolders;

        foreach(Transform holder in holders)
        {
            WeaponHolder weaponHolder = holder.GetComponent<WeaponHolder>();
            
            if(weaponHolder.WeaponType() == weaponType)
            {
                return holder;
            }
        }

        return transform;
    }
}
