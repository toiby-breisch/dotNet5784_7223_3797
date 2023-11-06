namespace DalApi;
using DO;
//<summary>
//Define an interface that will contain a separate attribute for each type of subinterface.
//</summary>
public interface IDal
{
    IDependency Dependency { get; }
    ITask Task { get; }
    IEngineer Engineer { get; }
}

