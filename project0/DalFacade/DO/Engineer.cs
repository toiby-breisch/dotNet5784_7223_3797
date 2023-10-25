namespace DO;
/// <summary>
/// Engineer Entity represents an engineer with all its props
/// </summary>
///<param name="Id">Personal unique ID of engineer (as in national id card)</param>
///<param name="Name">Private name of the engineer</param>
///<param name="Level">The level of the engineer</param>
///<param name="Email">Private Email of the engineer</param>
///<param name="Cost">A reminder for the month of the engineer</param>
public record Engineer
{
    int Id;
    string Name;
    EngineerExperience level ;
    string? Email;
    double Cost;
}
