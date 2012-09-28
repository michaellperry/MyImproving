using UpdateControls.Fields;
using MyImproving.Model;

namespace MyImproving.Models
{
    public class CompanySelectionModel
    {
        public enum SelectionMode
        {
            New,
            Edit
        }

        private Independent<string> _companyName = new Independent<string>();
        private Independent<Company> _selectedCompany = new Independent<Company>();
        private Independent<SelectionMode> _mode = new Independent<SelectionMode>();

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName.Value = value; }
        }

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany.Value = value; }
        }

        public SelectionMode Mode
        {
            get { return _mode; }
            set { _mode.Value = value; }
        }
    }
}
