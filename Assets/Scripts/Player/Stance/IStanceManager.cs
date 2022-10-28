using System.Collections;
using System.Collections.Generic;

public interface IStanceManager
{
    public bool CheckForStanceChange(List<IWeapon> equipedWeapons, IStance currentStance);
    public IStance GetCurrentStance();
}
