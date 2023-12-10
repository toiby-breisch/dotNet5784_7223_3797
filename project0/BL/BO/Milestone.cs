﻿using DO;

namespace BO;
public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string ?Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status? Status { get; set; }
    public DateTime ForecastDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ?Remarks { get; set; }
    public double CompletionPercentage { get; set; }
    public TaskInList? Task { get; set; }   
    //  public override string ToString() => this.ToStringProperty();
}
