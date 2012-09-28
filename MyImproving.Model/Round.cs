
namespace MyImproving.Model
{
    public partial class Round
    {
        public Candidate CreateCandidate(int skill, int relationship)
        {
            return Community.AddFact(new Candidate(this, skill, relationship));
        }
    }
}
