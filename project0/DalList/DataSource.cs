
namespace Dal;
using DO;
internal static class DataSource
{
    /// <summary>
    /// Creates lists of objects
    /// </summary>
    internal static List<DO.Engineer> Engineers { get; } = new(40);
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    /// <summary>
    /// Generates an automatic runner ID number
    /// </summary>
    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
        private static int nextDependencyId = startTaskId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
}
