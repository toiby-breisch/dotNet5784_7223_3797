namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerIementation : IEngineer
{
    public int Create(Engineer item)//add a new engineer
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        Engineer? result = DataSource.Engineers.Find(engineer => engineer.Id == id);
        if (result is not null)
            DataSource.Engineers.Remove(result);
        throw new Exception($"Engineer with ID={id} is not exists");
    }

    public Engineer? Read(int id)
    {
        Engineer? result = DataSource.Engineers.Find(engineer => engineer.Id == id);
        if (result is not null)
            return result;
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is not null)
        {
            DataSource.Engineers.Remove(engineerToUpdate);
            Engineer copy = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
            DataSource.Engineers.Add(copy);
        }
        else throw new Exception($"Engineer with ID={item.Id} does not exists");
    }
    }
   

