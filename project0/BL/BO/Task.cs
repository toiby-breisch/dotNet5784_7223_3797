using DO;

namespace BO;
public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string ?Alias { get; set; }
    public Status? Status { get; set; }
    public TaskInList ?TaskInList { get; set; }
    public DateTime BaseLineStartDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ScheduledStartDate { get; set; }
    public DateTime ForecastDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ?Remarks { get; set; }
    public string? product { get; set; }
    public EngineerInTask?engineer { get; set; }
    public Milestone? relatedMilestone { get; set; }
    public EngineerExperience? CopmlexityLevel { get; set; }
    public bool IsActive { get; set; }
    // public override string ToString() => this.ToStringProperty();
}
