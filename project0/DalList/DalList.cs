namespace Dal;

using DalApi;
using DO;

sealed internal class DalList : IDal
{
    public ITask Task => new TaskImplementation();
    public IEngineer Engineer => new EngineerIementation();
    public IDependency Dependency => new DependencyImplementation();
    public static IDal Instance { get; } = new Lazy<DalList>(()=>new DalList(),true).Value;
   
    private DalList() { }


}