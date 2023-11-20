using System.Reflection.Emit;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// Dependency Entity represents an Dependency with all its props
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="DependentTask">ID number of pending task</param>
///  <param name="DependsOnTask">Previous assignment ID number</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependsOnTask
)
{
    public Dependency() : this(0,0,0) { } //empty ctor for stage 3

    public DateTime RegistrationDate => DateTime.Now; //get only

    public static Dependency? Find(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}

