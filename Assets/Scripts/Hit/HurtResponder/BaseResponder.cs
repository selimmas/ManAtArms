using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResponder : HurtResponder
{
    [SerializeField] int stabMinCount;
    [SerializeField] int slashMinCount;
    [SerializeField] int pushMinCount;

    [SerializeField] float pushMultiplier;

    [SerializeField] float killDelay;

    [SerializeField] GameObject graphics;
    [SerializeField] GameObject doll;

    private bool applied;

    public override void Apply(HitData hitData)
    {
        if (hitData.attacker != gameObject)
        {
            Debug.Log("ENEMY [attacker: " + hitData.attacker + " | hurtBox: " + hitData.hurtBox + " | action: " + hitData.action + " ]");
        }
            

        //if (applied) return;

        //if (hitData.action == null)
        //{
        //    hitCount = 0;
        //    return;
        //}

        //switch(hitData.action.effect)
        //{
        //    case ActionEffect.STAB:
        //        if (hitCount > stabMinCount)
        //        {
        //            Kill();
        //            Debug.Log("weapon stay");
        //        }
                
                
        //        break;
        //    case ActionEffect.SLASH:
        //        if (hitCount > slashMinCount)
        //        {
        //            Kill();
        //            Debug.Log("major blood effect");
        //        }
                
        //        break;
        //    case ActionEffect.PUSH:
        //        if (hitCount > pushMinCount)
        //        {
        //            Push(-hitData.hitNormal * hitCount, hitData.hitPoint);
        //            Debug.Log("recoil");
        //        }
                
        //        break;
        //    default:
        //        Debug.Log("Default");
        //        break;
        //}

        
    }

    private void Kill()
    {
        graphics.SetActive(false);
        doll.SetActive(true);

        StartCoroutine(Disable(killDelay));

        applied = true;
        hitCount = 0;
    }

    IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay); 

        gameObject.SetActive(false);
    }

    private void Push(Vector3 force, Vector3 point)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForceAtPosition(force * pushMultiplier, point, ForceMode.Impulse);

        hitCount = 0;
    }
}
