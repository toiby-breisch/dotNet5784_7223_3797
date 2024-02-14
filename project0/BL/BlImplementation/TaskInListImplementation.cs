using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class TaskInListImplementation:BlApi.ITaskInList
    {
            private readonly DalApi.IDal _dal = DalApi.Factory.Get;
        /// <summary>
        /// Get Statues Of Task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
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
        /// ReadAll TaskInLis
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList?, bool>? filter = null)
        {

            IEnumerable<BO.TaskInList> allTasks = from task in _dal.Task.ReadAll()
                                            select new BO.TaskInList
                                            {
                                                Id = task.Id,
                                                Description = task.Description!,
                                                Alias = task!.Alias,
                                               Status = GetStatuesOfTask(task),
                                               
                                            };
            return filter == null ? allTasks : allTasks.Where(filter);

        }

    }
}
