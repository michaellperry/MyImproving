
namespace MyImproving.Model
{
    public partial class Company
    {
        public Turn CreateTurn(Round round)
        {
            return Community.AddFact(new Turn(this, round));
        }
    }
}
