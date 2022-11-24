using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponResponder : HurtResponder
{
    protected HitResponder hitResponder;

    public override void Apply(HitData hitData)
    {
        Debug.Log("hello");

        hitResponder = GetComponent<HitResponder>();

        if (hitResponder._attack && hitData.attacker != null)
        {
            Debug.Log("shock");
            hitData.attacker.GetComponentInChildren<Animator>().SetTrigger("SHOCK");
        }
    }
}
