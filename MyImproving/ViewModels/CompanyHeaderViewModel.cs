using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;

namespace MyImproving.ViewModels
{
    public class CompanyHeaderViewModel
    {
        private readonly Company _company;

        public CompanyHeaderViewModel(Company company)
        {
            _company = company;            
        }

        internal Company Company
        {
            get { return _company; }
        }

        public string Name
        {
            get { return _company.Name; }
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
