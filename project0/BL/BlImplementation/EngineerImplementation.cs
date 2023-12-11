namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;


internal class Engineer : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Add(int id, string name, int cost, string email)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<EngineerInList> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}

