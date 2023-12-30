namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskIplementation();
    public IMilestone Milestone => new MilestoneImplementation();
}
