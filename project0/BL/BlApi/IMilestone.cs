﻿namespace BlApi;
public interface IMilestone
{
    public BO.Milestone? Read(int id);
    public BO.Milestone? Update(BO.Task task);
    
}
