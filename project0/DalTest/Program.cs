﻿namespace DalTest;
using Dal;
using DalApi;
using DO;
internal class Program
{
    static readonly IDal s_dal = Factory.Get; //stage 4

    //<summary>
    // main menu
    //<summary>
    static public void Main_Menu(int num)
    {
        do
        {
            switch (num)
            {
                case 0:
                    return;
                case 1:
                    Engineer_Menu();
                    break;
                case 2:
                    dependency_Menu();
                    break;
                case 3:
                    Task_Menu();
                    break;
                default:
                    Console.WriteLine("enter another valua");
                    break;
            }
            Console.WriteLine("Enter your choise");
            int.TryParse(Console.ReadLine()!, out num);

        } while (true);
    }
    static void Main()
    {
        try
        {
            Initialization.Do(s_dal);
            Console.WriteLine("Enter your choise");
            var numChoise = Console.ReadLine();
            Main_Menu(int.Parse(numChoise!));
        }
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
    }
    //<summary>
    //  Managing the list of engineers
    //</summary>
    static public void Engineer_Menu()
    {
        Console.WriteLine("exit,creat,read,read all,Update,delete");
        Console.WriteLine("Enter your choise");
        var secondNumChoise = Console.ReadLine();
        switch (int.Parse(secondNumChoise!))
        {
            case 0:
                Main_Menu(0);
                break;
            case 1:
                CreatEngineer();
                break;
            case 2:
                ReadEngineer();
                break;
            case 3:
                ReadAllEngineer();
                break;
            case 4:
                updateEngineer();
                break;
            case 5:
                DeleteEngineer();
                break;
            default:
                Console.WriteLine("enter another valua");
                break;
        }

    }

    //<summary>
    //  create an engineer
    //</summary>

    static public void CreatEngineer()
    {
        try
        {
            Console.WriteLine("enter Id,Name,Email,Level,Cost");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            string? _Name = Console.ReadLine()!;
            string? _Email = Console.ReadLine()!;
            EngineerExperience _CopmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine(), out _CopmlexityLevel);
            double _Cost;
            double.TryParse(Console.ReadLine()!, out _Cost);
            DO.Engineer newEngineer = new(_id, _Name, _Email, _CopmlexityLevel, _Cost);
            s_dal!.Engineer.Create(newEngineer);
            Console.WriteLine(newEngineer.Id);

        }
        catch (DalAlreadyExistsException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    //<summary>
    //  read an engineer
    //</summary>
    static public void ReadEngineer()
    {
        int _id;
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal!.Engineer.Read(_id));
    }

    //<summary>
    //  read all the list of engineers
    //</summary>
    static public void ReadAllEngineer()
    {
        s_dal!.Engineer.ReadAll(ele=>ele.Id>0).ToList().ForEach(
         engineer => Console.WriteLine(engineer)
     );

    }
    //summary>
    //update an engineer
   // </summary>
    static public void updateEngineer()
    {
        try
        {
            Console.WriteLine("enter id");
            int _id;
            int.TryParse(Console.ReadLine(), out _id);
            Console.WriteLine(s_dal!.Engineer.Read(_id));
            Console.WriteLine("enter ,Name,Email,Level,Cost");
            string? _Name = Console.ReadLine()!;
            string? _Email = Console.ReadLine()!;
            EngineerExperience _CopmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine(), out _CopmlexityLevel);
            double _Cost;
            double.TryParse(Console.ReadLine()!, out _Cost);
            DO.Engineer ?temp =s_dal.Engineer.Read(_id);
            if (_Name =="") { _Name = temp!.Name; }
            if (_Email =="") { _Email = temp!.Email; }
            if (_CopmlexityLevel == 0) { _CopmlexityLevel = temp!.Level; }
            if (_Cost == 0) { _Cost = temp!.Cost; }
            DO.Engineer newEngineer = new(_id, _Name, _Email, _CopmlexityLevel, _Cost);
            s_dal!.Engineer.Update(newEngineer);

        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    //<summary>
    //  delete an engineer
    //</summary>
    static public void DeleteEngineer()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter engineer's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal!.Engineer.Delete(_id);
        }
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }
    }
    //<summary>
    //  Managing the list of tasks
    //</summary>

    static public void Task_Menu()
    {
        Console.WriteLine("exit,creat,read,read all,Update,delete");
        Console.WriteLine("Enter your choise");
        var secondNumChoise = Console.ReadLine();
        switch (int.Parse(secondNumChoise!))
        {
            case 0:
                Main_Menu(0);
                break;
            case 1:
                creatTask();
                break;
            case 2:
                readTask();
                break;
            case 3:
                readAllTask();
                break;
            case 4:
                UpdateTask();
                break;
            case 5:
                deleteTask();
                break;

        }
    }
    ///<summary> update a task</summary>
    static public void UpdateTask()
    {
        try
        {
            int id;
            Console.WriteLine("enter id");
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine(s_dal!.Task.Read(id));
            DateTime CreatedAt;
            Console.WriteLine("enter Description,Alias,Deadline,Deliverables,Remarks,Engineerid,CopmlexityLevel");
            string? Description = Console.ReadLine();
            string? Alias = Console.ReadLine();
            CreatedAt = DateTime.Now;
            DateTime Deadline;
            DateTime.TryParse(Console.ReadLine()!, out Deadline);
            string? Deliverables = Console.ReadLine();
            string? Remarks = Console.ReadLine();
            int Engineerid;
            Console.WriteLine("enter Engineerid");
            int.TryParse(Console.ReadLine(), out Engineerid);
            DO.Task ?temp = s_dal.Task.Read(id);
            if (s_dal!.Engineer!.Read(Engineerid) == null)
            {
                Engineerid = temp!.Engineerid;
            }
            EngineerExperience CopmlexityLevel;
            Console.WriteLine("enter CopmlexityLevel");
            EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);
            if (Description == "") { Description = temp!.Description; }
            if (Alias == "") { Description = temp!.Alias; }
            if (Deadline == DateTime.MinValue) { Deadline = Convert.ToDateTime( temp!.DeadlineDate); }
            if (Deliverables == "") { Deliverables = temp!.Deliverables; }

            if (Remarks == "") { Remarks = temp!.Remarks; }
            if (Engineerid == 0) { Engineerid = temp!.Engineerid; }
            if (CopmlexityLevel == 0) { CopmlexityLevel = temp!.CopmlexityLevel; }
            DO.Task newTask = new(id, Description!, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel,true);
            s_dal!.Task.Update(newTask);
            Console.WriteLine(newTask.Id);
        }
        
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }
        
    }

    /// <summary> 
    /// create a new task 
    /// </summary>
    static public void creatTask()
    {
        DateTime CreatedAt;
        Console.WriteLine("enter Description,Alias,Deadline,Deliverables,Remarks,Engineerid,CopmlexityLevel");
        string? Description = Console.ReadLine();
        while (Description == null || Description.Length == 0)
        {
              Console.Write("You must Enter Description ");
             Description = Console.ReadLine();
        }
        string? Alias = Console.ReadLine();
        while (Alias == null || Alias.Length == 0)
        {
            Console.Write("You must Enter Alias ");
            Alias = Console.ReadLine();
        }
        CreatedAt = DateTime.Now;
        DateTime Deadline;
        DateTime.TryParse(Console.ReadLine()!, out Deadline);
        string? Deliverables = Console.ReadLine();
        string? Remarks = Console.ReadLine();
        int Engineerid;
        int.TryParse(Console.ReadLine(), out Engineerid);
        EngineerExperience CopmlexityLevel;
        EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);
        DO.Task newTask = new(0, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel,true);
        int id = s_dal!.Task.Create(newTask);
        Console.WriteLine(id);
    }
   

    /// <summary> 
    /// read a task 
    /// </summary>
    static public void readTask()
    {
        int _id;
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal!.Task.Read(_id));
    }
    /// <summary> 
    /// read all tasks 
    /// </summary>
    static public void readAllTask()
    {
        s_dal!.Task.ReadAll(null!).ToList().ForEach(
         task => Console.WriteLine(task)
     );

    }
    /// <summary> 
    /// delete a task 
    /// </summary>
    static public void deleteTask()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter task's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal!.Task.Delete(_id);
        
        }
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }
    }


    //<summary>
    //  Managing the list of dependencies
    //</summary>
    static public void dependency_Menu()
    {
        Console.WriteLine("exit,creat,read,read all,Update,delete");
        Console.WriteLine("Enter your choise");
        var secondNumChoise = Console.ReadLine();
        switch (int.Parse(secondNumChoise!))
        {
            case 0:
                Main_Menu(0);
                break;
            case 1:
                CreatDependency();
                break;
            case 2:
                readDependency();
                break;
            case 3:
                readAllDependency();
                break;
            case 4:
                updateDependency();
                break;
            case 5:
                deleteDependency();
                break;
            default:
                Console.WriteLine("enter another valua");
                break;

        }

    }
    //<summary>
    //  update a dependency
    //</summary>
    static public void updateDependency()
    {
        try
        {
            Console.WriteLine("Enter ID");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine(s_dal!.Dependency.Read(id));
            Console.WriteLine("enter DependentTask,DependsOnTask");
            int DependentTask;
            int.TryParse(Console.ReadLine()!, out DependentTask);
            int DependsOnTask;
            int.TryParse(Console.ReadLine()!, out DependsOnTask);
            DO.Dependency ?temp =s_dal.Dependency.Read(id);
            if(DependentTask == 0&& DependsOnTask == 0)
            { DependentTask = temp!.DependentTask;
                DependsOnTask = temp!.DependsOnTask;
                return;
            }
            if (DependentTask == 0) { DependentTask = temp!.DependentTask; }
            if (DependsOnTask == 0) { DependsOnTask = temp!.DependsOnTask; }
            
            if (s_dal!.Dependency.IsDepend(DependentTask, DependsOnTask))
                Console.WriteLine("Enter another dependency");
            
           
           
            Dependency newDependency = new(id, DependentTask, DependsOnTask);
            s_dal!.Dependency.Update(newDependency);

        }
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }

    }
    //<summary>
    // create a new dependency
    //<summary>

    static public void CreatDependency()
    {
        Console.WriteLine("enter DependentTask,DependsOnTask");
        int DependentTask;
        int.TryParse(Console.ReadLine(), out DependentTask);
        int DependsOnTask;
        int.TryParse(Console.ReadLine(), out DependsOnTask);

        DO.Dependency newDependency = new(0, DependentTask, DependsOnTask);
        s_dal!.Dependency.Create(newDependency);
        Console.WriteLine(newDependency.Id);
    }
    //<summary>
    // read a dependency
    //<summary>
    static public void readDependency()
    {
        int _id;
        Console.WriteLine("Enter dalDependency's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal!.Dependency.Read(_id));
    }
    //<summary>
    // read all the dependencies
    //<summary>
    static public void readAllDependency()
    {
        s_dal!.Dependency.ReadAll(ele => ele.Id > 0).ToList().ForEach(
         dalDependency => Console.WriteLine(dalDependency)
     );

    }
    //<summary>
    // delete dependency
    //<summary>
    static public void deleteDependency()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter s_dalDependency's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal!.Dependency.Delete(_id);

        }
        catch (DalDoesNotExistException EX)
        {
            Console.WriteLine(EX.Message);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }
    }

}


