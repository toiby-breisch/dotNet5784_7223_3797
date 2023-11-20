﻿namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{


    public Dependency? Read(int _EngineerId, int _TaskId) //Reads entity object by 2 IDs
    {
        string fileName = "engineers";
        List<Dependency>? dependencys = new List<Dependency>();
        dependencys = XMLTools.LoadListFromXMLSerializer<Dependency>(fileName)!;
        return
         dependencys.Find(lk => lk.DependentTask == _EngineerId && lk.DependsOnTask == _TaskId);
    }

        public int Create(Dependency item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool isDepend(int dependentTask, int dependsOnTask)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter=null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
