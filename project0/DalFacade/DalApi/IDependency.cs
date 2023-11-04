namespace DalApi;
using DO;

public interface IDependency : ICrud<Dependency>
{
    public bool isDepend(int dependentTask, int dependsOnTask);//check if the task is depend
}
