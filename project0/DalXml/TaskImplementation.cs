
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
   public XElement createXelement(Task item)
    {
        Type t = typeof(Task);
       var prop= t.GetProperties();
        XElement? el = new("Task");
        //prop.t
        
        return el;
    }
    public int Create(Task item)
    {
       
        XDocument doc = XDocument.Load("tasks.xml");
        XElement e = doc.Root ?? throw new Exception("there is no data");
        XElement? one = e.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        if(one!=null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} " +
                $"already exists");
        XElement el =createXelement(item);
        e.Add(el);
        e.Save("tasks.xml");
        return item.Id;
    }

    public void Delete(int id)
    {
        XDocument doc = XDocument.Load("tasks.xml");
        XElement e = doc.Root ?? throw new Exception("there is no data");
        XElement? one = e.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (one != null)
            throw new DalAlreadyExistsException($"Engineer with ID={id} " +
                $"is not exists");
       // //Task? result = DataSource.Tasks.FirstOrDefault(task => task.Id == id);
       // //if (result is not null)
       // //{
       // //    DataSource.Tasks.Remove(result);
       // //    result = result with { Complete = DateTime.Now };
       // //    DataSource.Tasks.Add(result);

       // //}
       // one.Complete = DateTime.Now;
       //one!.Remove();
       //one.Save("tasks.xml");


    }
    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
