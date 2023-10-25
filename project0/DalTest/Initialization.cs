namespace DalTest;
using DalApi;
using DO;
using System;
using System.Threading;

public static class Initialization
{
     
    enum TaskLevel
    {
         VERY_EASY , 
        EASY, 
        MEDIUM, 
        HARD, 
        VERY_HARD
    }

    private static IEngineer? s_Engineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();

    private static void createEmployee()
    {
        const MIN_ID = 200000000;
        const MAX_ID = 400000000;
        const MIN_C = 27;
        const MAX_C = 300

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
        "Debbi Pety"


    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);
            _tEmail = _name;
            _tEmail.input.Trim().Replace(" ", "");
            _email = string(_tEmail + "@gmail.com");
            EngineerExperience _level = s_rand.Next(1, 3);
            int _cost = s_rand.Next(MIN_C, MAX_C);
            Engineer newEngineer = new(_id, _name, _level, _email, _cost);
            s_dalEmployee!.Create(newEngineer);
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
        string[] Aliases = ["a", "b", "c", "d", "e"]
           
         for (int i = 0; i < 100; i++)
        {
            int indexA = s_rand.Next(0, 4);
            int indexD = s_rand.Next(0, 4);
            string _description = String(TaskLevel)indexD;
            string _Alias = Aliases[indexA];
            bool _Milestone = s_rand.Next(0, 1);
            DateTime _CreatedAt = RandomDate(new DateTime(2020, 1, 1), DateTime.Today);
            DateTime _Start = RandomDate(_CreatedAt, DateTime.Today);
            DateTime ForecasDate = _Start.AddDays((((TaskLevel)indexD + 1) * 365).Days);
            DateTime Deadline= ForecasDate.AddDays((365/2).Days);
            DateTime Complete= RandomDate(ForecasDate, Deadline);
        }
    }

}
//do(IEngineer,)
//             {

