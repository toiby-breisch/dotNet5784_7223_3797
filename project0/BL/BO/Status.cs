namespace BO;
public class Status
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public string? Alias { get; set; }
    public bool IsActive { get; set; }
    DateTime? BirthDate { get; set; }
    public DateTime RegistrationDate { get; init; }
   
    public override string ToString() => this.ToStringProperty();
}
