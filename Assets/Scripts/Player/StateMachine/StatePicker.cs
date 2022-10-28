public enum State
{
    Idle,
    ShieldGuardIdle,
}

public static class StatePicker
{
    public static IState GetState(State state, StateMachine stateMachine, CharacterData data)
    {
        switch (state)
        {
            case State.Idle:
                return new PlayerIdle(stateMachine, data);
            case State.ShieldGuardIdle:
                return new ShieldGuardIdle(stateMachine, data);
            default:
                return null;
        }
    }
}
