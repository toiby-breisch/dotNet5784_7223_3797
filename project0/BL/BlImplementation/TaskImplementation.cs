namespace BlImplementation;
using BlApi;
using BO;
using DO;

using System.Collections;
using System.Collections.Generic;
using System.Numerics;


internal class Task : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Add(BO.Task item)
    {
        if (item.Id <= 0 || item.Alias == "")
            throw new BlNullPropertyException(nameof(item));
        // _dal.Dependency.ReadAll()
        //ליצור Dependency
        //ליצור Do task
        DO.EngineerExperience CopmlexityLevel =(DO.EngineerExperience) item.CopmlexityLevel!;
      
        DO.Task temp=new DO.Task(item.Id,item.Description,item.Alias,Milestone=null,
            DateTime.Now,item.StartDate, item.ForecastDate, item.DeadlineDate, item.CompleteDate, Deliverables=null,item.Remarks,
            item.engineer!.Id, CopmlexityLevel);
    //public int Id { get; init; }
    //public string? Description { get; set; }
    //public string? Alias { get; set; }
    //public Status? Status { get; set; }
    //public TaskInList? TaskInList { get; set; }
    //public DateTime BaseLineStartDate { get; set; }
    //public DateTime StartDate { get; set; }
    //public DateTime ScheduledStartDate { get; set; }n
    //public DateTime ForecastDate { get; set; }
    //public DateTime DeadlineDate { get; set; }
    //public DateTime CompleteDate { get; set; }
    //public string? Remarks { get; set; }
    //public string? product { get; set; }
    //public EngineerInTask? engineer { get; set; }
    //public Milestone? relatedMilestone { get; set; }
    //public EngineerExperience? CopmlexityLevel { get; set; }
    //public bool IsActive { get; set; }

       
    //    int Id,
    //string? Description,
    //string? Alias,
    //bool Milestone,
    //DateTime CreatedAt,
    //DateTime? Start,
    //DateTime? ForecasDate,
    //DateTime? Deadline,
    //DateTime? Complete,
    //string? Deliverables,
    //string? Remarks,
    //int Engineerid,
    //EngineerExperience CopmlexityLevel
        try
        {
            _dal.Task.Create(temp);
        }
        catch { };
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {

        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new DalAlreadyExistsException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {
            Id = id,
            Description = doTask.Description,
            Alias = doTask.Alias,

           // CreatedAt = doTask.CreatedAt,
           // IsActive = doTask.IsActive,
            
           // Description,
           // string ? Alias,
           //// bool Milestone,????
           // DateTime CreatedAt,
           // DateTime ? Start,
           // DateTime ? ForecasDate,
           // DateTime ? Deadline,
           // DateTime ? Complete,
           // string ? Deliverables,
           // string ? Remarks,
           // int Engineerid,
           // EngineerExperience CopmlexityLevel

        };
    }

    public IEnumerable<TaskInList> ReadAll(Func<BO.Engineer?, bool> filter=null!)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}