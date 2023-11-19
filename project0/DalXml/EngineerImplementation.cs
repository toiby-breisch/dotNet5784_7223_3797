namespace Dal;
using DalApi;
using DO;
using System;


using System.Collections.Generic;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        string fileName = "engineers";
        List<Engineer> Engineers = new List<Engineer>();
        XMLTools.LoadListFromXMLSerializer<Engineer>(fileName);
         if (Engineers.Find(x => x.Id == item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(Engineers,fileName);
        return item.Id;
    }
}

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
