
namespace DalTest
{
    static public void Main_Menu(int num)
    {
        switch (num)
        {
            case 0:return;
                break;
                case 1:
                createEmployee();
                break;
                case 2:
                createTask();
        }
    }
    internal class program
    {
        private static ITask? s_dalTask = new TaskImplementation();
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static IDependency? s_dalDependency = new DependencyImplementation();
        static void Main()
        {

            try
            {
                Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);

            }
            catch(Exception EX)
            {
                Console.WriteLine(EX);
            }


        }
    }
}