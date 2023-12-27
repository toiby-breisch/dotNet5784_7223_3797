namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using DO;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;


internal class Task : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int create(BO.Task BoTask)
    {
        if (BoTask.Id <= 0 || BoTask.Alias == "")
            throw new BlNullPropertyException(nameof(BoTask));
        try
        {
            DO.EngineerExperience CopmlexityLevel = (DO.EngineerExperience)BoTask.CopmlexityLevel!;
            DO.Task doTask = new DO.Task(BoTask.Id, BoTask.Description, BoTask.Alias, false,
                DateTime.Now, BoTask.StartDate, BoTask.ForecastDate, BoTask.DeadlineDate, BoTask.CompleteDate, BoTask.Deliverables, BoTask.Remarks,
                BoTask.engineer!.Id, CopmlexityLevel, true);
            if (BoTask.DependsList == null)
            {
                return BoTask.Id;
            }
            var dependencies = from TaskInList task in BoTask.DependsList
                               select new DO.Dependency(0, BoTask.Id, task.Id);

            _dal.Task.Create(doTask);
        }
        catch
        {
            //////////
        };
        return BoTask.Id;
    }

    public void Delete(int id)
    {
        try
        {
            var task = _dal.Task.ReadAll((task) => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                throw new Exception();
            }
            Dependency dependency = _dal.Dependency.ReadAll((dependency) => dependency.DependsOnTask == id).FirstOrDefault()!;
            if (dependency == null)
            {

                task = task with { IsActive = false };
                _dal.Task.Create(task);
                _dal.Task.Delete(id);
            }
            else
                throw new Exception();
        }
        catch { throw new NotImplementedException(); };

    }

    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            if (doTask == null)
                throw new DalAlreadyExistsException($"Task with ID={id} does Not exist");
            if (!doTask!.IsActive) { throw new Exception(); }
            DO.Engineer? doEngineer = _dal.Engineer.Read(doTask.Engineerid);
            EngineerInTask engineerInTask = new(doTask.Engineerid, doEngineer!.Name);
            Status status = null!;
            var dependsList = from dependency in _dal.Dependency.ReadAll(null!)
                              where dependency.DependsOnTask == doTask.Engineerid
                              select new TaskInList(id, doTask.Description, doTask.Alias, status);
            return new BO.Task()
            {
                Id = id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                CreatedAtDate = doTask.CreatedAt,
                status = status,
                DependsList = null,
                milestone = null,
                BaseLineStartDate = new DateTime(),
                StartDate = doTask.Start,
                ScheduledStartDate = null,
                ForecastDate = doTask.ForecasDate,
                DeadlineDate = doTask.Deadline,
                CompleteDate = doTask.Complete,
                Remarks = doTask.Remarks,
                Deliverables = doTask.Deliverables,
                engineer = engineerInTask,
                CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
            };
        }
        catch { return null; }
    }




//bool Milestone,
//DateTime ,

//DateTime? ForecasDate,
//DateTime? Deadline,
//DateTime? Complete,
//string? Deliverables,
//string? Remarks,
//int Engineerid,
//EngineerExperience CopmlexityLevel,
//  bool IsActive
//




public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter = null!)
    {
        throw new NotImplementedException();
       
    }

    public void Update(BO.Task item)
    {

    //public DateTime? ForecastDate { get; set; }
    //public DateTime? DeadlineDate { get; set; }
    //public DateTime? CompleteDate { get; set; }
        if(item.BaseLineStartDate < DateTime.Now|| item.ScheduledStartDate< DateTime.Now||item.ForecastDate<)
        throw new NotImplementedException();
    }
}