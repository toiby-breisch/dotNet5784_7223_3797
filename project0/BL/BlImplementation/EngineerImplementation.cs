﻿namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;

internal class Engineer : IEngineer
{
    private EngineerInTask? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
    DalApi.IDal _dal = DalApi.Factory.Get;
    var tasks = _dal.Task.ReadAll(null!);
    EngineerInTask engineerInTask =
        (from task in tasks
         where (task.Id == idOfEngineer&& _Dal.Engineers.Read(idOfEngineer).IsActive == true)
         select new
         {
             Id = task.Id,
             Name = task.Description,
         }).FirstOrDefault();
     return engineerInTask;
       }


    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Add(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name!, boEngineer.Email, boEngineer.Level, boEngineer.Cost, boEngineer.IsActive);
        if (boEngineer?.Id <=0)
        {
            //throw new BO.BlAlreadyExistsException();
        }
        if(boEngineer?.Email == "")
        {
            //throw new BO.BlAlreadyExistsException();
        }
        if(boEngineer?.Name =="")
        {
           // throw new BO.BlAlreadyExistsException();
        }
        if(boEngineer?.Cost <= 0)
        {
            //throw new BO.BlAlreadyExistsException();
        }
        if (Read(boEngineer!.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists");
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
          //  throw new BO.BlAlreadyExistsException($"boEngineer with ID={boEngineer?.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer?.Id == null)
            throw new DalAlreadyExistsException($"Student with ID={id} does Not exist");
        //תבדוק שהמהנדס לא בביצוע של משימה
        //אי אפשר למחוק מהנדס שכבר סיים לבצע משימה או נמצא בביצוע פעיל של משימה
        //:לבדוק 
        Delete(id);
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new DalAlreadyExistsException($"Student with ID={id} does Not exist");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = doEngineer.Level,
            Cost = doEngineer.Cost,
            IsActive = doEngineer.IsActive,
           
        };
    }

    public IEnumerable<EngineerInList> ReadAll() => (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(null!)
                                                     select new BO.EngineerInList
                                                     {
                                                         Id = doEngineer.Id,
                                                         Email = doEngineer.Email,
                                                         Level = doEngineer.Level,
                                                         Cost = doEngineer.Cost,
                                                         Task = GetCurrentTaskOfEngineer(doEngineer.Id),
                                                     });

    public void Update(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name!, boEngineer.Email, boEngineer.Level, boEngineer.Cost, boEngineer.IsActive);
        if (boEngineer?.Id <= 0)
        {
            throw new DalAlreadyExistsException("");
        }
        if (boEngineer?.Email == "")
        {
            throw new DalAlreadyExistsException("");
        }
        if (boEngineer?.Name == "")
        {
             throw new DalAlreadyExistsException("");
        }
        if (boEngineer?.Cost <= 0)
        {
            throw new DalAlreadyExistsException("");
        }
        if (Read(boEngineer!.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists");
        Update(boEngineer);
    }
 
}

