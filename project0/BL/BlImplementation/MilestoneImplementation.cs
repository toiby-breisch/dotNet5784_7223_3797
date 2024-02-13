//namespace BlImplementation;
//using System.Collections.Generic;
//using System.Data.SqlTypes;

///// <summary>
///// Milistone implementation.
///// 
///// </summary>
//internal class MilestoneImplementation : BlApi.IMilestone
//{
//    private DalApi.IDal _dal = DalApi.Factory.Get;
//    //private void CreatingTheProjectSchedule()
//    //{
//    //    IEnumerable<DO.Dependency> dependencies = _dal.Dependency.ReadAll()!;
//    //    var groupBy = dependencies.GroupBy(dependency => dependency!.DependsOnTask, (dependencyOnTask, dependenciesTasks) => new
//    //    {
//    //        key = dependencyOnTask,
//    //        dependencies = dependenciesTasks
//    //    }).Order();
//    //    var groupByDistinct = groupBy.Distinct();
//    //    var createMilistones = from milistone in groupByDistinct
//    //                           let dependencyMilistone = _dal.Task.Read(milistone.key)
//    //                           select new BO.Milestone
//    //                           {

//    //                           }


//    //   //if (dependencyMilistone.) { }
//    //   // Alias = 'M' + dependencyMilistone.Alias;
//    //   // Alias = dependencyMilistone.Alias,

//    //}

//    /// <summary>
//    /// The function conculate the status's value.
//    /// </summary>
//    /// <param name="task"></param>
//    /// <returns> BO.Status</returns>
//    private BO.Status getStatuesOfTask(DO.Task task)
//    {
//        DateTime now = DateTime.Now;
//        if (task.scheduledDate == DateTime.MinValue)
//            return BO.Status.Unscheduled;
//        else if (task.StartDate == DateTime.MinValue)
//            return BO.Status.Scheduled;
//        else if (task.DeadlineDate < now && task.CompleteDate == DateTime.MinValue)
//            return BO.Status.InJeopardy;
//        else return BO.Status.OnTrack;
//    }

//    /// <summary>
//    /// The function get CompletionPercentage of Milistone
//    /// </summary>
    
//    private double getCompletionPercentage(IEnumerable<BO.TaskInList> dependencies)
//    {
//        var sumOfDependenciesTask = dependencies.Sum(task => task.Status == BO.Status.Unscheduled ? 1 : 0);
//        return (dependencies.Count() / sumOfDependenciesTask) * 100;
//    }
//    private IEnumerable<BO.TaskInList> getDependeciesOfMilistone(int id)
//    {
        
//            IEnumerable<BO.TaskInList> dependencies = from dependency in _dal.Dependency.ReadAll(dep => dep!.DependentTask == id)
//                                                      let task = _dal.Task.Read(dependency.Id)
//                                                      select new BO.TaskInList
//                                                      {
//                                                          Id = task.Id,
//                                                          Alias = task.Alias,
//                                                          Description = task.Description,
//                                                          Status = getStatuesOfTask(task)
//                                                      };
//            return dependencies;

  
//    }

//    /// <summary>
//    /// The function get a milistone's id and return the BO.milistone object.
//    /// </summary>
  
//    public BO.Milestone? Read(int id)
//    {
//        try
//        {
//            //forcase???                 
//            ///צריך לבדוק האם Milestone =true? 
//            DO.Task taskMilestone = _dal.Task.Read(id)!;
//            return new BO.Milestone
//            {
//                Id = taskMilestone.Id,
//                Description = taskMilestone.Description,
//                Alias = taskMilestone.Alias,
//                CreatedAtDate = taskMilestone.CreatedAt,
//                Status = getStatuesOfTask(taskMilestone),
//                DeadlineDate = taskMilestone.DeadlineDate,
//                CompleteDate = taskMilestone.CompleteDate,
//                Remarks = taskMilestone.Remarks,
//                CompletionPercentage = getCompletionPercentage(getDependeciesOfMilistone(id)),
//                Dependencies = getDependeciesOfMilistone(id).ToList(),
//                ForecastDate = null
//            };
//        }
//        catch(DO.DalDoesNotExistException ex)
//        {
//            throw new BO.BlDoesNotExistException($"Milistone with ID={id} is not exists",ex);
//        };
//    }

//    /// <summary>
//    /// The function get a miliston object and update this milistone.
//    /// </summary>
//    public BO.Milestone? Update(BO.Task task)
//    {
//        if (task.Alias == "" || task.Description == "")
//            throw  new BO.BlNullOrNotIllegalPropertyException($"Milistone with ID={task.Id}lacks values");
//        try
//        {
//            DO.Task taskMilestone = _dal.Task.Read(task.Id)!;
//            DO.Task updateMilistone= taskMilestone with { Alias=task.Alias,Description=task.Description,Remarks=task.Remarks };
//            _dal.Task.Update(updateMilistone);
//            return new BO.Milestone
//            {
//                Id = updateMilistone.Id,
//                Description = updateMilistone.Description,
//                Alias = updateMilistone.Alias,
//                CreatedAtDate = updateMilistone.CreatedAt,
//                Status = getStatuesOfTask(updateMilistone),
//                DeadlineDate = updateMilistone.DeadlineDate,
//                CompleteDate = updateMilistone.CompleteDate,
//                Remarks = updateMilistone.Remarks,
//                CompletionPercentage = getCompletionPercentage(getDependeciesOfMilistone(updateMilistone.Id)),
//                Dependencies = getDependeciesOfMilistone(updateMilistone.Id).ToList(),
//                ForecastDate = null
//            };
//        }
//        catch(DO.DalDoesNotExistException ex) 
//        {
//            throw new BO.BlDoesNotExistException($"Milistone with ID={task.Id} is not exists-can't update",ex);
//        };
//    }
//}
