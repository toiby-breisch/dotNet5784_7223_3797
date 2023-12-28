﻿using DO;

namespace BO;
public class Milestone
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public required DateTime CreatedAtDate { get; set; }
    public required Status  Status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string ?Remarks { get; set; }
    public double CompletionPercentage { get; set; }
    public required TaskInList Dependencies { get; set; }   
    //  public override string ToString() => this.ToStringProperty();
}
