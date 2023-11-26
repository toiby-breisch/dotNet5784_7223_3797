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
    private static IDal? s_dal;//stage2;


    private static readonly Random s_rand = new();
    public static void Do(IDal dal)
    {

        s_dal = dal ?? throw new DalDeletionImpossible("DAL object can not be null!"); //stage 2
        createEngineer();
        createTask();
        createDependency();
    }
    /// <summary>
    /// Initializes the list of engineers
    /// </summary>
    private static void createEngineer()
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
        //"Kaila Avramovitz",
        //"Rachely Vainberg",
        //"Gili Reker",
        //"Zehava Simcha",
        //"Nahama Levi",
        //"Hindi Nachumi",
        //"Leaha Segal",
        //"Chaya Toyal",
        //"Debbi Pety",
        //"Anna Coheni",
        //"Efrat Kati",
        //"Devora Tal",
        //"Tova Eliimelech",
        //"Yeudit Avramov",
        //"Sury Shvartz",
        //"Malki Gotfrid",
        //"Sari Brodi",
        //"Roizy Safrin",
        //"Eti Deblinger",
        //"Racheli Bekerman",
        //"Miri Kaner",
        //"Suly Eler"



    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(_id) != null);
            string _tEmail = _name.Replace(" ", "");
            string _email = _tEmail + "@gmail.com";
            int _level = _id % 5;
            int _cost = s_rand.Next(MIN_C, MAX_C);

            Engineer newEngineer = new(_id, _name, _email, (DO.EngineerExperience)_level, _cost);
            s_dal.Engineer!.Create(newEngineer);
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
        List<Engineer> engineers = s_dal!.Engineer.ReadAll(ele => ele.Id > 0).ToList();
        string[] Aliases = { "a", "b", "c", "d", "e" };
        string[] Remarks = { "a", "b", "c", "d", "e" };
        string[] Deliverables = { "r", "a", "c", "e", "l" };

        for (int i = 0; i < 5; i++)
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
            int indexId = s_rand.Next(0, 40);
            int _Engineerid = engineers[indexId].Id;
            int indexEI = s_rand.Next(0, 40);
            int _level = indexA % 5;
            int indexR = s_rand.Next(0, 4);
            int indexDe = s_rand.Next(0, 4);
            string _Remarks = Remarks[indexR];
            string _Deliverables = Deliverables[indexDe];
            Task newTask = new(3, _description, _Alias, _Milestone, _CreatedAt, _Start, _ForecasDate, _Deadline, _Complete, _Deliverables, _Remarks, _Engineerid, (DO.EngineerExperience)_level);
            s_dal!.Task.Create(newTask);
        }
    }



    public static void createDependency()
    {
        int _dependentTask, _dependsOnTask;
        for (int i = 0; i < 5; i++)
        {
            do
            {
                _dependentTask = s_rand.Next(100);
                _dependsOnTask = s_rand.Next(_dependentTask);
            }
            while (s_dal!.Dependency.isDepend(_dependentTask, _dependsOnTask));
            Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
            s_dal!.Dependency.Create(newDependency);
        }

    }
}




