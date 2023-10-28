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
);
