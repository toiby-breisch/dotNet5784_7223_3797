
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class TaskImplementation : ITask
{
   //public XElement createXelement(Task item)
   // {
   //     Type t = typeof(Task);
   //    var prop= t.GetProperties();
   //     XElement? el = new("Task");
   //     //prop.t
        
   //     return el;
   // }




    static Task? getTask(XElement t) =>
      t.ToIntNullable("Id") is null ? null : new Task()
      {
          Id = (int)t.Element("Id")!,
          Description = (string?)t.Element("Description"),
          Alias = (string?)t.Element("Alias"),
          Milestone =(bool)t.Element("Milestone")!,
          CreatedAt = (DateTime)t.Element("CreatedAt")!,
          Start = (DateTime?)t.Element("Start"),
          ForecasDate = (DateTime)t.Element("ForecasDate")!,
          Deadline = (DateTime)t.Element("Deadline")!,
          Complete = (DateTime)t.Element("Complete")!,
          Deliverables = (string)t.Element("Deliverables")!,
          Remarks = (string)t.Element("Remarks")!,
          Engineerid = (int)t.Element("Engineerid")!,
          
      };

    static IEnumerable<XElement> createTaskElement(Task task)
    {
        yield return new XElement("Id", task.Id);
        if (task.Description is not null)
            yield return new XElement("Description", task.Description);
        if (task.Alias is not null)
            yield return new XElement("Alias", task.Alias);
        if (task.Milestone is not false)
            yield return new XElement("Milestone", task.Milestone);
        if (task.CreatedAt !=DateTime.MinValue  )
            yield return new XElement("CreatedAt", task.CreatedAt);
        if (task.Start != DateTime.MinValue)
            yield return new XElement("Start", task.Start);
        if (task.ForecasDate != DateTime.MinValue)
            yield return new XElement("ForecasDate", task.ForecasDate);
        if (task.Deadline != DateTime.MinValue)
            yield return new XElement("Deadline", task.Deadline);
        if (task.Complete != DateTime.MinValue)
            yield return new XElement("Complete", task.Complete);
        if (task.Deliverables is not null)
            yield return new XElement("Deliverables", task.Deliverables);
        if (task.Remarks is not null)
            yield return new XElement("Remarks", task.Remarks);
        if (task.Engineerid != 0)
            yield return new XElement("Engineerid", task.Engineerid);
    }
  

/// <param name="Complete">Actual end date of the assignment</param>
/// <param name="Deliverables">Product (string describing the product) of the assignment</param>
/// <param name="Remarks">Notes of the assignment</param>
/// <param name="Engineerid">The engineer ID assigned to the task</param>
/// <param name="CopmlexityLevel
    public int Create(Task item)
    {
       
        XDocument doc = XDocument.Load("tasks.xml");
        XElement e = doc.Root ?? throw new Exception("there is no data");
        XElement? one = e.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        if(one!=null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} " +
                $"already exists");
        //XElement el =createXelement(item);
        //e.Add(el);
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
        one!.Element("Complete")!.Value = DateTime.Now.ToString();
        one.Save("tasks.xml");
    }
    public Task? Read(int id)
    {
         XDocument doc = XDocument.Load("tasks.xml");
        XElement e = doc.Root ?? throw new Exception("there is no data");
        XElement? one = e.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (one is not null)
        {
          return getTask(one);
            
        }
        return null;
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
