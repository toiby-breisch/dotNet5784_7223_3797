namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = newId };
        DataSource.Dependencies.Add(copy);
        return newId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        Dependency?result = DataSource.Dependencies.Find(dependency => dependency.Id == id);
            if (result is not null)
            return result;
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);

    }

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
