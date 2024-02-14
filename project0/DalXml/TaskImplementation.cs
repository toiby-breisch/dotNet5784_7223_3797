
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

    static Task? GetTask(XElement t) =>
      t.ToIntNullable("Id") is null ? null : new Task()
      {
          Id = (int)t.Element("Id")!,
          Description = (string)t.Element("Description")!,
          Alias = (string)t.Element("Alias")!,
          Milestone = (bool)t.Element("Milestone")!,
          CreatedAt = (DateTime)t.Element("CreatedAt")!,
          StartDate = (DateTime)t.Element("StartDate")!,
          ScheduledDate = (DateTime)t.Element("scheduledDate")!,
          DeadlineDate = (DateTime)t.Element("DeadlineDate")!,
          CompleteDate = (DateTime)t.Element("CompleteDate")!,
          Deliverables = (string)t.Element("Deliverables")!,
          Remarks = (string)t.Element("Remarks")!,
          Engineerid = (int)t.Element("Engineerid")!,

      };
    /// <summary>
    /// create a task from task to xelement
    /// </summary>
    static IEnumerable<XElement> CreateTaskElement(Task task)
    {
        yield return new XElement("Id", task.Id);
        if (task.Description is not null)
            yield return new XElement("Description", task.Description);
        if (task.Alias is not null)
            yield return new XElement("Alias", task.Alias);
        if (task.Milestone is not false)
            yield return new XElement("Milestone", task.Milestone);
        if (task.CreatedAt != DateTime.MinValue)
            yield return new XElement("CreatedAt", task.CreatedAt);
        if (task.StartDate != DateTime.MinValue)
            yield return new XElement("Start", task.StartDate);
        if (task.ScheduledDate != DateTime.MinValue)
            yield return new XElement("ForecasDate", task.ScheduledDate);
        if (task.DeadlineDate != DateTime.MinValue)
            yield return new XElement("Deadline", task.DeadlineDate);
        if (task.CompleteDate != DateTime.MinValue)
            yield return new XElement("Complete", task.CompleteDate);
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
        var el = CreateTaskElement(copyItem);
        tasks.Add(new XElement("Task", el));
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
            one.Remove();
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
            return GetTask(one);

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
        return GetTask(tasks.Elements().FirstOrDefault(d => filter(GetTask(d)!))!);

    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        const string fileName = "tasks";
        XElement tasks = XMLTools.LoadListFromXMLElement(fileName)!;
        if (filter is not null)
            return tasks.Elements().Where(d => filter(GetTask(d)!)).Select(GetTask)!;
        return (tasks.Elements()
            .Select(GetTask)!);


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

            Task task = new(item.Id, item.Description, item.Alias, item.Milestone, item.CreatedAt, item.StartDate,
                item.ScheduledDate, item.DeadlineDate, item.CompleteDate, item.Deliverables, item.Remarks, item.Engineerid, item.CopmlexityLevel, true);
            var x = CreateTaskElement(task);
            tasks.Add(new XElement("Task", x));

            XMLTools.SaveListToXMLElement(tasks, fileName);
        }
        else throw new DalAlreadyExistsException($"Task with ID={item.Id} " +
             $"is not exists");

    }
}