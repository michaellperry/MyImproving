
namespace MyImproving.Model
{
    public partial class Turn
    {
        public Offer CreateOffer(Candidate candidate, int chances)
        {
            return Community.AddFact(new Offer(this, candidate, chances));
        }
    }
}
