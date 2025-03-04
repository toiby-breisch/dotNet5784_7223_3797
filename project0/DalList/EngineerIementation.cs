﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerIementation : IEngineer
{
    /// <summary>
    ///create a new engineer
    /// </summary>
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }
    /// <summary>
    /// delete an engineer
    /// </summary>
    public void Delete(int id)
    {
        Engineer? result = DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == id);
        if (result is not null)
            DataSource.Engineers.Remove(result);
       else throw new DalDoesNotExistException($"Engineer with ID={id} is not exists");
    }
    /// <summary>
    /// read an engineer
    /// </summary>
    public Engineer? Read(int id)
    {
        Engineer? result = DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == id);
        if (result is not null)
            return result;
        return null;
    }
   
    /// <summary>
    /// update an engineer
    /// </summary>
    public void Update(Engineer item)
    {
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is not null)
        {
            DataSource.Engineers.Remove(engineerToUpdate);
            Engineer copy = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
            DataSource.Engineers.Add(copy);
        }
        else throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exists");
    }
    /// <summary>
    /// read an engineer according to a parameter
    /// </summary>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(d => filter(d));
    }
    /// <summary>
    /// read all the engineers
    /// </summary>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }

}


