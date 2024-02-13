using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class EngineerInListImplementation : BlApi.IEngineerInList
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public IEnumerable<BO.EngineerInList> ReadAll(Func<BO.EngineerInList?, bool>? filter = null)
        {

            IEnumerable<BO.EngineerInList> allTasks = from doEngineer in _dal.Engineer.ReadAll()
                                                select new BO.EngineerInList
                                                {
                                                    Id = doEngineer.Id,
                                                    Name = doEngineer.Name,
                                                    Level = (BO.EngineerExperience)doEngineer.Level,

                                                };
            return filter == null ? allTasks : allTasks.Where(filter);

        }
    }
}
