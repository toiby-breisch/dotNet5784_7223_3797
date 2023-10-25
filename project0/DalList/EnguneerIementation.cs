namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EnguneerIementation : Iengineer
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
        if(DataSource.Engineers.Contains(Read(id)))
            return 
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<T>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
