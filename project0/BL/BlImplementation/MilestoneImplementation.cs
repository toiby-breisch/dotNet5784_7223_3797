namespace BlImplementation;
using BlApi;
using BO;
using DalApi;
using System.Collections.Generic;
using System.Data.SqlTypes;

internal class Milestone : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private  BO.Status getStatusFromDo(DO.Task taskMilestone)
    {
        return (BO.Status)1;
    }
    ///לבדוק באיזה תנאי הוא יספור לאיזה enum
    private double getCompletionPercentage(IEnumerable<BO.TaskInList> dependencies) { 
        var sumOfDependenciesTask = dependencies.Sum(task => task.Status == BO.Status.Unscheduled ? 1 : 0);
       return (dependencies.Count() / sumOfDependenciesTask) *100;
    }
    public BO.Milestone? Read(int id)
    {
        try
        {
            IEnumerable<BO.TaskInList> dependencies = from dependency in _dal.Dependency.ReadAll(dep => dep!.DependentTask == id)
                                                      let task = _dal.Task.Read(dependency.Id)
                                                      select new BO.TaskInList
                                                      {
                                                          Id = task.Id,
                                                          Alias = task.Alias,
                                                          Description = task.Description,
                                                          Status = getStatusFromDo(task)
                                                      };
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
                CompletionPercentage = getCompletionPercentage(dependencies),
                Dependencies = dependencies.ToList(),
                ForecastDate = null
            };
        }
        catch
        {
            throw new Exception();
        };
    }

    public BO.Milestone? Update(int id)
    {
        throw new NotImplementedException();
    }
}
