public enum ActionSide
{
    LEFT,
    RIGHT,
}

public enum ActionType
{
    SIMPLE,
    HOLD,
}

public interface IAction
{
    public ActionType Type { get; set; }
    public ActionSide Side { get; set; }
    public float Duration(CharacterData data);
    public void Execute(CharacterData data);
}