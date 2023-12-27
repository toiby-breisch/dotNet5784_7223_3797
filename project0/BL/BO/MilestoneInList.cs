namespace BO;
public class MilestoneInList
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public required Status Status { get; set; }


    // public override string ToString() => this.ToStringProperty();
}

