namespace DalApi;
using DO;
using System;
using System.Collections.Generic;

public interface IDependency : ICrud<Dependency>
{
    public bool isDepend(int dependentTask, int dependsOnTask);//check if the task is depend
   
}
