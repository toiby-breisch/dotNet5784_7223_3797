namespace BlImplementation;

using DalApi;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Task implemrntation.
/// </summary>
internal class TaskIplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IDal Dal { get => _dal; set => _dal = value; }

    public TaskIplementation(IDal dal)
    {
        Dal = dal;
    }

    public TaskIplementation()
    {
    }

    /// <summary>
    /// The function create a new task.
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns> task's id</returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public int Create(BO.Task boTask)
    {
        if (boTask.StartDate > boTask.ScheduledDate || boTask.ScheduledDate > boTask.ForecastDate ||
           boTask.ForecastDate < boTask.CompleteDate || boTask.DeadlineDate < boTask.CompleteDate )
            throw new BO.BlNullOrNotIllegalPropertyException($"The dates you enterd are not legal");
        if (boTask.Id < 0 || boTask.Alias == "" || boTask.Description == "")
            throw new BO.BlNullOrNotIllegalPropertyException("ERROR: '\n'The data you entered is incorrect.");

        if (Dal.Engineer.Read(boTask!.Engineer!.Id) == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={boTask!.Engineer!.Id} does not exixt ");

        try
        {
            DO.EngineerExperience copmlexityLevel = (DO.EngineerExperience)boTask.CopmlexityLevel!;
            DO.Task doTask = new(boTask.Id, boTask.Description, boTask.Alias, false,
                DateTime.Now, boTask.StartDate, boTask.ForecastDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks,
                boTask.Engineer!.Id, copmlexityLevel, true);
            Dal.Task.Create(doTask);
            if (boTask.DependenciesList == null)
            {
                return boTask.Id;
            }
            var dependencies = from BO.TaskInList task in boTask.DependenciesList
                               select new DO.Dependency(0, boTask.Id, boTask.Id);

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
        var task = Dal.Task.ReadAll((task) => task.Id == id).FirstOrDefault();
        if (task == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists");
        }
        DO.Dependency dependency = Dal.Dependency.ReadAll((dependency) => dependency.DependsOnTask == id).FirstOrDefault()!;
        if (dependency == null)
        {
            task = task with { IsActive = false };
            Dal.Task.Create(task);
            Dal.Task.Delete(id);
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
            DO.Task? doTask = Dal.Task.Read(id);
            if (!doTask!.IsActive)
            {
                throw new BO.BlCantReadUnActive($"Task with ID={id} isnt active");
            };
            return new BO.Task()
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                CreatedAtDate = doTask.CreatedAt,
                status = GetStatuesOfTask(doTask),
                DependenciesList = GetDependenciesList(doTask),
                Milestone = GetMilistoneInLis(doTask),
                StartDate = doTask.StartDate,
                ForecastDate = null,
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Remarks = doTask.Remarks,
                Deliverables = doTask.Deliverables,
                Engineer = GetEngineerInTask(doTask),
                CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
            };

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists", ex);
        }
    }

    /// <summary>
    /// The function reads all the tasks.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<BO.Task></returns>
    /// 
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter = null)
    {

        IEnumerable<BO.Task> allTasks = from task in Dal.Task.ReadAll()
                                        select new BO.Task
                                        {
                                            Id = task.Id,
                                            Description = task.Description!,
                                            Alias = task!.Alias,
                                            status = GetStatuesOfTask(task),
                                            Milestone = GetMilistoneInLis(task),
                                            DependenciesList = GetDependenciesList(task),
                                            CreatedAtDate = task.CreatedAt,
                                            ScheduledDate = task.scheduledDate,
                                            StartDate = task.StartDate,
                                            ForecastDate = DateTime.Now,
                                            DeadlineDate = task.DeadlineDate,
                                            CompleteDate = task.CompleteDate,
                                            Deliverables = task.Deliverables,
                                            Remarks = task.Remarks,
                                            CopmlexityLevel = (BO.EngineerExperience)task.CopmlexityLevel,
                                        };
        return filter == null ? allTasks : allTasks.Where(filter);

    }

    /// <summary>
    /// The function updates a task
    /// </summary>
    /// <param name="task"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task task)
    {

        if (task.StartDate > task.ScheduledDate || task.ScheduledDate > task.ForecastDate ||
          task.ForecastDate < task.CompleteDate || task.DeadlineDate < task.CompleteDate )
            throw new BO.BlNullOrNotIllegalPropertyException($"The dates you enterd are not legal");

        if (Dal.Engineer.Read(task!.Engineer!.Id) == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={task!.Engineer!.Id} does not exixt ");

        try
        {
            Dal.Task.Update(new DO.Task(task.Id, task.Description, task.Alias, false,
               task.CreatedAtDate, task.StartDate, task.ForecastDate,
              task.DeadlineDate, task.CompleteDate, task.Deliverables, task.Remarks,
              task.Engineer!.Id, (DO.EngineerExperience)task.CopmlexityLevel!, true));
        }


        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does not existes ", ex);
        }
    }

    /// <summary>
    /// The function gets the status of task.
    /// </summary>
    /// <param name="task"></param>
    /// <returns> BO.Status</returns>
    private static BO.Status GetStatuesOfTask(DO.Task task)
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
        DO.Engineer? doEngineer = Dal.Engineer.Read(doTask.Engineerid);
        BO.EngineerInTask engineerInTask = new(doTask.Engineerid, doEngineer!.Name);
        return engineerInTask;
    }
    /// <summary>
    /// The function gets the DependenciesList's task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>IEnumerable<TaskInList?></returns>
    private IEnumerable<BO.TaskInList?> GetDependenciesList(DO.Task task)
    {
        IEnumerable<BO.TaskInList?> DependenciesList = Dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id).Select(d => new BO.TaskInList
        {
            Id = d!.Id,
            Alias = Dal.Task.Read(d.Id)!.Alias,
            Status = GetStatuesOfTask(Dal.Task.Read(d.Id)!),
            Description = Dal.Task.Read(d.Id)!.Description
        });
        return DependenciesList;
    }
    /// <summary>
    /// The function gets the MilistoneInList's task.
    /// </summary>
    /// <param name="doTask"></param>
    /// <returns>BO.MilestoneInTask?</returns>
    private BO.MilestoneInTask? GetMilistoneInLis(DO.Task doTask)
    {
        BO.MilestoneInTask milestoneInTask = Dal.Task.ReadAll().Select(t => new BO.MilestoneInTask
        {
            Id = t!.Id,
            Alias = t.Alias
        }
        ).Where(t => (Dal.Task.Read(t.Id)!.Milestone && (Dal.Dependency.ReadAll
        (d => d.DependentTask == doTask!.Id && d.DependsOnTask == t.Id)) is not null)).FirstOrDefault()!;
        return milestoneInTask;
    }
}
