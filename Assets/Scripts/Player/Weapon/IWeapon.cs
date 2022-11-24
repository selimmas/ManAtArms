using UnityEngine;

public interface IWeapon
{
    public Transform Subject();
    public WeaponType WeaponType();
    public float AttackRange();
    public ActionSide Side { get; set; }
    public string Trigger(ActionType type);
    public float ActionDuration(ActionType type);
    public ActionData CurrentAction { get; set; }
}
