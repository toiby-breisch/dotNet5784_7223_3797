﻿namespace BO;
public class TaskInList
{
    //public TaskInList(int id, string description, string alias, Status status)
    //{
    //    Id = id;
    //    Description = description;
    //    Alias = alias;
    //    Status = status;
    //}

    public int Id { get; init; }
    public required string Description { get; init; }
    public required string Alias { get; set; }
    public  Status Status { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
    // public override string ToString() => this.ToStringProperty();
}

