﻿namespace BO;
/// <summary>
/// Engineer In List
/// </summary>
public class EngineerInTask
{
    public EngineerInTask(int engineerid, string name)
    {
        this.Id = engineerid;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
