using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HurtResponder : MonoBehaviour, IHurtResponder
{
    protected int hitCount;
   
    List<HurtBox> _hurtBoxes = new List<HurtBox>();

    void Start()
    {
        _hurtBoxes = new List<HurtBox>(GetComponentsInChildren<HurtBox>());

        foreach (HurtBox hurtBox in _hurtBoxes)
        {
            hurtBox.HurtResponder = this;
        }

        Debug.Log(gameObject + " : " + _hurtBoxes.Count);
    }

    public bool CheckHit(HitData hitData)
    {
        return true;
    }

    public void Response(HitData hitData)
    {
        hitCount++;

        Apply(hitData);
    }

    public abstract void Apply(HitData hitData);
}
