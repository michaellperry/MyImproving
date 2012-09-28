using System.Collections.Generic;
using MyImproving.Model;
using UpdateControls.Collections;

namespace MyImproving.Moderator.Models
{
    public class CompanySelectionModel
    {
        private IndependentList<Company> _selectedCompanies = new IndependentList<Company>();

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
