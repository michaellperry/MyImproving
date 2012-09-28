using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;
using MyImproving.Moderator.Models;

namespace MyImproving.Moderator.ViewModels
{
    public class CompanyHeaderViewModel
    {
        private readonly Company _company;
        private readonly CompanySelectionModel _selection;
        
        public CompanyHeaderViewModel(Company company, CompanySelectionModel selection)
        {
            _company = company;
            _selection = selection;
        }

        public string Name
        {
            get
            {
                return _company.Name.Value == null
                    ? "<New company>"
                    : _company.Name.Value;
            }
        }

        public bool IsSelected
        {
            get { return _selection.IsCompanySelected(_company); }
            set { _selection.SetCompanySelected(_company, value); }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            CompanyHeaderViewModel that = obj as CompanyHeaderViewModel;
            if (that == null)
                return false;
            return Object.Equals(this._company, that._company);
        }

        public override int GetHashCode()
        {
            return _company.GetHashCode();
        }
    }
}
