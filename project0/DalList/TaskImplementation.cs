﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// create a new Task
    /// </summary>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copy = item with { Id = newId }; 
        DataSource.Tasks.Add(copy);
        return newId;
    }

    /// <summary>
    /// delete a  Task
    /// </summary>
    public void Delete(int id)
    {
        Task? result = DataSource.Tasks.FirstOrDefault(task => task.Id == id);
        if (result is not null)
        {
            DataSource.Tasks.Remove(result);

        }

        else throw new DalDoesNotExistException($"Task with ID={id} is not exists");
    }
    /// <summary>
    /// read a  Task
    /// </summary>
    public Task? Read(int id)
    {
        Task ?result = DataSource.Tasks.FirstOrDefault(task => task.Id == id);
         if (result is not null)
           return result;
        throw new DalDoesNotExistException($"Task with ID={id} is not exists");
    }
  
    /// <summary>
    /// update a Task
    /// </summary>
    public void Update(Task item)
    {
        Task? taskToUpdate=Read(item.Id);    
        if (taskToUpdate is not null)
        {
            DataSource.Tasks.Remove(taskToUpdate);
            DataSource.Tasks.Add(item);
        }
        else 
            throw new DalDoesNotExistException ($"Task with ID={item.Id} does not existes ");
    }
    /// <summary>
    /// read a task according to a parameter
    /// </summary>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(d => filter(d));
    }
    /// <summary>
    /// read allthe tasks
    /// </summary>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter) 
    {
        if (filter == null)
            return DataSource.Tasks.Select(task => task);
        else
            return DataSource.Tasks.Where(filter);
    }

}
