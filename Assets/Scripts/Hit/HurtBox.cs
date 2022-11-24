using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour, IHurtBox
{
    [SerializeField] bool _active = true;
    [SerializeField] GameObject _owner = null;

    IHurtResponder _responder;

    public bool Active { get => _active; }

    public GameObject Owner { get => _owner; }

    public Transform Transform { get => transform; }

    public IHurtResponder HurtResponder { get => _responder; set => _responder = value; }

    public bool CheckHit(HitData hitData)
    {
        if(_responder == null)
        {
            Debug.Log("No responder");
        }

        return true;
    }
}
