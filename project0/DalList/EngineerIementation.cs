namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerIementation : Iengineer
{
    public int Create(Engineer item)//add a new engineer
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Student with ID={item.Id} already exists");
        DataSource.Engineers.Add(item);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        Engineer result= DataSource.Engineers.Find(Engineer => Engineer.Id = id)
        if (result)
            return result;
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {

        if (Read(item.Id) is not null)
        {
            DataSource.Engineers.Remove(Read(item.Id);
             DataSource.Engineers.Add(item);
        }
        throw new Exception($"Engineer with ID={item.Id} does not exists");

    }
   

