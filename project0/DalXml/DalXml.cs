
namespace Dal;

using DalApi;
using DO;


public class DalXml : IDal
    {
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new  EngineerImplementation();
    public IDependency Dependency => new DependencyImplementation();
}

