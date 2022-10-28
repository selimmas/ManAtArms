
// Implémentation concrète d'un autre état.
// Voir commentaires sur PlayerIdle.
public class PlayerFall : BaseState
{
    public PlayerFall(StateMachine stateMachine, CharacterData data) : base(stateMachine, data){}

    public override bool CheckTransitions()
    {
        return false;
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (CheckTransitions()) return;
    }

    public override IStance Stance()
    {
        return new IdleStance();
    }
}
