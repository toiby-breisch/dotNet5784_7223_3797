namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();
    public ITaskInList TaskInList => new TaskInListImplementation();
    public IEngineerInList EngineerInList => new EngineerInListImplementation();

    public ITask Task => new TaskIplementation();
    //public IMilestone Milestone => new MilestoneImplementation();
    //public IMilestone EngineerInListImplementation => new MilestoneImplementation();
    //public IMilestone TaskInListImplementation => new MilestoneImplementation();
}
