

namespace BO
{
    public class MilestoneInTask
    {
        //public MilestoneInTask(int id, string alias)
        //{
        //    Id = id;
        //    Alias = alias;
        //}
        public override string ToString() => Tools.ToStringProperty(this);
        public int Id { get; init; }
        public   required string Alias { get; set; }
    }
}
