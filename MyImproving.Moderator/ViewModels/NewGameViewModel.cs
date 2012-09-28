using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MyImproving.Model;
using MyImproving.Moderator.Models;
using UpdateControls.XAML;

namespace MyImproving.Moderator.ViewModels
{
    public class NewGameViewModel
    {
        private readonly Domain _domain;
        private readonly CompanySelectionModel _selection;

        public NewGameViewModel(Domain domain, CompanySelectionModel selection)
        {
            _domain = domain;
            _selection = selection;
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
                    .When(() => _selection.SelectedCompanies.Any())
                    .Do(delegate
                    {
                        _domain.CreateGame(_selection.SelectedCompanies);
                        _selection.ClearSelectedCompanies();
                    });
            }
        }
    }
}
