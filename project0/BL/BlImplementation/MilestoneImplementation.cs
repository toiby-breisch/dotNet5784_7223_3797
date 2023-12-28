namespace BlImplementation;
using BlApi;
using DalApi;
using System.Data.SqlTypes;

internal class Milestone : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private  BO.Status getStatusFromDo(DO.Task taskMilestone)
    {
        return (BO.Status)1;
    }
    public BO.Milestone? Read(int id)
    {
        try {
            ///depenontask או השני?
            IEnumerable <BO.TaskInList> dependencies = _dal.Dependency.ReadAll(d=>d.DependsOnTask==id).select new TaskInList
            {

            }
                );
        ///צריך לבדוק האם milestone =true?
        DO.Task taskMilestone = _dal.Task.Read(id)!;
        new BO.Milestone milestone(taskMilestone.Id, taskMilestone.Description, taskMilestone.Alias,
           taskMilestone.CreatedAt, getStatusFromDo(taskMilestone), taskMilestone.ForecasDate, taskMilestone.Deadline,
           taskMilestone.Complete, taskMilestone.Remarks,2,);
        //public double CompletionPercentage { get; set; }
        //public required TaskInList Task { get; set; }
        }
        catch { throw new Exception(); };
    }

    public BO.Milestone? Update(int id)
    {
        throw new NotImplementedException();
    }
}
