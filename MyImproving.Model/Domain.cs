using System;
using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;

namespace MyImproving.Model
{
    public partial class Domain
    {
        public Game CreateGame(IEnumerable<Company> companies)
        {
            return Community.AddFact(new Game(this, companies));
        }
    }
}
