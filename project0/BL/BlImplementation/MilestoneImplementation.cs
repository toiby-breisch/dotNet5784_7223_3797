namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System.Collections.Generic;
using System.Data.SqlTypes;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.Status getStatusFromDo(DO.Task taskMilestone)
    {
        return (BO.Status)1;
    }
    ///לבדוק באיזה תנאי הוא יספור לאיזה enum
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
                                                          Status = getStatusFromDo(task)
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
                Status = getStatusFromDo(taskMilestone),
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
                Status = getStatusFromDo(updateMilistone),
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
