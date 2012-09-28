using System.Collections.Generic;
using System.Linq;
using MyImproving.Model;
using MyImproving.Models;
using System.Windows.Input;
using UpdateControls.XAML;
using System;

namespace MyImproving.ViewModels
{
    public class CompanySelectionViewModel
    {
        private readonly Individual _individual;
        private readonly CompanySelectionModel _selection;

        public CompanySelectionViewModel(Individual individual, CompanySelectionModel selection)
        {
            _individual = individual;
            _selection = selection;
        }

        public string CompanySelectionHeader
        {
            get
            {
                return
                    _selection.SelectedCompany != null
                        ? _selection.SelectedCompany.Name.Value :
                    _individual.Companies.Any()
                        ? "<Select a company>"
                        : "<Create a company>";
            }
        }

        public string CompanyName
        {
            get { return _selection.CompanyName; }
            set { _selection.CompanyName = value; }
        }

        public IEnumerable<CompanyHeaderViewModel> Companies
        {
            get
            {
                return
                    from company in _individual.Companies
                    orderby company.Name.Value
                    select new CompanyHeaderViewModel(company);
            }
        }

        public CompanyHeaderViewModel SelectedCompany
        {
            get
            {
                return _selection.SelectedCompany == null
                    ? null
                    : new CompanyHeaderViewModel(_selection.SelectedCompany);
            }
            set
            {
                _selection.SelectedCompany = value == null
                    ? null
                    : value.Company;
            }
        }

        public ICommand EditCompany
        {
            get
            {
                return MakeCommand
                    .When(() => _selection.SelectedCompany != null)
                    .Do(delegate
                    {
                        _selection.CompanyName = _selection.SelectedCompany.Name.Ensure();
                        _selection.Mode = CompanySelectionModel.SelectionMode.Edit;
                        if (DisplayCompanyEdit != null)
                            DisplayCompanyEdit();
                    });
            }
        }

        public ICommand NewCompany
        {
            get
            {
                return MakeCommand
                    .Do(delegate
                    {
                        _selection.CompanyName = string.Empty;
                        _selection.Mode = CompanySelectionModel.SelectionMode.New;
                        if (DisplayCompanyEdit != null)
                            DisplayCompanyEdit();
                    });
            }
        }

        public ICommand SaveCompanyName
        {
            get
            {
                return MakeCommand
                    .When(() => !String.IsNullOrEmpty(_selection.CompanyName))
                    .Do(delegate
                    {
                        if (_selection.Mode == CompanySelectionModel.SelectionMode.New)
                            _selection.SelectedCompany = _individual.CreateCompany();
                        _selection.SelectedCompany.Name = _selection.CompanyName;
                        if (HideCompanyEdit != null)
                            HideCompanyEdit();
                    });
            }
        }

        public event Action DisplayCompanyEdit;
        public event Action HideCompanyEdit;
    }
}
