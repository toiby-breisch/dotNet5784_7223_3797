namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerIementation : IEngineer
{
    /// <summary>
    ///create a new engineer
    /// </summary>
    public int Create(Engineer item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Engineer with ID={item.Id} already exists");
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
       else throw new Exception($"Engineer with ID={id} is not exists");
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
    /// read all engeneers
    /// </summary>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
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
        else throw new Exception($"Engineer with ID={item.Id} does not exists");
    }
}
   

