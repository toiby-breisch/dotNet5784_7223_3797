using DO;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace BO;
public class Task
{
     public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public required Status status { get; set; }
    public IEnumerable<TaskInList?>? DependenciesList { get; set; }=new List<TaskInList?>();  
    public MilestoneInTask? Milestone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate  { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string ?Remarks { get; set; }
    public string? Deliverables { get; set; }
    public EngineerInTask?Engineer { get; set; }
    public EngineerExperience? CopmlexityLevel { get; set; }

    public override string ToString() => Tools.ToStringProperty(this);
  
}
