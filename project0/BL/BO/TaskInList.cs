namespace BO;
public class TaskInList
{
    public int Id { get; init; }
    public string ?Description { get; init; }
    public string? Alias { get; set; }
    public Status? Status { get; set; }

    // public override string ToString() => this.ToStringProperty();
}

