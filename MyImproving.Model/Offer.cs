
namespace MyImproving.Model
{
    public partial class Offer
    {
        public void CreateHire()
        {
            Community.AddFact(new Hire(this));
        }
    }
}
