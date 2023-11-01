namespace DalApi;
using DO;

public interface IDependency : ICrud<T>
{
    public bool isDepend(int dependentTask, int dependsOnTask);//check if the task is depend
}
