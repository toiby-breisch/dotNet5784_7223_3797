namespace BlApi;
public interface ITask
{
    public BO.Task? Read(int id);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.Engineer?, bool> filter);
    public void Update(BO.Engineer item);
    public void Delete(int id);
    public void Add(BO.Task item);

}