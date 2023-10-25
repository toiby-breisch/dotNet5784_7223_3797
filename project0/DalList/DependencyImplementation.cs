namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int Id = DataSource.Config;
        Dependency dependency = item;
        dependency.Id = Id; 
        Dependencies.Add(dependency);

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        Dependency result = DataSource.Dependencies.Find(Dependency => Dependency.Id = id)
            if (result)
            return result;
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<T>(DataSource.Desependenci);

    }

    public void Update(Dependency item)
    {
        if (Read(item.Id) is not null)
        {
            DataSource.Dependencies.Remove(Read(item.Id);
            DataSource.Dependencies.Add(item);
        }
        throw new Exception($"Dependency with ID={item.Id} does 
    }
}
