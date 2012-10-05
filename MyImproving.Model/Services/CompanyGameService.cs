using System.Collections.Generic;
using System.Linq;
using MyImproving.Model.Machine;
using MyImproving.Model;
using UpdateControls.Correspondence;
using UpdateControls.Fields;

namespace MyImproving.Model.Services
{
    public class CompanyGameService
    {
        private class CreateTurnAutomaton : IAutomaton<Round>
        {
            private CompanyGameService _service;

            public CreateTurnAutomaton(CompanyGameService service)
            {
                _service = service;
            }

            public IEnumerable<Round> GetItems()
            {
                return _service._game.Rounds
                    .Where(r => !r.Turns.Ensure()
                        .Any(t => t.Company == _service._company));
            }

            public void Process(Round item)
            {
                _service._company.CreateTurn(item);
            }
        }

        private readonly Company _company;
        private readonly Game _game;

        private Dependent<Turn> _currentTurn;

        public CompanyGameService(Company company, Game game)
        {
            _company = company;
            _game = game;

            _currentTurn = new Dependent<Turn>(() => _game.Rounds.Ensure()
                .SelectMany(r => r.Turns.Ensure())
                .Where(t => t.Company == _company)
                .OrderByDescending(t => t.Round.Index)
                .FirstOrDefault());
        }

        public void RegisterWith(Actuator actuator)
        {
            actuator.Add(new CreateTurnAutomaton(this));
        }

        public IEnumerable<Turn> Turns
        {
            get
            {
                return _game.Rounds
                    .SelectMany(r => r.Turns.Where(t => t.Company == _company));
            }
        }

        public IEnumerable<Candidate> Candidates
        {
            get
            {
                Turn currentTurn = _currentTurn.Value;
                return currentTurn == null
                    ? null
                    : currentTurn.Round.Candidates;
            }
        }

        public Offer MakeOffer(Candidate candidate, int chances)
        {
            Turn currentTurn = _currentTurn.Value;
            return currentTurn.CreateOffer(candidate, chances);
        }

        public IEnumerable<Hire> Employees
        {
            get
            {
                return _game.Rounds
                    .SelectMany(round => round.Turns)
                    .Where(turn => turn.Company == _company)
                    .SelectMany(turn => turn.Hires);
            }
        }
    }
}
