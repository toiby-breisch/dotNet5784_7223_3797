namespace BlApi;
public interface IEngineer
{
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.EngineerInList> ReadAll();
     public void Update(BO.Engineer item);
    public void Delete(int id);
    public int Add(BO.Engineer boEngineer);

}