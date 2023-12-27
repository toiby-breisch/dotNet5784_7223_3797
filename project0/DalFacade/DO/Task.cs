namespace DO;
/// <summary>
/// Task Entity represents an Task with all its props
/// </summary>
/// <param name="Id">ID number of the assignment</param>
/// <param name="Description">Description of the assignment</param>
/// <param name="Alias">Nickname of the assignment</param>
/// <param name="Milestone">Milestone (Boolean) - of the assignment</param>
/// <param name="CreatedAt">Creation date of the assignment</param>
/// <param name="Start">Start date of the assignment</param>
/// <param name="ForecasDate">Estimated date for completion of the assignment</param>
/// <param name="Deadline">Last date to finish the assignment</param>
/// <param name="Complete">Actual end date of the assignment</param>
/// <param name="Deliverables">Product (string describing the product) of the assignment</param>
/// <param name="Remarks">Notes of the assignment</param>
/// <param name="Engineerid">The engineer ID assigned to the task</param>
/// <param name="CopmlexityLevel">Difficulty level of the assignment</param>
public record Task
(
    int Id,
    string? Description,
    string? Alias,
    bool ?Milestone,
    DateTime CreatedAt,
    DateTime Start,
    DateTime ForecasDate,
    DateTime Deadline,
    DateTime Complete,
    string ?Deliverables,
    string? Remarks,
    int Engineerid,
    EngineerExperience CopmlexityLevel,
      bool IsActive 
)
{
    public Task() : this(0,"","",false,DateTime.MinValue,DateTime.MinValue, DateTime.MinValue,
        DateTime.Now, DateTime.Now,"","",0,0,true) { } //empty ctor 
}


