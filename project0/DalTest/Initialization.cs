namespace DalTest;
using DO;
using DalApi;
using System;
using System.Threading;
using System.Security.Cryptography;


public static class Initialization
{

    enum TaskLevel
    {
        VERY_EASY,
        EASY,
        MEDIUM,
        HARD,
        VERY_HARD
    }

    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();
    /// <summary>
    /// Initializes the list of engineers
    /// </summary>
    private static void createEmployee()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        const int MIN_C = 27;
        const int MAX_C = 300;

        string[] engineerNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof",
        "Toiby Braish",
        "Maly Kibelevitz",
        "Ruti Salomon",
        "Dvory Mimran",
        "Sari Brodi",
        "Roizy Lefkovit",
        "Chani Rozinberg",
        "Ayala Shraber",
        "Chaya Klain",
        "Esty Shvartz",
        "Pnini Cohen",
        "Giti Leder",
        "Feigy Haker",
        "Kaila Avramovitz",
        "Rachely Vainberg",
        "Gili Reker",
        "Zehava Simcha",
        "Nahama Levi",
        "Hindi Nachumi",
        "Leaha Segal",
        "Chaya Toyal",
        "Debbi Pety",
        "Anna Coheni",
        "Efrat Kati",
        "Devora Tal",
        "Tova Eliimelech",
        "Yeudit Avramov",
        "Sury Shvartz",
        "Malki Gotfrid",
        "Sari Brodi",
        "Roizy Safrin",
        "Eti Deblinger",
        "Racheli Bekerman",
        "Miri Kaner",
        "Suly Eler"



    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
             _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);
            string _tEmail = _name.Replace(" ", "");
            string _email = _tEmail + "@gmail.com";
            int _level = _id % 5;
            int _cost = s_rand.Next(MIN_C, MAX_C);
            
            Engineer newEngineer = new(_id, _name, _email,(DO.EngineerExperience)_level, _cost);
            s_dalEngineer!.Create(newEngineer);
        }
    }
    /// <summary>
    /// Initializes the datetime
    /// </summary>
    private static DateTime RandomDate(DateTime startDate, DateTime endDate)
    {
        Random gen = new Random();
        int range = (endDate - startDate).Days;
        return startDate.AddDays(gen.Next(range));
    }

    /// <summary>
    /// Initializes the tasks
    /// </summary>

    private static void createTask()

    {
        List<Engineer> engineers = s_dalEngineer!.ReadAll();
        string[] Aliases = { "a", "b", "c", "d", "e" };
        string[] Remarks = { "a", "b", "c", "d", "e" };
        string[] Deliverables = { "r", "a", "c", "e", "l" };

        for (int i = 0; i < 100; i++)
        {
            int indexA = s_rand.Next(0, 4);
            int indexD = s_rand.Next(0, 4);
            string _description = ((TaskLevel)indexD).ToString();
            string _Alias = Aliases[indexA];
            bool _Milestone = s_rand.Next(0, 1) == 0 ? true : false;
            DateTime _CreatedAt = RandomDate(new DateTime(2020, 1, 1), DateTime.Today);
            DateTime _Start = RandomDate(_CreatedAt, DateTime.Today);
            DateTime _ForecasDate = _Start.AddDays(((indexD + 1) * 365));
            DateTime _Deadline = _ForecasDate.AddDays((365 / 2));
            DateTime _Complete = RandomDate(_ForecasDate, _Deadline);
            int indexId= s_rand.Next(0, 40);
            int _Engineerid = engineers[indexId].Id;
            int indexEI = s_rand.Next(0, 40);
            int _level = indexA % 5;
            int indexR = s_rand.Next(0, 4);
            int indexDe = s_rand.Next(0, 4);
            string _Remarks = Remarks[indexR];
            string _Deliverables = Deliverables[indexDe];
            Task newTask = new(3,_description, _Alias, _Milestone,_CreatedAt,_Start,_ForecasDate,_Deadline,_Complete,_Deliverables,_Remarks,_Engineerid,(DO.EngineerExperience)_level);
            s_dalTask!.Create(newTask);
        }
    }



    public static void createDependency()
    {
        int _dependentTask, _dependsOnTask;
        for (int i = 0; i < 250; i++)
        {
            do
            {
                _dependentTask = s_rand.Next(100);
                _dependsOnTask = s_rand.Next(_dependentTask);
        }
        while (s_dalDependency!.isDepend(_dependentTask, _dependsOnTask)) ;
        Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
            s_dalDependency!.Create(newDependency);
        }

    }

    public static void Do(IEngineer? dalEngineer, ITask? dalTask, IDependency? dalDependency)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createEmployee();
        createTask();
        createDependency();
    }


}

