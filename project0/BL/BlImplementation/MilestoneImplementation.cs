namespace BlImplementation;
using BlApi;
using DalApi;

internal class Milestone : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public BO.Milestone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Milestone? Update(int id)
    {
        throw new NotImplementedException();
    }
}
