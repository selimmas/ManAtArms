using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResponder : HurtResponder
{
    public override void Apply(HitData hitData)
    {
        if(hitData.attacker != gameObject)
        {
            Debug.Log("PLAYER [attacker: " + hitData.attacker + " | hurtBox: " + hitData.hurtBox + " | action: " + hitData.action + " ]");
        }

        //Debug.Log("kill");
        //Destroy(gameObject);
    }
}
