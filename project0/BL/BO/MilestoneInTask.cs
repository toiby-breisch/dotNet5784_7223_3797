

namespace BO
{
    public class MilestoneInTask
    {
        public MilestoneInTask(int id, string alias)
        {
            Id = id;
            Alias = alias;
        }

        public int Id { get; init; }
        public  string Alias { get; set; }
    }
}
