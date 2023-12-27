

using DO;

namespace BO;

public class EngineerInList
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double Cost { get; set; }
    public EngineerExperience Level { get; set; }
    public TaskInEngineer? Task { get; set; }
}
