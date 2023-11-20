namespace Dal;
using DalApi;
using DO;
using System;


using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        if (Engineers.Find(x => x.Id == item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(Engineers!, fileName);
        return item.Id;
    }
    public void Delete(int id)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        Engineer? result = Engineers.FirstOrDefault(engineer => engineer.Id == id);
        if (result is not null)
        {
            Engineers.Remove(result);
            XMLTools.SaveListToXMLSerializer<Engineer>(Engineers!, fileName);
        }
        else throw new DalDoesNotExistException($"Engineer with ID={id} is not exists");

    }

    public Engineer? Read(int id)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        Engineer? result = Engineers.FirstOrDefault(engineer => engineer.Id == id);
        if (result is not null)
            return result;
        return null;
    }
    
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        return Engineers.FirstOrDefault(d => filter(d));
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool> filter)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        if (filter != null)
        {
            return from item in Engineers
                   where filter(item)
                   select item;
        }
        return (from item in Engineers
                select item);

    }

    public void Update(Engineer item)
    {
        string fileName = "engineers";
        List<Engineer>? Engineers = new List<Engineer>();
        Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(fileName)!;
        Engineer? engineerToUpdate = Read(item.Id);
        if (engineerToUpdate is not null)
        {
            Engineers.Remove(engineerToUpdate);
            Engineer copy = new(item.Id, item.Name, item.Email, item.Level, item.Cost);
            Engineers.Add(copy);
            XMLTools.SaveListToXMLSerializer<Engineer>(Engineers!, fileName);
        }
        else throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exists");
    }
}




