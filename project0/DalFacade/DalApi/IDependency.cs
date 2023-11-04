﻿namespace DalApi;
using DO;

public interface IDependency
{
    public bool isDepend(int dependentTask, int dependsOnTask);//check if the task is depend

    int Create(Dependency item); //Creates new entity object in DAL

    Dependency? Read(int id); //Reads entity object by its ID

    List<Dependency?> ReadAll(); //stage 1 only, Reads all entity objects

    void Update(Dependency item); //Updates entity object

void Delete(int id); //Deletes an object by is Id 
}
