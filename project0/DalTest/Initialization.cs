namespace DalTest;
using DalApi;
using DO;
using System;

//<summary>
//initiation the lists
//</summary>

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
    private static IDal? s_dal;


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
        List<Engineer> engineers = s_dal!.Engineer.ReadAll(ele => ele.Id > 0).ToList()!;
        string[] Aliases = { "a", "b", "c", "d", "e" };
        string[] Remarks = { "a", "b", "c", "d", "e" };
        string[] Deliverables = { "r", "a", "c", "e", "l" };

        for (int i = 0; i < 100; i++)
        {
            int indexA = s_rand.Next(0, 4);
            int indexD = s_rand.Next(0, 4);
            string _description = ((TaskLevel)indexD).ToString();
            string _alias = Aliases[indexA];
            TimeSpan span = new(s_rand.Next(300), s_rand.Next(24), s_rand.Next(60), s_rand.Next(60));
            TimeSpan _requiredEffortTime = span / 2;
            DateTime _createdAt = DateTime.Today;
            DateTime? _start = s_rand.Next(3) > 1 ? DateTime.Today + span / 10 : null;
            DateTime? _forecastEndDate = _start == null ? null : _start + _requiredEffortTime;
            DateTime _deadline = _createdAt + span;
            DateTime _baselineStart = _createdAt + span / 20;
            int _engineerId = engineers.ElementAt(s_rand.Next(40)).Id;
            int _complexityLevel = s_rand.Next(1, 6);

            Task newTask = new(0, _description, _alias, false, _createdAt, _start,
                _forecastEndDate, _deadline, null, null, null, _engineerId, (DO.EngineerExperience)_complexityLevel, true);
            s_dal.Task!.Create(newTask);


        }
    }

    /// <summary>
    /// Initializes the dependencies
    /// </summary>

    public static void createDependency()
    {
        int _dependentTask, _dependsOnTask;
        for (int i = 0; i < 100; i++)
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




