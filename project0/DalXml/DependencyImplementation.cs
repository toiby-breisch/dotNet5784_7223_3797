namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{


    public bool isDepend(int dependentTask, int dependsOnTask) //Reads entity object by 2 IDs
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
       
        Dependency? isDepend= dependencies.Find(lk => lk.DependentTask == dependentTask && lk.DependsOnTask == dependsOnTask);
        return
            isDepend != null;
    }

        public int Create(Dependency item)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        int newId =Config.NextDependencyId;
        Dependency copy = item with { Id = newId };
        dependencies.Add(copy);
        XMLTools.SaveListToXMLSerializer<Dependency>(dependencies!, fileName);
        return newId;
    }

    public void Delete(int id)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        Dependency? result = dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
        {
            dependencies.Remove(result);
            XMLTools.SaveListToXMLSerializer<Dependency>(dependencies!, fileName);
        }
        else throw new DalDoesNotExistException($"Dependency with ID={id} is not exists");
    }
    //public bool isDepend(int dependentTask, int dependsOnTask)
    //{
    //    throw new NotImplementedException();
    //}


    public Dependency? Read(int id)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        Dependency? result = dependencies.FirstOrDefault(dependency => dependency.Id == id);
        if (result is not null)
            return result;
        return null;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        return dependencies.FirstOrDefault(d => filter(d));
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter=null)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        return new List<Dependency>(dependencies);
    }

    public void Update(Dependency item)
    {
        string fileName = "dependencies";
        List<Dependency>? dependencies = new List<Dependency>();
        dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        
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
