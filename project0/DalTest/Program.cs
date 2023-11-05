using DO;
using DalList;
namespace DalTest;
using DalApi;
using System;


internal class Program
{
    static readonly IDal s_dal = new Dal.DalList(); //stage 2

    //<summary>
    //  Managing the list of tasks
    //</summary>
    static public void task_Menu()
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
                updateTask();
                break;
            case 5:
                deleteTask();
                break;

        }
    }
    ///<summary> update a task</summary>
    static public void updateTask()
    {
        try
        {
            int id;
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
            do
            {
                Console.WriteLine("enter Engineerid");
                int.TryParse(Console.ReadLine(), out Engineerid);
            }
            while (s_dal!.Engineer!.Read(Engineerid) == null);
            EngineerExperience CopmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);
            DO.Task newTask = new(id, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel);
            s_dal!.Task.Update(newTask);
            Console.WriteLine(newTask.Id);
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
        string? Alias = Console.ReadLine();
        CreatedAt = DateTime.Now;
        DateTime Deadline;
        DateTime.TryParse(Console.ReadLine()!, out Deadline);
        string? Deliverables = Console.ReadLine();
        string? Remarks = Console.ReadLine();
        int Engineerid;
        int.TryParse(Console.ReadLine(), out Engineerid);
        EngineerExperience CopmlexityLevel;
        EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);
        DO.Task newTask = new(0, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel);
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
        s_dal!.Task.ReadAll().ForEach(
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
        catch (Exception EX)
        {
            Console.WriteLine(EX.Message);
        }
    }
    //<summary>
    //  Managing the list of engineers
    //</summary>
    static public void engineer_Menu()
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
                creatEngineer();
                break;
            case 2:
                readEngineer();
                break;
            case 3:
                readAllEngineer();
                break;
            case 4:
                updateEngineer();
                break;
            case 5:
                deleteEngineer();
                break;
            default:
                Console.WriteLine("enter another valua");
                break;
        }

    }
    static public void updateEngineer()
    {
        try

        {
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
            DO.Engineer newEngineer = new(_id, _Name, _Email, _CopmlexityLevel, _Cost);
            s_dal!.Engineer.Update(newEngineer);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    //<summary>
    //  create an engineer
    //</summary>

    static public void creatEngineer()
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


    }
    //<summary>
    //  read an engineer
    //</summary>
    static public void readEngineer()
    {
        int _id;
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal!.Engineer.Read(_id));
    }
    //<summary>
    //  read all the list of engineers
    //</summary>
    static public void readAllEngineer()
    {
        s_dal!.Engineer.ReadAll().ForEach(
         engineer => Console.WriteLine(engineer)
     );

    }
    //<summary>
    //  delete an engineer
    //</summary>
    static public void deleteEngineer()
    {

        try
        {
            int _id;
            Console.WriteLine("Enter engineer's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal!.Engineer.Delete(_id);
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
                creatDependency();
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
            if (s_dal!.Dependency.isDepend(DependentTask, DependsOnTask))
                Console.WriteLine("Enter another dependency");
            Dependency newDependency = new(id, DependentTask, DependsOnTask);
            s_dal!.Dependency.Update(newDependency);

        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.ToString());
        }

    }
    //<summary>
    // create a new dependency
    //<summary>

    static public void creatDependency()
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
        s_dal!.Dependency.ReadAll().ForEach(
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
        catch (Exception EX)
        {
            Console.WriteLine(EX.ToString());
        }
    }
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

                    engineer_Menu();
                    break;
                case 2:

                    dependency_Menu();
                    break;
                case 3:
                    task_Menu();
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
        catch (Exception EX)
        {
            Console.WriteLine(EX.ToString());
        }


    }
}


