namespace BlApi;
public interface ITaskInList
{
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList?, bool>? filter = null);
}