namespace BlApi;
public interface ITask
{
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task?, bool>? filter=null);
    public void Update(BO.Task item);
    public void Delete(int id);
    public int create(BO.Task item);

}