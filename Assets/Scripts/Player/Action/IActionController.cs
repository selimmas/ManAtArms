public interface IActionController
{
    public bool CheckForActions();
    public IAction Action { get; set; }
}