using System.Collections.Generic;
using System.Linq;
using System;

namespace MyImproving.Model.Services
{
    public class ModeratorService
    {
        private readonly Game _game;

        private CandidateDeck _candidateDeck = new CandidateDeck();
        private Random _dice = new Random();

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
        
        public void AwardCandidates()
        {
            var candidates = _currentRound.Candidates.Ensure().ToList();
            foreach (var candidate in candidates)
            {
                var offers = candidate.Offers.Ensure().ToList();
                if (offers.Any())
                {
                    int chances = offers.Sum(offer => offer.Chances);
                    int roll = _dice.Next(chances);
                    foreach (var offer in offers)
                    {
                        roll -= offer.Chances;
                        if (roll < 0)
                        {
                            offer.CreateHire();
                            break;
                        }
                    }
                }
            }
        }
    }
}
