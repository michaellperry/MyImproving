using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MyImproving.Model;
using UpdateControls.Correspondence;
using System.Collections.Generic;
using System.Linq;
using MyImproving.Model.Machine;

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

        public readonly Company _company;
        public readonly Game _game;

        public CompanyGameService(Company company, Game game)
        {
            _company = company;
            _game = game;
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
    }
}
