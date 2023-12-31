//// See https://aka.ms/new-console-template for more information

    namespace DalTest;

using BO;
using Dal;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

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
    static public void Main()
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            // DalTest.Initialization.Do();
            Console.WriteLine("jumk");
        Console.WriteLine("Enter your choise:1:engineer_Menu, 2:milstone_Menu, 3:task_Menu");
        var numChoise = Console.ReadLine();
        Main_Menu(int.Parse(numChoise!));
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

    //<summary>
    //  create an engineer
    //</summary>

    static public void creatEngineer()
    {
        try
        {
            Console.WriteLine("enter Id,Name,Email,Level,Cost,current task");
            int _id, current_task;
            string current_task_alyas;
            int.TryParse(Console.ReadLine()!, out _id);
            string? _Name = Console.ReadLine()!;
            string? _Email = Console.ReadLine()!;
            BO. EngineerExperience _CopmlexityLevel;
            BO. EngineerExperience.TryParse(Console.ReadLine(), out _CopmlexityLevel);
            double _Cost;
            double.TryParse(Console.ReadLine()!, out _Cost);
            int.TryParse(Console.ReadLine(), out current_task);
            try
            {
                current_task_alyas = s_bl.Task.Read(_id)!.Alias;
            }
            catch (BO.BlDoesNotExistException Ex){ throw Ex; }
            BO.TaskInEngineer taskInEngineer = new BO.TaskInEngineer { Id = current_task, Alias = current_task_alyas };
            BO.Engineer newEngineer = new BO.Engineer {Id= _id,Name= _Name,Email= _Email,Level= _CopmlexityLevel,Cost= _Cost,CurrentTask= taskInEngineer };
            try
            {
                s_bl!.Engineer.create(newEngineer);
            }
            catch (BO.BlAlreadyExistsException Ex){ throw Ex; }
           
             
            Console.WriteLine(newEngineer.Id);

        }
      
        catch (Exception Ex)
        {
            Console.WriteLine(Ex);
        }
    }

    //<summary>
    //  read an engineer
    //</summary>
    static public void readEngineer()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter engineer's ID");
            int.TryParse(Console.ReadLine()!, out _id);
            Console.WriteLine(s_bl!.Engineer.Read(_id));
        }
        catch (BO.BlDoesNotExistException Ex) {  throw Ex; }
    }

    //<summary>
    //  read all the list of engineers
    //</summary>
    static public void readAllEngineer()
    {

        s_bl!.Engineer.ReadAll(ele => ele!.Id > 0).ToList().ForEach(
         engineer => Console.WriteLine(engineer) );

    }
    //summary>
    //update an engineer
    // </summary>
    static public void updateEngineer()
    {
        try
        {
            Console.WriteLine("enter id");
            int _id, current_task;
            BO.Engineer? temp;
            int.TryParse(Console.ReadLine(), out _id);
            Console.WriteLine(s_bl!.Engineer.Read(_id));
            Console.WriteLine("enter ,Name,Email,Level,Cost,current task");
            string? _Name = Console.ReadLine()!;
            string? _Email = Console.ReadLine()!;
            BO.EngineerExperience _CopmlexityLevel;
            BO.EngineerExperience.TryParse(Console.ReadLine(), out _CopmlexityLevel);
            double _Cost;
            double.TryParse(Console.ReadLine()!, out _Cost);
            int.TryParse(Console.ReadLine(), out current_task);
            try
            {
                temp = s_bl.Engineer.Read(_id);
            }
            catch (BO.BlDoesNotExistException Ex) { throw Ex; }
            if (_Name == "") { _Name = temp!.Name; }
            if (_Email == "") { _Email = temp!.Email; }
            if (_CopmlexityLevel == 0) { _CopmlexityLevel = temp!.Level; }
            if (_Cost == 0) { _Cost = temp!.Cost; }
            BO.TaskInEngineer? taskInEngineer;
            string current_task_alyas;
            try
            {
                current_task_alyas = s_bl.Task.Read(_id)!.Alias;
                taskInEngineer = new TaskInEngineer { Id = _id, Alias = current_task_alyas };
            }
            catch (BO.BlDoesNotExistException Ex) { taskInEngineer = temp!.CurrentTask; }
            BO.Engineer newEngineer = new BO.Engineer { Id = _id, Name = _Name, Email = _Email, Level = _CopmlexityLevel,
                Cost = _Cost, CurrentTask = taskInEngineer };
            s_bl!.Engineer.Update(newEngineer);
        }
        catch (BO.BlDoesNotExistException e)
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
    static public void deleteEngineer()
    {

        try
        {
            int _id;
            Console.WriteLine("Enter engineer's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_bl!.Engineer.Delete(_id);
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
    //         public int Id { get; init; }
    //public required string Description { get; set; }
    //public required string Alias { get; set; }
    //public DateTime CreatedAtDate { get; set; }
    //public required Status status { get; set; }
    //public IEnumerable<TaskInList?> DependenciesList { get; set; } = new List<TaskInList?>();
    //public MilestoneInTask? milestone { get; set; }
    //public DateTime? StartDate { get; set; }
    //public DateTime? scheduledDate { get; set; }
    //public DateTime? ForecastDate { get; set; }
    //public DateTime? DeadlineDate { get; set; }
    //public DateTime? CompleteDate { get; set; }
    //public string? Remarks { get; set; }
    //public string? Deliverables { get; set; }
    //public EngineerInTask? engineer { get; set; }
    //public EngineerExperience? CopmlexityLevel { get; set; }
        try
        {
            int id;
            Console.WriteLine("enter id");//
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine(s_bl!.Task.Read(id));
            DateTime CreatedAt;
            Console.WriteLine("enter Description,Alias,Deadline,Deliverables,Remarks,Engineerid,CopmlexityLevel");
            string? Description = Console.ReadLine();//
            string? Alias = Console.ReadLine();//
            CreatedAt = DateTime.Now;//
            DateTime Deadline;
            DateTime.TryParse(Console.ReadLine()!, out Deadline);//
            string? Deliverables = Console.ReadLine();//
            string? Remarks = Console.ReadLine();//
            int Engineerid;
            Console.WriteLine("enter Engineerid");
            int.TryParse(Console.ReadLine(), out Engineerid);//
            BO.Task? temp = s_bl.Task.Read(id);
            BO.EngineerInTask? EngineerInTask;
            string Engineer_Name;
            try
            {
                Engineer_Name = s_bl.Engineer.Read(id)!.Name;
                EngineerInTask = new EngineerInTask { Id = id, Name = Engineer_Name };
            }
            //catch (BO.BlDoesNotExistException Ex) { taskInEngineer = temp!.CurrentTask; }
            //BO.Engineer newEngineer = new BO.Engineer
            //{
            //    Id = _id,
            //    Name = _Name,
            //    Email = _Email,
            //    Level = _CopmlexityLevel,
            //    Cost = _Cost,
            //    CurrentTask = taskInEngineer
            //};
            if (s_bl!.Engineer!.Read(Engineerid) == null)
            {
                Engineerid = temp!.engineer;
            }
            BO.EngineerExperience CopmlexityLevel;
            Console.WriteLine("enter CopmlexityLevel");
            BO.EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);//
            if (Description == "") { Description = temp!.Description; }
            if (Alias == "") { Description = temp!.Alias; }
            if (Deadline == DateTime.MinValue) { Deadline = Convert.ToDateTime(temp!.DeadlineDate); }
            if (Deliverables == "") { Deliverables = temp!.Deliverables; }

            if (Remarks == "") { Remarks = temp!.Remarks; }
            if (Engineerid == 0) { Engineerid = temp!.; }
            if (CopmlexityLevel == 0) { CopmlexityLevel = temp!.CopmlexityLevel; }
            DO.Task newTask = new(id, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel, true);
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
        BO.EngineerExperience CopmlexityLevel;
       BO.EngineerExperience.TryParse(Console.ReadLine(), out CopmlexityLevel);
        DO.Task newTask = new(0, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel, true);
        int id = s_bl!.Task.Create(newTask);
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
                creatMilstone();
                break;
            case 2:
                readMilstone();
                break;
            case 3:
                readAllMilstone();
                break;
            case 4:
                updateMilstone();
                break;
            case 5:
                deleteMilstone();
                break;
            default:
                Console.WriteLine("enter another valua");
                break;

        }

    }
    //<summary>
    //  update a dependency
    //</summary>
    static public void updateMilstone()
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
            DO.Dependency? temp = s_dal.Dependency.Read(id);
            if (DependentTask == 0 && DependsOnTask == 0)
            {
                DependentTask = temp!.DependentTask;
                DependsOnTask = temp!.DependsOnTask;
                return;
            }
            if (DependentTask == 0) { DependentTask = temp!.DependentTask; }
            if (DependsOnTask == 0) { DependsOnTask = temp!.DependsOnTask; }

            if (s_dal!.Dependency.isDepend(DependentTask, DependsOnTask))
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

    static public void creatMilstone()
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
    static public void readMilstone()
    {
        int _id;
        Console.WriteLine("Enter dalDependency's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal!.Dependency.Read(_id));
    }
    //<summary>
    // read all the dependencies
    //<summary>
    static public void readAllMilstone()
    {
        s_dal!.Dependency.ReadAll(ele => ele.Id > 0).ToList().ForEach(
         dalDependency => Console.WriteLine(dalDependency)
     );

    }
    //<summary>
    // delete dependency
    //<summary>
    static public void deleteMilstone()
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


