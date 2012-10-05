using System.Collections.Generic;
using System.Linq;

namespace MyImproving.Model.Services
{
    public class ModeratorService
    {
        private readonly Game _game;

        private CandidateDeck _candidateDeck = new CandidateDeck();

        private Round _currentRound;

        public ModeratorService(Game game)
        {
            _game = game;
        }

        public void BeginNextRound()
        {
            int nextRoundIndex = _game.Rounds.Ensure()
                .OrderBy(round => round.Index)
                .Select(round => round.Index)
                .FirstOrDefault() + 1;
            _currentRound = _game.CreateRound(nextRoundIndex);
        }

        public List<Candidate> DealCandidates(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => _candidateDeck.DealCandidateCard(_currentRound))
                .ToList();
        }
    }
}
