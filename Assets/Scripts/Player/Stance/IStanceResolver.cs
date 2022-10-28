using System.Collections;
using System.Collections.Generic;

public interface IStanceResolver 
{
    public IStance FindStance(List<IWeapon> weapons);

}
