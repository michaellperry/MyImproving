using System;
using System.Linq;
using UpdateControls.Correspondence;

namespace MyImproving.Model
{
    public partial class Individual
    {
        public Company CreateCompany()
        {
            Domain theDomain = Community.AddFact(new Domain("Improving Enterprises"));
            Company company = Community.AddFact(new Company(theDomain));
            Community.AddFact(new Director(this, company));
            return company;
        }
    }
}
