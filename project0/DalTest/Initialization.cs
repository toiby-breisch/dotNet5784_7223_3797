namespace DalTest;
    using DalApi;
    using DO;
    using System;
    using System.Threading;

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
                string _tEmail = _name;
                _tEmail.input.Tr.Replace(" ", "");
                string _email = _tEmail + "@gmail.com";
                EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 2);
                int _cost = s_rand.Next(MIN_C, MAX_C);
                Engineer newEngineer = new(_id,_name,_email,_level,_cost);
                s_dalEngineer!.Create(newEngineer);
            }
        }

        private static DateTime RandomDate(DateTime startDate, DateTime endDate)
        {
            Random gen = new Random();
            int range = (endDate - startDate).Days;
            return startDate.AddDays(gen.Next(range));
        }

        private static void createTask()
        {
            string[] Aliases = { "a", "b", "c", "d", "e" };
            string[] Remarks = { "a", "b", "c", "d", "e" };
        string[] Deliverables = { "r", "a", "c", "e", "l" };

        for (int i = 0; i < 100; i++)
            {
                int indexA = s_rand.Next(0, 4);
                int indexD = s_rand.Next(0, 4);
                string _description = ((TaskLevel)indexD).ToString();
                string _Alias = Aliases[indexA];
                bool _Milestone = s_rand.Next(0, 1)==0 ? true:false;
                DateTime _CreatedAt = RandomDate(new DateTime(2020, 1, 1), DateTime.Today);
                DateTime _Start = RandomDate(_CreatedAt, DateTime.Today);
                DateTime ForecasDate = _Start.AddDays((((TaskLevel)indexD + 1) * 365).Days);
                DateTime Deadline = ForecasDate.AddDays((365 / 2).Days);
                DateTime Complete = RandomDate(ForecasDate, Deadline);
                int indexEI = s_rand.Next(0, 40);
               // int _EngineerId = DataList.DataSource.Engineers[indexEI].Id;
                EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 2);
                int indexR = s_rand.Next(0, 4);
                int indexDe = s_rand.Next(0, 4);
                string _Remarks = Remarks[indexR];
                string _Deliverables = Deliverables[indexDe];
                Task newTask = new(_Description, _Alias, _Milestone, _CreatedAt, _Start, _ForecasDate, _Deadline,
                _Complete, _Deliverables, _Remarks, _Engineerid, _level);
                s_dalTask!.Create(newTask);

            }
        }
        private static void createDependency()
        {
            for (int i = 0; i < 100; i++)
            {
                int indexDT = s_rand.Next(0, 100);
                int indexDOT = s_rand.Next(0, 100);
                //int _DependentTask = Dependencies[indexDT];
                //int _DependsOnTask = Dependencies[indexDOT];
                //Engineer newDependent = new(_DependentTask, _DependsOnTask);
                //s_dalDependency!.Create(newDependent);
            }

        }

    public static void Do(IEngineer? dalEngineer, ITask? dalTask, IDependency? dalDependency)
    {
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //createEmployee();
        //createTask();
        //createDependency();
    }


}

