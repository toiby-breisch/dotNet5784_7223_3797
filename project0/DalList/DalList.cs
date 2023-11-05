namespace Dal;

using DalApi;
using DO;



sealed public class DalList : IDal
{
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new EngineerIementation();
    public IDependency Dependency => new DependencyImplementation();
}