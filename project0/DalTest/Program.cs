using DO;
using Dal;
namespace DalTest;
using DalApi;
using System;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

internal class program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerIementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();
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
    static public void updateTask()
    {
        try
        {
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine(s_dalTask!.Read(id));
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
            DO.Task newTask = new(id, Description, Alias, false, CreatedAt, null, null, Deadline, null, Deliverables, Remarks, Engineerid, CopmlexityLevel);
            s_dalTask!.Update(newTask);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX/*.ToString()*/);
        }

    }


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
        s_dalTask!.Create(newTask);
        Console.WriteLine(newTask.Id);
    }

    static public void readTask()
    {
        int _id ;
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dalTask!.Read(_id));         
    }
    static public void readAllTask()
    {
        s_dalTask!.ReadAll().ForEach(
         task => Console.WriteLine(task)
     );
        
    }
    
    static public void deleteTask()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter task's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dalTask!.Delete(_id);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX/*.ToString()*/);
        }
    }




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

        }
        
    }
    static public void updateEngineer()
    {
        try

        {
            int _id;
            int.TryParse(Console.ReadLine(), out _id);
            Console.WriteLine(s_dalEngineer!.Read(_id));
            Console.WriteLine("enter ,Name,Email,Level,Cost");
          
            string? _Name = Console.ReadLine()!;
            string? _Email = Console.ReadLine()!;
            EngineerExperience _CopmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine(), out _CopmlexityLevel);
            double _Cost;
            double.TryParse(Console.ReadLine()!, out _Cost);
            DO.Engineer newEngineer = new(_id, _Name, _Email, _CopmlexityLevel, _Cost);
            s_dalEngineer!.Update(newEngineer);

        }
        catch (Exception e)
        {
            Console.WriteLine(e/*.ToString()*/);
        }
  
    }


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
            double _Cost ;
            double.TryParse(Console.ReadLine()!, out _Cost);
            DO.Engineer newEngineer = new(_id, _Name, _Email, _CopmlexityLevel, _Cost);
            s_dalEngineer!.Create(newEngineer);

        }
        catch (Exception e)
        {
             Console.WriteLine(e/*.ToString()*/);
        }
        
       
    }

    static public void readEngineer()
    {
        int _id;
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dalEngineer!.Read(_id));
    }
    static public void readAllEngineer()
    {
        s_dalEngineer!.ReadAll().ForEach(
         engineer => Console.WriteLine(engineer)
     );

    }

    static public void deleteEngineer()
    {
   
        try
        {
            int _id;
            Console.WriteLine("Enter engineer's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dalEngineer!.Delete(_id);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX/*.ToString()*/);
        }
    }

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

        }

    }
    static public void updateDependency()
    {
        try
        {
            int id;
            int.TryParse(Console.ReadLine(), out id);
            Console.WriteLine(s_dalDependency!.Read(id));
            Console.WriteLine("enter DependentTask,DependsOnTask");
            int DependentTask;
            int.TryParse(Console.ReadLine(), out DependentTask);
            int DependsOnTask;
            int.TryParse(Console.ReadLine(), out DependsOnTask);
            Dependency newDependency = new(0, DependentTask, DependsOnTask);
            s_dalDependency!.Update(newDependency);

        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.ToString());
        }

    }


    static public void creatDependency()
    {
        Console.WriteLine("enter DependentTask,DependsOnTask");
        int DependentTask;
        int.TryParse(Console.ReadLine(), out DependentTask);
        int DependsOnTask;
        int.TryParse(Console.ReadLine(), out DependsOnTask);
        DO.Dependency newDependency = new(0, DependentTask, DependsOnTask);
        s_dalDependency!.Create(newDependency);
        Console.WriteLine(newDependency.Id);
    }

    static public void readDependency()
    {
        int _id;
        Console.WriteLine("Enter dalDependency's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dalDependency!.Read(_id));
    }
    static public void readAllDependency()
    {
        s_dalDependency!.ReadAll().ForEach(
         dalDependency => Console.WriteLine(dalDependency)
     );

    }

    static public void deleteDependency()
    {
        try
        {
            int _id;
            Console.WriteLine("Enter s_dalDependency's ID to delete");
            int.TryParse(Console.ReadLine()!, out _id);
            s_dalDependency!.Delete(_id);
        }
        catch (Exception EX)
        {
            Console.WriteLine(EX.ToString());
        }
    }


    static public void Main_Menu(int num)
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

        }
    }
    
    static void Main()
    {

        try
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
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




//        private static void updateEngineer()
//        {
//            try
//            {
//                int _id;
//                DO.EngineerExperiece _level;
//                string _name, _email;
//                double _cost;
//                Console.WriteLine("Enter id");
//                int.TryParse(Console.ReadLine()!, out _id);
//                Console.WriteLine(s_dalEngineer!.Read(_id));
//                Console.WriteLine("Enter details to update");
//                _name = Console.ReadLine()!;
//                _email = Console.ReadLine()!;
//                DO.EngineerExperiece.TryParse(Console.ReadLine()!, out _level);
//                double.TryParse(Console.ReadLine()!, out _cost);
//                Engineer newEngineer = new(_id, _name, _email, _level, _cost);
//                s_dalEngineer!.Update(newEngineer);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }


//        private static void crudEngineer()
//        {
//            string choice;
//            choice = Console.ReadLine()!;
//            do
//            {
//                switch (choice)
//                {
//                    case "1":
//                        {
//                            createEngineer();
//                            break;
//                        }
//                    case "2":
//                        {
//                            readEngineer();
//                            break;
//                        }
//                    case "3":
//                        {
//                            readAllEngineers();
//                            break;
//                        }
//                    case "4":
//                        {
//                            updateEngineer();
//                            break;
//                        }
//                    case "5":
//                        {
//                            deleteEngineer();
//                            break;
//                        }
//                    default:
//                        break;
//                }
//                crudMenu("engineer");
//                choice = Console.ReadLine()!;
//            } while (choice != "0");
//        }
//        private static int createTask()
//        {
//            DO.EngineerExperiece _complexityLevel;
//            int _engineerId;
//            string? _desciption, _alias, _remarks, _deliverable;
//            DateTime _deadline, _createdAt;
//            Console.WriteLine("Enter description, alias, dead line, deliverable, remarks, engineer ID, complexity level");
//            _desciption = Console.ReadLine();
//            _alias = Console.ReadLine();
//            _createdAt = DateTime.Now;
//            DateTime.TryParse(Console.ReadLine()!, out _deadline);
//            _deliverable = Console.ReadLine();
//            _remarks = Console.ReadLine();
//            int.TryParse(Console.ReadLine()!, out _engineerId);
//            DO.EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
//            DO.Task newTask = new(0, _desciption, _alias, false, _createdAt, null, null, _deadline, null, _deliverable, _remarks, _engineerId, _complexityLevel);
//            return s_dalTask!.Create(newTask);
//        }
//        private static void readTask()
//        {
//            int _id;
//            Console.WriteLine("Enter task's ID");
//            int.TryParse(Console.ReadLine()!, out _id);
//            Console.WriteLine(s_dalTask!.Read(_id));
//        }

//        private static void readAllTask()
//        {
//            s_dalTask!.ReadAll().ForEach(ele =>
//            {
//                Console.WriteLine(ele);
//            });
//        }

//        private static void updateTask()
//        {
//            try
//            {
//                DO.EngineerExperiece _complexityLevel;
//                int ID;
//                int _engineerId;
//                string? _desciption, _alias, _remarks, _deliverable;
//                bool _milestone;
//                DateTime _deadline, _createdAt, _complete, _start, _forecastDate;
//                Console.WriteLine("Enter ID");
//                int.TryParse(Console.ReadLine()!, out ID);
//                Console.WriteLine(s_dalTask!.Read(ID));
//                Console.WriteLine("Enter details to update");
//                _desciption = Console.ReadLine();
//                _alias = Console.ReadLine();
//                bool.TryParse(Console.ReadLine()!, out _milestone);
//                _createdAt = DateTime.Now;
//                DateTime.TryParse(Console.ReadLine()!, out _start);
//                DateTime.TryParse(Console.ReadLine()!, out _forecastDate);
//                DateTime.TryParse(Console.ReadLine()!, out _deadline);
//                DateTime.TryParse(Console.ReadLine()!, out _complete);
//                _deliverable = Console.ReadLine();
//                _remarks = Console.ReadLine();
//                int.TryParse(Console.ReadLine()!, out _engineerId);
//                DO.EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
//                DO.Task newTask = new(ID, _desciption, _alias, _milestone, _createdAt, _start, _forecastDate, _deadline, _complete, _deliverable, _remarks, _engineerId, _complexityLevel);
//                s_dalTask!.Update(newTask);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        private static void deleteTask()
//        {
//            try
//            {
//                Console.WriteLine("Enter task's ID");
//                int _id;
//                int.TryParse(Console.ReadLine()!, out _id);
//                s_dalTask!.Delete(_id);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//        private static void crudTask()
//        {
//            string choice;
//            choice = Console.ReadLine()!;
//            do
//            {
//                switch (choice)
//                {
//                    case "1":
//                        {
//                            createTask();
//                            break;
//                        }
//                    case "2":
//                        {
//                            readTask();
//                            break;
//                        }
//                    case "3":
//                        {
//                            readAllTask();
//                            break;
//                        }
//                    case "4":
//                        {
//                            updateTask();
//                            break;
//                        }
//                    case "5":
//                        {
//                            deleteTask();
//                            break;
//                        }
//                    default:
//                        break;
//                }
//                crudMenu("task");
//                choice = Console.ReadLine()!;
//            } while (choice != "0");

//        }

//        private static int createDependency()
//        {
//            int _dependsOnTask, _dependentTask;
//            Console.WriteLine("Enter, pending task, previous task");
//            int.TryParse(Console.ReadLine()!, out _dependentTask);
//            int.TryParse(Console.ReadLine()!, out _dependsOnTask);
//            DO.Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
//            return s_dalDependency!.Create(newDependency);
//        }
//        private static void readDependency()
//        {
//            int _id;
//            Console.WriteLine("Enter dependency's ID");
//            int.TryParse(Console.ReadLine()!, out _id);
//            Console.WriteLine(s_dalDependency!.Read(_id));
//        }

//        private static void readAllDependencies()
//        {
//            s_dalDependency!.ReadAll().ForEach(ele =>
//            {
//                Console.WriteLine(ele);
//            });
//        }

//        private static void updateDependency()
//        {
//            try
//            {
//                int ID, _dependsOnTask, _dependentTask;
//                Console.WriteLine("Enter ID");
//                int.TryParse(Console.ReadLine()!, out ID);
//                Console.WriteLine(s_dalDependency!.Read(ID));
//                int.TryParse(Console.ReadLine()!, out _dependentTask);
//                int.TryParse(Console.ReadLine()!, out _dependsOnTask);
//                Dependency newDependency = new(ID, _dependentTask, _dependsOnTask);
//                s_dalDependency!.Update(newDependency);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        private static void deleteDependency()
//        {
//            try
//            {
//                Console.WriteLine("Enter dependency's ID");
//                int _id;
//                int.TryParse(Console.ReadLine()!, out _id);
//                s_dalDependency!.Delete(_id);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//        private static void crudDependency()
//        {
//            string choice;
//            choice = Console.ReadLine()!;
//            do
//            {
//                switch (choice)
//                {
//                    case "1":
//                        {
//                            createDependency();
//                            break;
//                        }
//                    case "2":
//                        {
//                            readDependency();
//                            break;
//                        }
//                    case "3":
//                        {
//                            readAllDependencies();
//                            break;
//                        }
//                    case "4":
//                        {
//                            updateDependency();
//                            break;
//                        }
//                    case "5":
//                        {
//                            deleteDependency();
//                            break;
//                        }
//                    default:
//                        break;
//                }
//                crudMenu("dependency");
//                choice = Console.ReadLine()!;
//            } while (choice != "0");
//        }
//        private static void crudMenu(string entity)
//        {
//            Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
//            $" 3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n");
//        }

//        private static void mainMenu()
//        {
//            Console.WriteLine("Choose:\n 0 to exit\n 1 to engineer\n 2 to task\n 3 to dependency\n");
//            string choice;
//            choice = Console.ReadLine()!;
//            do
//            {
//                switch (choice)
//                {
//                    case "1":
//                        {
//                            crudMenu("engineer");
//                            crudEngineer();
//                            break;
//                        }
//                    case "2":
//                        {
//                            crudMenu("task");
//                            crudTask();
//                            break;
//                        }
//                    case "3":
//                        {
//                            crudMenu("dependency");
//                            crudDependency();
//                            break;
//                        }
//                }
//                Console.WriteLine("Choose:\n 0 to exit\n 1 to engineer\n 2 to task\n 3 to dependency\n");
//                choice = Console.ReadLine()!;
//            } while (choice != "0");
//        }
//        static void Main()
//        {
//            try
//            {
//                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
//                mainMenu();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }
//    }
//}
