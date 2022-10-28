public interface IState
{
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
    public abstract bool CheckTransitions();
    public abstract void OnDrawGizmos();
    public void OnStateBaseUpdate();
    public void OnStateBaseFixedUpdate();
    public IStance Stance();
}