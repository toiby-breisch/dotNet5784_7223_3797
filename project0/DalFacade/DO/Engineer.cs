
using System.Reflection.Emit;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Engineer Entity represents a engineer with all its prop
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Name of the engineer</param>
/// <param name="Email">Email address of the engineer</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="Cost">Hourly cost of the engineer</param>
public record Engineer
(
       int Id,
       string Name,
       string? Email = null,
       EngineerExperience Level = EngineerExperience.Beginner,
       double Cost = 0,
       bool IsActive=true
)
{
    public Engineer() : this(0,"") { }
    public DateTime RegistrationDate => DateTime.Now; //get only
}

