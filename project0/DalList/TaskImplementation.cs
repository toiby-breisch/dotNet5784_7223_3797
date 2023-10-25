namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int Id = DataSource.Config;
        Task task = item;
        Task.Id = Id;
        Tasks.Add(task);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        Task result= DataSource.Tasks.Find(Task => Task.Id = id)
            if (result)
                return result;
            return null;
    }

    public List<Task> ReadAll()
    {
        return new List<T>(DataSource.Tasks);
    }

    public void Update(Task item)
    {

        if (Read(item.Id) is not null)
        {
            DataSource.Tasks.Remove(Read(item.Id);
            DataSource.Tasks.Add(item);
        }
        throw new Exception($"Task with ID={item.Id} does 
    }
}
