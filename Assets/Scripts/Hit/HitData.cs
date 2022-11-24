using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    public GameObject attacker;
    public ActionData action;
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public IHurtBox hurtBox;
    public IHitDetector hitDetector;

    public bool Validate()
    {
        if (hurtBox != null)
        {
            if (hurtBox.CheckHit(this))
            {
                if (hurtBox.HurtResponder == null || hurtBox.HurtResponder.CheckHit(this))
                {
                    if(hitDetector.HitResponder == null || hitDetector.HitResponder.CheckHit(this))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

public interface IHurtBox
{
    public bool Active { get; }
    public GameObject Owner { get; }
    public Transform Transform { get; }
    public IHurtResponder HurtResponder { get; }
    public bool CheckHit(HitData hitData);
}

public interface IHitDetector
{
    public IHitResponder HitResponder { get; }
    public void CheckHit();
}

public interface IHurtResponder
{
    public void Response(HitData hitData);
    public bool CheckHit(HitData hitData);
}

public interface IHitResponder
{
    ActionData Action { get; }
    public void Response(HitData hitData);
    public bool CheckHit(HitData hitData);
}



