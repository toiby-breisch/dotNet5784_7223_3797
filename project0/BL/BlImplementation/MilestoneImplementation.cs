namespace BlImplementation;

using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Data.SqlTypes;

internal class MilestoneImplementation : BlApi.IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private void CreatingTheProjectSchedule()
    {
        var tasks = from task in _dal.Task.ReadAll(null!)
                    select new DO.Task
                    {
                        Alias = task.Alias,
                        Id=task.Id,
                        Description=task.Description,
                        Milestone=true,
                        CreatedAt=task.CreatedAt,
                        StartDate=task.CreatedAt,
                         scheduledDate=task.CreatedAt,
                         DeadlineDate=task.CreatedAt,
                        CompleteDate=task.CompleteDate,
                         Deliverables=task.Deliverables,
                         Remarks=task.Remarks,
                         Engineerid=task.Engineerid,
                         CopmlexityLevel=task.CopmlexityLevel,
                        IsActive=true

                    };
        new 

                    _dal.Dependency.ReadAll(dep => dep!.DependsOnTask == task.Id)


    }
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
    private double getCompletionPercentage(IEnumerable<BO.TaskInList> dependencies)
    {
        var sumOfDependenciesTask = dependencies.Sum(task => task.Status == BO.Status.Unscheduled ? 1 : 0);
        return (dependencies.Count() / sumOfDependenciesTask) * 100;
    }
    private IEnumerable<BO.TaskInList> getDependeciesOfMilistone(int id)
    {
        //לא צריך לעשות TRY כי אין בDAL?
            IEnumerable<BO.TaskInList> dependencies = from dependency in _dal.Dependency.ReadAll(dep => dep!.DependentTask == id)
                                                      let task = _dal.Task.Read(dependency.Id)
                                                      select new BO.TaskInList
                                                      {
                                                          Id = task.Id,
                                                          Alias = task.Alias,
                                                          Description = task.Description,
                                                          Status = getStatuesOfTask(task)
                                                      };
            return dependencies;

  
    }
    public BO.Milestone? Read(int id)
    {
        try
        {
            //forcase???                 
            ///צריך לבדוק האם milestone =true? 
            DO.Task taskMilestone = _dal.Task.Read(id)!;
            return new BO.Milestone
            {
                Id = taskMilestone.Id,
                Description = taskMilestone.Description,
                Alias = taskMilestone.Alias,
                CreatedAtDate = taskMilestone.CreatedAt,
                Status = getStatuesOfTask(taskMilestone),
                DeadlineDate = taskMilestone.DeadlineDate,
                CompleteDate = taskMilestone.CompleteDate,
                Remarks = taskMilestone.Remarks,
                CompletionPercentage = getCompletionPercentage(getDependeciesOfMilistone(id)),
                Dependencies = getDependeciesOfMilistone(id).ToList(),
                ForecastDate = null
            };
        }
        catch(DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Milistone with ID={id} is not exists");
        };
    }

    public BO.Milestone? Update(BO.Task task)
    {
        if (task.Alias == "" || task.Description == "")
            throw new Exception();
        try
        {
            //לבדוק אם זה אבן דרך? אפה מקבלים את הערכים?
            DO.Task taskMilestone = _dal.Task.Read(task.Id)!;
            DO.Task updateMilistone= taskMilestone with { Alias=task.Alias,Description=task.Description,Remarks=task.Remarks };
            _dal.Task.Update(updateMilistone);
            return new BO.Milestone
            {
                Id = updateMilistone.Id,
                Description = updateMilistone.Description,
                Alias = updateMilistone.Alias,
                CreatedAtDate = updateMilistone.CreatedAt,
                Status = getStatuesOfTask(updateMilistone),
                DeadlineDate = updateMilistone.DeadlineDate,
                CompleteDate = updateMilistone.CompleteDate,
                Remarks = updateMilistone.Remarks,
                CompletionPercentage = getCompletionPercentage(getDependeciesOfMilistone(updateMilistone.Id)),
                Dependencies = getDependeciesOfMilistone(updateMilistone.Id).ToList(),
                ForecastDate = null
            };
        }
        catch(DO.DalDoesNotExistException ) 
        {
            throw new BO.BlDoesNotExistException($"Milistone with ID={task.Id} is not exists");
        };
    }
}
