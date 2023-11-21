using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalXml : IDal
    {
        public IDependency Dependency => throw new NotImplementedException();

        public ITask Task => throw new NotImplementedException();

        public IEngineer Engineer => throw new NotImplementedException();
    }
}
