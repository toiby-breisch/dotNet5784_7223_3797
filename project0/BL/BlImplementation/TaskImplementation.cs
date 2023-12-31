namespace BlImplementation;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
internal class TaskIplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.Status Scheduled { get; private set; }

    public int create(BO.Task BoTask)
    {
        if (BoTask.Id <= 0 || BoTask.Alias == "")
             throw new BO.BlNullPropertyException(nameof(BoTask));
            try
            {
                DO.EngineerExperience CopmlexityLevel = (DO.EngineerExperience)BoTask.CopmlexityLevel!;
                DO.Task doTask = new DO.Task(BoTask.Id, BoTask.Description, BoTask.Alias, false,
                    DateTime.Now, BoTask.StartDate, BoTask.ForecastDate, BoTask.DeadlineDate, BoTask.CompleteDate, BoTask.Deliverables, BoTask.Remarks,
                    BoTask.engineer!.Id, CopmlexityLevel, true);
                if (BoTask.DependenciesList == null)
                {
                    return BoTask.Id;
                }
                var dependencies = from BO.TaskInList task in BoTask.DependenciesList
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
        //אי אפשר למוק משימה אחרי יצירת הלו"ז 
        try
        {
            var task = _dal.Task.ReadAll((task) => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); ;
            }
            DO.Dependency dependency = _dal.Dependency.ReadAll((dependency) => dependency.DependentTask == id).FirstOrDefault()!;
            if (dependency == null)
            {
                task = task with { IsActive = false };
                _dal.Task.Create(task);
                _dal.Task.Delete(id);
            }
            else
                throw new BO.BlMustNotBeDeleted("This Task must not be deleted");
        }
        catch(DO.DalDoesNotExistException) 
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists");
        };
    }

    public BO.Task? Read(int id)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(id);
            if (doTask == null)
                throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); ;
            if (!doTask!.IsActive) { throw new Exception(); }
            return (createBoTaskFromDoTask(doTask, _dal));

        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} is not exists");
        };
    }
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter)
    {
        IEnumerable<DO.Task?> allTasks = _dal.Task.ReadAll((Func<DO.Task?, bool>)filter);
        IEnumerable<BO.Task> allTaskinBo = allTasks.Select(task => new BO.Task
        {
            Id = task!.Id,
            Description = task!.Description!,
            Alias = task!.Alias,
            milestone = _dal.Task.ReadAll(null!).Select(t => new BO.MilestoneInTask
            {
                Id = t!.Id,
                Alias = t.Alias
            }
        ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll(d => d!.DependentTask == t!.Id && d.DependsOnTask == t.Id)) is not null)).FirstOrDefault(),
            status = getStatuesOfTask(task),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id).Select(d => new BO.TaskInList
            {
                Id = d!.Id,
                Alias = _dal.Task.Read(d.Id)!.Alias,
                Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            CreatedAtDate = task!.CreatedAt,
            scheduledDate = task.scheduledDate,
            StartDate = task.StartDate,
            ForecastDate = DateTime.Now/*doTask.ForecastDate,*/,
            DeadlineDate = task.DeadlineDate,
            CompleteDate = task.CompleteDate,
            Deliverables = task.Deliverables,
            Remarks = task.Remarks,
            CopmlexityLevel = (BO.EngineerExperience)task.CopmlexityLevel,
        });
        return allTaskinBo;
    }



    public void Update(BO.Task item)
    {
        //איזה בדיקות על התאריכם?
        if (item.StartDate > item.scheduledDate ||
            item.ForecastDate < item.CompleteDate || item.DeadlineDate < item.CompleteDate ||
            item.Id <= 0 || item.Alias == "")

            throw new NotImplementedException();
        try
        {
            _dal.Task.Update(new DO.Task(item.Id, item.Description, item.Alias, false,
               item.CreatedAtDate, item.StartDate, item.ForecastDate,
              item.DeadlineDate, item.CompleteDate, item.Deliverables, item.Remarks,
              item.engineer!.Id, (DO.EngineerExperience)item.CopmlexityLevel!, true));
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={item.Id} is not exists");
        };
    }

    //        DO.Task? doTask = _dal.Task.Read(id);
    //    var milestomeInList = _dal.Task.ReadAll().Select(t => new MilestomeInList
    //    {
    //Id = t!.Id,
    //    Alias = t.Alias,
    //    Description = t.Description,
    //    CreatedAt = t.CreatedAt,
    //    CompletionPercentage = 33/* t.Complete[] double*/ ,
    //    Status = getStatuesOfTask(t)/**/
//    }
//    ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == doTask!.Id && d.DependOnTask == t.Id)) is not null)).FirstOrDefault();
//       return new BO.Task
//        {
//            Id = doTask!.Id,
//            Description = doTask!.Description,
//            Alias = doTask!.Alias,
//            Milestone = milestomeInList,
//            Status = getStatuesOfTask(doTask),
//          
//})
//        };
private BO.Status getStatuesOfTask(DO.Task task)
    {
        DateTime now = DateTime.Now;
        if (task.scheduledDate == DateTime.MinValue)
            return BO.Status.Unscheduled;
        else if (task.StartDate == DateTime.MinValue)
            return BO.Status.Scheduled;
        else if (task.DeadlineDate < now && task.CompleteDate == DateTime.MinValue)
            return BO.Status.InJeopardy;
        else return BO.Status.OnTrack;
    }
    private BO.EngineerInTask GetEngineerInTask(DO.Task doTask)
    {
        try
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(doTask.Engineerid);
            BO.EngineerInTask engineerInTask = new(doTask.Engineerid, doEngineer!.Name);
            return engineerInTask;
        }
        catch (DO.DalDoesNotExistException) {

            throw new BO.BlDoesNotExistException($"Task with ID={doTask.Id} is not exists");
        }
      
    }
     private BO.Task createBoTaskFromDoTask(DO.Task doTask, DalApi.IDal _dal)
     {
        try
        {
            BO.MilestoneInTask? milestomeInList = _dal.Task.ReadAll(null!).Select(t => new BO.MilestoneInTask
            {
                Id = t!.Id,
                Alias = t.Alias
            }
           ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll
           ((d => d.DependentTask == doTask!.Id && d.DependsOnTask == t.Id)) is not null).FirstOrDefault());

            return new BO.Task()
            {
                Id = doTask.Id,
                Description = doTask.Description!,
                Alias = doTask.Alias!,
                CreatedAtDate = doTask.CreatedAt,
                status = (BO.Status)1,// getStatuesOfTask(doTask),
                DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == doTask.Id).Select(d => new BO.TaskInList
                {
                    Id = d!.Id,
                    Alias = _dal.Task.Read(d.Id)!.Alias,
                    Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                    Description = _dal.Task.Read(d.Id)!.Description
                }),

                milestone = milestomeInList,
                StartDate = doTask.StartDate,
                ForecastDate = null,//Nה לשים פה?
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Remarks = doTask.Remarks,
                Deliverables = doTask.Deliverables,
                engineer = GetEngineerInTask(doTask),
                CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
            };
        }
        catch (DO.DalDoesNotExistException)
        {

            throw new BO.BlDoesNotExistException($"Task with ID={doTask.Id} is not exists");
        }

    }


}
