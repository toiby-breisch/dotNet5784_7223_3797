namespace DalApi;
using DO;
using System;
using System.Collections.Generic;

public interface IDependency : ICrud<Dependency>
{
    public bool IsDepend(int dependentTask, int dependsOnTask);//check if the task is depend
   
}
