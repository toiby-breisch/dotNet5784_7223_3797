
using DalApi;
namespace Dal;

//stage 2
sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerIementation();
    public IDependency Dependency => new DependencyImplementation();
    public ITask Task => new TaskImplementation();

   
}