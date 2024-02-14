namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
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

    public bool IsDepend(int dependentTask, int dependsOnTask)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(d => d.DependentTask == dependentTask && d.DependsOnTask == dependsOnTask);
        if (result is not null)
            return true;
        return false;
    }

    /// <summary>
    /// delete a  dependency
    /// </summary>
    public void Delete(int id)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
            DataSource.Dependencies.Remove(result);
        else throw new DalDoesNotExistException($"Dependency with ID={id} is not exists");
    }
    /// <summary>
    /// read a dependency
    /// </summary>
    public Dependency? Read(int id)
    {
        Dependency? result = DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
            return result;
        return null;
    }
    /// <summary>
    /// read all dependencies
    /// </summary>

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter)
    {
        if (filter == null)
            return DataSource.Dependencies.Select(dependency => dependency);
        else
            return DataSource.Dependencies.Where(filter);

    }

    /// <summary>
    /// update a dependency
    /// </summary>
    /// 
    public void Update(Dependency item)
    {
        Dependency? dependencyToUpdate = Read(item.Id);
        if (dependencyToUpdate is not null)
        {
            DataSource.Dependencies.Remove(dependencyToUpdate);
            Dependency dependency = new(item.Id, item.DependentTask, item.DependsOnTask);
            DataSource.Dependencies.Add(dependency);
        }
        else throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exists ");
    }

    /// <summary>
    /// read a dependency according to a parameter
    /// </summary>
    /// 
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(d => filter(d));
    }
}
