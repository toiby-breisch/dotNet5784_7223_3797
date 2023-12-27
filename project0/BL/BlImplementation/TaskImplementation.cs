namespace BlImplementation;
using BlApi;

using DalApi;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;


internal class Task : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int create(BO.Task BoTask)
    {
        if (BoTask.Id <= 0 || BoTask.Alias == "")
            // throw new BlNullPropertyException(nameof(BoTask));
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
        try
        {
            var task = _dal.Task.ReadAll((task) => task.Id == id).FirstOrDefault();
            if (task == null)
            {
                throw new Exception();
            }
            DO.Dependency dependency = _dal.Dependency.ReadAll((dependency) => dependency.DependsOnTask == id).FirstOrDefault()!;
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
                // throw new DalAlreadyExistsException($"Task with ID={id} does Not exist");
                if (!doTask!.IsActive) { throw new Exception(); }
            return (createBoTaskFromDoTask(doTask, _dal));

        }
        catch { return null; }
    }



    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool> filter = null!)
    {
        IEnumerable<BO.Task> boTasks = _dal.Task.ReadAll((Func<DO.Task?, bool>)filter).Select
            (task =>  
            new BO.Task
            {
                Id = task.Id,
                Description = task.Description!,
                Alias = task.Alias!,
                CreatedAtDate = task.CreatedAt,
                status = (BO.Status)1,// getStatuesOfTask(doTask),
                DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == task.Id).Select(d => new BO.TaskInList
                {
                    Id = d.Id,
                    Alias = _dal.Task.Read(d.Id)!.Alias,
                    Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                    Description = _dal.Task.Read(d.Id)!.Description
                }),
                milestone = _dal.Task.ReadAll(null!).Select(t => new BO.MilestoneInTask
                {
                    Id = t.Id,
                    Alias = t.Alias
                }
          ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d!.DependentTask == task!.Id && d.DependOnTask == t.Id)) is not null)).FirstOrDefault(),
               BaseLineStartDate = DateTime.Now,
                StartDate = task.Start,
                ForecastDate = task.ForecasDate,
                DeadlineDate = task.Deadline,
                CompleteDate = task.Complete,
                Remarks = task.Remarks,
                Deliverables = task.Deliverables,
                engineer = GetEngineerInTask(task ),
                CopmlexityLevel = (BO.EngineerExperience?)task.CopmlexityLevel
            }
             
                
            );
        
                                       
        return boTasks;

    }


    public void Update(BO.Task item)
    {

        if (item.BaseLineStartDate < DateTime.Now ||
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

        catch
        {
            //throw
        }
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
        return (BO.Status)1;
    }
    private BO.EngineerInTask GetEngineerInTask(DO.Task doTask)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(doTask.Engineerid);
        BO.EngineerInTask engineerInTask = new(doTask.Engineerid, doEngineer!.Name);
        return engineerInTask;
    }
private BO.Task createBoTaskFromDoTask(DO.Task doTask, DalApi.IDal _dal)
    {
          BO.MilestoneInTask? milestomeInList = _dal.Task.ReadAll(null!).Select(t => new BO.MilestoneInTask
          {
              Id = t.Id,
              Alias = t.Alias
          }
         ).Where(t => (_dal.Task.Read(t.Id)!.Milestone && (_dal.Dependency.ReadAll((d) => d.DependentTask == doTask!.Id && d.DependOnTask! == t.Id)) is not null)).FirstOrDefault();
        

        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description!,
            Alias = doTask.Alias!,
            CreatedAtDate = doTask.CreatedAt,
            status =( BO.Status)1,// getStatuesOfTask(doTask),
            DependenciesList = _dal.Dependency.ReadAll((d) => d!.DependentTask == doTask.Id).Select(d => new TaskInList
            {
                Id = d.Id,
                Alias = _dal.Task.Read(d.Id).Alias,
                Status = getStatuesOfTask(_dal.Task.Read(d.Id)!),
                Description = _dal.Task.Read(d.Id)!.Description
            }),
            
            milestone = milestomeInList,
            BaseLineStartDate = new DateTime(),
            StartDate = doTask.Start,
            ForecastDate = doTask.ForecasDate,
            DeadlineDate = doTask.Deadline,
            CompleteDate = doTask.Complete,
            Remarks = doTask.Remarks,
            Deliverables = doTask.Deliverables,
            engineer = GetEngineerInTask( doTask),
            CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
        };
        
    }


}

