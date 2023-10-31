namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task copy = item with { Id = newId }; 
        DataSource.Tasks.Add(copy);
        return newId;
    }

    public void Delete(int id)
    {
        Task? result = DataSource.Tasks.Find(task => task.Id == id);
        if (result is not null)
        {
            DataSource.Tasks.Remove(result);
            result = result with { Complete = new DateTime(00,0,0) };
           DataSource.Tasks.Add(result);

        }

          
        else throw new Exception($"Task with ID={id} is not exists");
    }

    public Task? Read(int id)
    {
        Task ?result = DataSource.Tasks.Find(task => task.Id == id);
         if (result is not null)
           return result;
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task? taskToUpdate=Read(item.Id);    
        if (taskToUpdate is not null)
        {
            DataSource.Tasks.Remove(taskToUpdate);
            DataSource.Tasks.Add(item);
        }
        else 
            throw new Exception($"Task with ID={item.Id} does ");
    }
}
