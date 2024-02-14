

namespace BO
{
    public class MilestoneInTask
    {
        public override string ToString() => Tools.ToStringProperty(this);
        public int Id { get; init; }
        public   required string Alias { get; set; }
    }
}
