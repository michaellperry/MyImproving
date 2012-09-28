using System.Linq;

namespace MyImproving.Model
{
    public partial class Game
    {
        public Round CreateRound(int index)
        {
            return Community.AddFact(new Round(this, index));
        }

    }
}
