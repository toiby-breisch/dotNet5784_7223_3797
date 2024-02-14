namespace BO;
public class TaskInList
{
    public int Id { get; init; }
    public required string Description { get; init; }
    public required string Alias { get; set; }
    public Status Status { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}

