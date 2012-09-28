using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyImproving.Model;

namespace MyImproving.Test
{
    [TestClass]
    public class ModelTest : TestBase
    {
        [TestInitialize]
        public void Initialize()
        {
            InitializeCommunity();
        }

        [TestMethod]
        public void AlanCreatesACompany()
        {
            Company companyAlan = _individualAlan.CreateCompany();
            companyAlan.Name = "Initech";

            Synchronize();

            List<Company> companies = _domainModerator.Companies.ToList();
            Assert.AreEqual(1, companies.Count);
            Assert.AreEqual("Initech", companies[0].Name.Value);
        }

        [TestMethod]
        public void ModeratorCreatesAGame()
        {
            Company companyAlan = _individualAlan.CreateCompany();
            companyAlan.Name = "Initech";

            Company companyFlynn = _individualFlynn.CreateCompany();
            companyFlynn.Name = "Flynn's";

            Synchronize();

            _domainModerator.CreateGame(_domainModerator.Companies);

            Synchronize();

            List<Game> gamesAlan = companyAlan.Games.ToList();
            Assert.AreEqual(1, gamesAlan.Count);
            Assert.AreEqual(2, gamesAlan[0].Companies.Count());
        }
    }
}
