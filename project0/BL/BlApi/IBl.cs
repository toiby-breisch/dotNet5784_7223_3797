namespace BlApi;
public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ITaskInList TaskInList { get; }
    public IEngineerInList EngineerInList { get; }
    //public IMilestone Milestone { get; }
}
