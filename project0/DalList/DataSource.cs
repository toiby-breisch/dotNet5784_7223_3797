
namespace Dal;
using DO;
internal static class DataSource
{
    internal static List<DO.Engineer> Engineers { get; } = new(40);
    internal static List<DO.Task> Taskls { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
        private static int nextDependencyId = startTaskId;
        internal static int NextDependencyId { get => nextTaskId++; }
    }
}
