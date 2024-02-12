namespace BlImplementation;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
/// <summary>
/// Task implemrntation.
/// </summary>
internal class TaskIplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// The function create a new task.
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns> task's id</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public int create(BO.Task boTask)
    {
        if (boTask.Id <= 0 || boTask.Alias == "")
            throw new BO.BlNullOrNotIllegalPropertyException(nameof(boTask));
        try
        {
            DO.EngineerExperience copmlexityLevel = (DO.EngineerExperience)boTask.CopmlexityLevel!;
            DO.Task doTask = new DO.Task(boTask.Id, boTask.Description, boTask.Alias, false,
                DateTime.Now, boTask.StartDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks,
                boTask.Engineer!.Id, copmlexityLevel, true);
            if (boTask.DependenciesList == null)
            {
                return boTask.Id;
            }
            var dependencies = from BO.TaskInList task in boTask.DependenciesList
                               select new DO.Dependency(0, boTask.Id, task.Id);
            _dal.Task.Create(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        };
        return boTask.Id;
    }
    /// <summary>
    /// The function deletes a Task.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlDeletionImpossible"></exception>
    public void Delete(int id)
    {
        //אי אפשר למוק משימה אחרי יצירת הלו"ז 
        var task = _dal.Task.ReadAll((task) => task.Id == id).FirstOrDefault();
        if (task == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists");
        }
        DO.Dependency dependency = _dal.Dependency.ReadAll((dependency) => dependency.DependsOnTask == id).FirstOrDefault()!;
        if (dependency == null)
        {
            task = task with { IsActive = false };
            _dal.Task.Create(task);
            _dal.Task.Delete(id);
        }
        else

            throw new BO.BlDeletionImpossible("This Task must not be deleted");

    }
/// <summary>
/// The function read a task.
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
/// <exception cref="BlCantReadUnActive"></exception>
/// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            if (!doTask!.IsActive) {
                throw new BO.BlCantReadUnActive($"Task with ID={id} isnt active");
            };
            return new BO.Task()
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                CreatedAtDate = doTask.CreatedAt,
                status = getStatuesOfTask(doTask),
                DependenciesList = getDependenciesList(doTask),
                Milestone = getMilistoneInLis(doTask),
                StartDate = doTask.StartDate,
                ForecastDate = null,//Nה לשים פה?
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Remarks = doTask.Remarks,
                Deliverables = doTask.Deliverables,
                Engineer = GetEngineerInTask(doTask),
                CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
            };

        }
        catch(DO.DalDoesNotExistException ex) {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists",ex);
        }
    }

    /// <summary>
    /// The function reads all the tasks.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<BO.Task></returns>
    /// 
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter=null)
    {

        IEnumerable<BO.Task> allTasks = from task in _dal.Task.ReadAll()
                                            select new BO.Task
        {
            Id = task.Id,
            Description = task.Description!,
            Alias = task!.Alias,
            status = getStatuesOfTask(task),
            Milestone = getMilistoneInLis(task),   
            DependenciesList = getDependenciesList(task),
            CreatedAtDate = task.CreatedAt,
            ScheduledDate = task.scheduledDate,
            StartDate = task.StartDate,
            ForecastDate = DateTime.Now/*doTask.ForecastDate,*/,
            DeadlineDate = task.DeadlineDate,
            CompleteDate = task.CompleteDate,
            Deliverables = task.Deliverables,
            Remarks = task.Remarks,
            CopmlexityLevel = (BO.EngineerExperience)task.CopmlexityLevel,
        };
        return filter == null ? allTasks : allTasks.Where(filter);

    }
    //public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter = null)
    //{

    //    IEnumerable<BO.Engineer> allTasks = from doEngineer in _dal.Engineer.ReadAll()
    //                                        select new BO.Engineer
    //                                        {
    //                                            Id = doEngineer.Id,
    //                                            Name = doEngineer.Name,
    //                                            Email = doEngineer.Email,
    //                                            Level = (BO.EngineerExperience)doEngineer.Level,
    //                                            Cost = doEngineer.Cost,
    //                                            CurrentTask = GetCurrentTaskOfEngineerActive(doEngineer.Id)
    //                                        };
    //    return filter == null ? allTasks : allTasks.Where(filter);

    //}
    /// <summary>
    /// The function updates a task
    /// </summary>
    /// <param name="task"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task task)
    {
        //איזה בדיקות על התאריכם?
        if (task.StartDate > task.ScheduledDate || task.ScheduledDate>task.ForecastDate||
            task.ForecastDate < task.CompleteDate || task.DeadlineDate < task.CompleteDate ||
            task.Id <= 0 || task.Alias == "")
            throw new BO.BlNullOrNotIllegalPropertyException($"Milistone with ID={task.Id}lacks values");
        try
        {
            _dal.Task.Update(new DO.Task(task.Id, task.Description, task.Alias, false,
               task.CreatedAtDate, task.StartDate, task.ForecastDate,
              task.DeadlineDate, task.CompleteDate, task.Deliverables, task.Remarks,
              task.Engineer!.Id, (DO.EngineerExperience)task.CopmlexityLevel!, true));
        }
 

        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does not existes ", ex);
        }
    }
    /// <summary>
    /// The function gets the status of task.
    /// </summary>
    /// <param name="task"></param>
    /// <returns> BO.Status</returns>
    private BO.Status getStatuesOfTask(DO.Task task)
    {
        DateTime now = DateTime.Now;
        if (task.scheduledDate == null)
            return BO.Status.Unscheduled;
        else if (task.StartDate == null)
            return BO.Status.Scheduled;
        else if (task.DeadlineDate < now && task.CompleteDate == null)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;

    }
    /// <summary>
    /// The function gets the EngineerInTask's task.
    /// </summary>
    /// <param name="doTask"></param>
    /// <returns>BO.EngineerInTask</returns>
    private BO.EngineerInTask GetEngineerInTask(DO.Task doTask)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(doTask.Engineerid);
        BO.EngineerInTask engineerInTask = new(doTask.Engineerid, doEngineer!.Name);
        return engineerInTask;
    }
    /// <summary>
    /// The function gets the DependenciesList's task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>IEnumerable<TaskInList?></returns>
    private IEnumerable<BO.TaskInList?> getDependenciesList(DO.Task task)
    {
        IEnumerable<BO.TaskInList?> DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id).Select(d => new BO.TaskInList
        {
            Id = d!.Id,
            Alias = _dal.Task.Read(d.Id)!.Alias,
            Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
            Description = _dal.Task.Read(d.Id)!.Description
        });
        return DependenciesList;
    }
    /// <summary>
    /// The function gets the MilistoneInList's task.
    /// </summary>
    /// <param name="doTask"></param>
    /// <returns>BO.MilestoneInTask?</returns>
    private BO.MilestoneInTask? getMilistoneInLis(DO.Task doTask) {
        BO.MilestoneInTask milestoneInTask = _dal.Task.ReadAll().Select(t => new BO.MilestoneInTask
        {
            Id = t!.Id,
            Alias = t.Alias
        }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll
        (d => d.DependentTask == doTask!.Id && d.DependsOnTask == t.Id)) is not null)).FirstOrDefault()!;
        return milestoneInTask;
    }
}
