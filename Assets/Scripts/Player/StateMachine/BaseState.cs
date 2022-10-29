using UnityEngine;

public abstract class BaseState : IState
{
    protected StateMachine _stateMachine;
    protected CharacterData _data;

    protected PlayerInput playerInput;
    protected ISpring spring;
    protected ILookAt lookAt;

    protected IStanceManager stanceManager;

    public BaseState(StateMachine stateMachine, CharacterData data)
    {
        _stateMachine = stateMachine;
        _data = data;

        playerInput = new PlayerInput();

        spring = new Spring();
        lookAt = new LookAt();

        stanceManager = new StanceManager();
    }

    public void OnStateBaseUpdate()
    {
        playerInput.CheckInputs(_data);

        if (stanceManager.CheckForStanceChange(_data.weapons, _data.CurrentStance))
        {
            IState newState = StatePicker.GetState(stanceManager.GetCurrentStance().InitialState(), _stateMachine, _data);

            _stateMachine.TransitionToState(newState);
        }
}

    public void OnStateBaseFixedUpdate()
    {
        if (_data.springEnabled)
            spring.CheckForGround(_data);

        if(_data.lookAtEnabled)
            lookAt.CheckForTarget(_data);
    }


    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
    public abstract void OnStateFixedUpdate();
    public abstract bool CheckTransitions();
    public abstract IStance Stance();

    public virtual void OnDrawGizmos()
    {
        lookAt.DrawGizmo();
    }
}
