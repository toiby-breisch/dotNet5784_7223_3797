﻿namespace DalTest;
using DalApi;
using DO;

public static class Initialization
{
   

    private static IEngineer? s_Engineer; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();
   
    private static void createEmployee()
    {
        const MIN_ID = 200000000;
        const MAX_ID = 400000000;
        const MIN_C = 27;
        const MAX_C =300

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
        "Chaya "


    };

        foreach (var _name in engineerNames)
        {
            int _id;
            do
               _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalAEmployee!.Read(_id) != null);
            _tEmail = _name;
            _tEmail.input.Trim().Replace(" ", "");
            _email = string(_tEmail + "@gmail.com");
             EngineerExperience _level = s_rand.Next(1, 3);
            int _cost = s_rand.Next(MIN_C, MAX_C);
            Engineer newEngineer=new(_id,_name,_level,_email,_cost);
            s_crEmployee!.Create(newEngineer);
        }
    }


   
   //do(IEngineer,)
   //             {

        }