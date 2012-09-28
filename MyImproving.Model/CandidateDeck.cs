using System;

namespace MyImproving.Model
{
    public class CandidateDeck
    {
        private Random _random = new Random();

        public Candidate DealCandidateCard(Round round)
        {
            int skill = _random.Next(Enum.GetValues(typeof(Candidate.SkillEnum)).Length);
            int relationship = _random.Next(Enum.GetValues(typeof(Candidate.RelationshipEnum)).Length);
            return round.CreateCandidate(skill, relationship);
        }
    }
}
