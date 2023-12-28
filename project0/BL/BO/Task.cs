using DO;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace BO;
public class Task
{
      public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public required Status status { get; set; }
    public IEnumerable<TaskInList?> DependenciesList { get; set; }=new List<TaskInList?>();  
    public MilestoneInTask? milestone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? scheduledDate  { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string ?Remarks { get; set; }
    public string? Deliverables { get; set; }
    public EngineerInTask?engineer { get; set; }
    public EngineerExperience? CopmlexityLevel { get; set; }
    //public Task(Task task)
    //{
   
    //}

    public Task()
    {
    }
    // public override string ToString() => this.ToStringProperty();
}
