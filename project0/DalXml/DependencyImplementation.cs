namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    
    //<summary>
    // check if 2 tasks are dependency
    //</summary>
    public bool isDepend(int dependentTask, int dependsOnTask) 
    {
        const string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
       
       
        Dependency? isDepend= dependencies.Find(lk => lk.DependentTask == dependentTask && lk.DependsOnTask == dependsOnTask);
        return
            isDepend != null;
    }
    //<summary>
    // create a new dependency
    //</summary>

    public int Create(Dependency item)
    {
        const string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        int newId =Config.NextDependencyId;
        Dependency copy = item with { Id = newId };
        dependencies.Add(copy);
        XMLTools.SaveListToXMLSerializer<Dependency>(dependencies!, fileName);
        return newId;
    }
    //<summary>
    // delete a dependency
    //</summary>

    public void Delete(int id)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        Dependency? result = dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
        {
            dependencies.Remove(result);
            XMLTools.SaveListToXMLSerializer<Dependency>(dependencies!, fileName);
        }
        else throw new DalDoesNotExistException($"Dependency with ID={id} is not exists");
    }
    //<summary>
    // read a  dependency
    //</summary>
    public Dependency? Read(int id)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        Dependency? result = dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
            return result;
        return null;
    }
    //<summary>
    // read a  dependency
    //</summary>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        return dependencies.FirstOrDefault(d => filter(d));
    }
    //<summary>
    // read all the  dependencies
    //</summary>

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter=null)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        return new List<Dependency>(dependencies);
    }

    //<summary>
    // update a dependency
    //</summary>
    public void Update(Dependency item)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        
            Dependency? dependencyToUpdate = Read(item.Id);
            if (dependencyToUpdate is not null)
            {
                dependencies.Remove(dependencyToUpdate);
                Dependency dependency = new(item.Id, item.DependentTask, item.DependsOnTask);
                dependencies.Add(dependency);
            XMLTools.SaveListToXMLSerializer<Dependency>(dependencies!, fileName);
        }
            else throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exists ");
        }
}
