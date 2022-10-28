using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceManager : IStanceManager
{
    private PlayerInput playerInput;

    private IStance currentStance;
    private IStanceResolver stanceResolver;

    public StanceManager()
    {
        playerInput = new PlayerInput();
        stanceResolver = new StanceResolver();
    }

    public IStance CurrentStance { get => currentStance; set => currentStance = value; }

    public bool CheckForStanceChange(List<IWeapon> equipedWeapons, IStance currentStance)
    {
        if (playerInput.CheckForStanceChange())
        {
            if(currentStance.isFightingStance())
            {
                CurrentStance = new IdleStance();
            }
            else
            {
                CurrentStance = stanceResolver.FindStance(equipedWeapons);
            }
            
            return true;
        }

        return false;
    }

    public IStance GetCurrentStance()
    {
        return CurrentStance;
    }
}
