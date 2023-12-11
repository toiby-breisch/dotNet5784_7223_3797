﻿namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;


internal class Task : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Add(BO.Task item)
    {

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {

        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new DalAlreadyExistsException($"Task with ID={id} does Not exist");
        return new BO.Task()
        {
            Id = id,
            Description = doTask.Description,
            Alias = doTask.Alias,

           // CreatedAt = doTask.CreatedAt,
           // IsActive = doTask.IsActive,
            
           // Description,
           // string ? Alias,
           //// bool Milestone,????
           // DateTime CreatedAt,
           // DateTime ? Start,
           // DateTime ? ForecasDate,
           // DateTime ? Deadline,
           // DateTime ? Complete,
           // string ? Deliverables,
           // string ? Remarks,
           // int Engineerid,
           // EngineerExperience CopmlexityLevel

        };
    }

    public IEnumerable<TaskInList> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}