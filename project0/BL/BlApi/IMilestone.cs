namespace BlApi;
public interface IMilestone
{
    //לוז להוסיף וכן לסדרnullable init
    public BO.Milestone? Read(int id);
    public BO.Milestone? Update(int id);
    
}
