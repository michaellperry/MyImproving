using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MyImproving.Model;
using MyImproving.Moderator.Models;
using UpdateControls.XAML;
using System;

namespace MyImproving.Moderator.ViewModels
{
    public class NewGameViewModel
    {
        private readonly Domain _domain;
        private readonly NewGameSelectionModel _selection;

        public NewGameViewModel(Domain domain, NewGameSelectionModel selection)
        {
            _domain = domain;
            _selection = selection;
        }

        public string GameName
        {
            get { return _selection.GameName; }
            set { _selection.GameName = value; }
        }

        public IEnumerable<CompanyHeaderViewModel> Companies
        {
            get
            {
                return
                    from company in _domain.Companies
                    orderby company.Name.Value
                    select new CompanyHeaderViewModel(company, _selection);
            }
        }

        public ICommand BeginGame
        {
            get
            {
                return MakeCommand
                    .When(() =>
                        !String.IsNullOrEmpty(_selection.GameName) &&
                        _selection.SelectedCompanies.Any())
                    .Do(delegate
                    {
                        var game = _domain.CreateGame(_selection.SelectedCompanies);
                        game.Name = _selection.GameName;
                        _selection.GameName = string.Empty;
                        _selection.ClearSelectedCompanies();
                        if (HideNewGame != null)
                            HideNewGame();
                    });
            }
        }

        public event Action HideNewGame;
    }
}
