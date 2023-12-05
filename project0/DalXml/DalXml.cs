
namespace Dal;

using DalApi;
using DO;


sealed internal class DalXml : IDal
{
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new  EngineerImplementation();
    public IDependency Dependency => new DependencyImplementation();
    public static IDal Instance { get; } = new Lazy<DalXml>(true).Value;
    private DalXml() { }
}



    


