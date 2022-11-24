using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiedResponder : HurtResponder
{
    protected HitResponder hitResponder;

    public override void Apply(HitData hitData)
    {
        hitResponder = GetComponent<HitResponder>();

        if(hitResponder._attack && hitData.attacker != null)
        {
            hitData.attacker.GetComponentInChildren<Animator>().SetTrigger("BLOCKED");
        }
    }
}
