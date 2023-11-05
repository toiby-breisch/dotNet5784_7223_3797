namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    /// <summary>
    /// crate a new dependency
    /// </summary>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = newId };
        DataSource.Dependencies.Add(copy);
        return newId;
    }
    /// <summary>
    /// check if 2 task are dependency
    /// </summary>
    public bool isDepend(int dependentTask, int dependsOnTask)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(d => d.DependentTask == dependentTask&& d.DependsOnTask == dependsOnTask);
        if (result is not null)
            return true ;
        return false;
    }
    public bool isDepend1(int dependentTask, int dependsOnTask)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(d => d.DependentTask == dependentTask && d.DependsOnTask == dependsOnTask);
        if (result is not null)
            return true;
        return false;
    }
    /// <summary>
    /// delete a  Task
    /// </summary>
    public void Delete(int id)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
            DataSource.Dependencies.Remove(result);
       else throw new Exception($"Dependency with ID={id} is not exists");
    }
    /// <summary>
    /// read a Task
    /// </summary>
    public Dependency? Read(int id)
    {
        Dependency?result = DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
            if (result is not null)
            return result;
        return null;
    }
    /// <summary>
    /// read all Tasks
    /// </summary>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);

    }
    /// <summary>
    /// update a Task
    /// </summary>
    public void Update(Dependency item)
    {
        Dependency? dependencyToUpdate = Read(item.Id);
        if (dependencyToUpdate is not null)
        {
            DataSource.Dependencies.Remove(dependencyToUpdate);
            Dependency dependency = new(item.Id, item.DependentTask, item.DependsOnTask);
            DataSource.Dependencies.Add(dependency);
        }
        else throw new Exception($"Dependency with ID={item.Id} does") ;
    }
}
