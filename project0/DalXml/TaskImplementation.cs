
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Xml.Linq;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// get task from xelement to task
    /// </summary>

    static Task? getTask(XElement t) =>
      t.ToIntNullable("Id") is null ? null : new Task()
      {
          Id = (int)t.Element("Id")!,
          Description = (string?)t.Element("Description"),
          Alias = (string?)t.Element("Alias"),
          Milestone =(bool)t.Element("Milestone")!,
          CreatedAt = (DateTime)t.Element("CreatedAt")!,
          Start = (DateTime)t.Element("Start")!,
          ForecasDate = (DateTime)t.Element("ForecasDate")!,
          Deadline = (DateTime)t.Element("Deadline")!,
          Complete = (DateTime)t.Element("Complete")!,
          Deliverables = (string)t.Element("Deliverables")!,
          Remarks = (string)t.Element("Remarks")!,
          Engineerid = (int)t.Element("Engineerid")!,
          
      };
    /// <summary>
    /// create a task from task to xelement
    /// </summary>
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

    //<summary>
    //create a new item
    //</summary>
    public int Create(Task item)
    {
        int newId = Config.NextTaskId;
        Task copyItem = item with { Id = newId };
        string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        var el = createTaskElement(copyItem);
        tasks.Add(new XElement ("Task" ,el));
      XMLTools.SaveListToXMLElement(tasks, fileName);
        return copyItem.Id;
    }
    //<summary>
    // delete a task
    //<summary>
    public void Delete(int id)
    {
        string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        XElement? one = tasks.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (one is not null)
        {
            one!.Element("Complete")!.Value = DateTime.Now.ToString("yyyy - MM - ddThh: mm:ss", CultureInfo.InvariantCulture);
            XMLTools.SaveListToXMLElement(tasks, fileName);
        }
           else throw new DalAlreadyExistsException($"Task with ID={id} " +
                $"is not exists");
    }
    //<summary>
    //read a task
    //</summary>
    public Task? Read(int id)
    {
        string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        XElement? one = tasks.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == id.ToString()).FirstOrDefault();
        if (one is not null)
        {
          return getTask(one);
            
        }
        return null;
    }
    //<summary>
    //read a task
    //</summary>
    public Task? Read(Func<Task, bool> filter)
    {
        string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        return getTask(tasks.Elements().FirstOrDefault(d =>  filter(getTask( d)!))!);

    }
    
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter=null)
    {
       const string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        if (filter is not null)
            return tasks.Elements().Where(d => filter(getTask( d)!)).Select(getTask)! ;
        return (tasks.Elements()
            .Select(getTask)!);
        
        
    }
    //<summary>
    //update a task
    //</summary>
    public void Update(Task item)
    {

        string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        XElement? one = tasks.Elements("Task")?.
                  Where(p => p.Element("Id")?.Value == item.Id.ToString()).FirstOrDefault();
        if (one != null)
        {

            one.Remove();
            
            Task task = new(item.Id,item.Description,item.Alias,item.Milestone,item.CreatedAt,item.Start,
                item.ForecasDate,item.Deadline,item.Complete,item.Deliverables,item.Remarks,item.Engineerid,item.CopmlexityLevel);
           var x= createTaskElement(task);
            tasks.Add(new XElement("Task", x));
            //  task.Id = (int)item.Id;
            //  Description = (string?)one.Element("Description");
            //  Alias = (string?)one.Element("Alias");
            //  Milestone = (bool)one.Element("Milestone")!;
            //  CreatedAt = (DateTime)one.Element("CreatedAt")!;
            //  Start = (DateTime)one.Element("Start")!;
            //  ForecasDate = (DateTime)one.Element("ForecasDate")!;
            //  Deadline = (DateTime)one.Element("Deadline")!;
            //  Complete = (DateTime)one.Element("Complete")!;
            //  Deliverables = (string)one.Element("Deliverables")!;
            //  Remarks = (string)one.Element("Remarks")!;
            //Engineerid = (int)one.Element("Engineerid")!;
            XMLTools.SaveListToXMLElement(tasks, fileName);
        }
           else throw new DalAlreadyExistsException($"Task with ID={item.Id} " +
                $"is not exists");
       
    }
}
