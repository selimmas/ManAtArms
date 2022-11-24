using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitResponder : MonoBehaviour, IHitResponder
{
    [SerializeField] public GameObject owner;
    [SerializeField] HitDetector _detector;
    [SerializeField] public bool _attack;

    private ActionData _action;
    private IWeapon _weapon;

    public ActionData Action { get => _action; }

    void Start()
    {
        _weapon = GetComponent<IWeapon>();
        _detector.HitResponder = this;
    }

    void Update()
    {
        _detector.CheckHit();
    }

    public bool CheckHit(HitData hitData)
    {
        return _attack;
    }

    public void Response(HitData hitData)
    {
        if(_attack)
        {
            hitData.attacker = owner;
            hitData.action = _weapon.CurrentAction;
        }
        else
        {
            hitData.action = null;
        }
    }
}
