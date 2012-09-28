using System.Collections.Generic;
using MyImproving.Model;
using UpdateControls.Collections;
using UpdateControls.Fields;

namespace MyImproving.Moderator.Models
{
    public class NewGameSelectionModel
    {
        private Independent<string> _gameName = new Independent<string>();
        private IndependentList<Company> _selectedCompanies = new IndependentList<Company>();

        public string GameName
        {
            get { return _gameName; }
            set { _gameName.Value = value; }
        }

        public IEnumerable<Company> SelectedCompanies
        {
            get { return _selectedCompanies; }
        }

        public bool IsCompanySelected(Company company)
        {
            return _selectedCompanies.Contains(company);
        }

        public void SetCompanySelected(Company company, bool value)
        {
            if (value == true && !_selectedCompanies.Contains(company))
                _selectedCompanies.Add(company);
            else if (value == false && _selectedCompanies.Contains(company))
                _selectedCompanies.Remove(company);
        }

        public void ClearSelectedCompanies()
        {
            _selectedCompanies.Clear();
        }
    }
}
