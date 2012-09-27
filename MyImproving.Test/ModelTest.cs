using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;
using MyImproving.Model;

namespace MyImproving.Test
{
    [TestClass]
    public class ModelTest
    {
        private Community _communityFlynn;
        private Community _communityAlan;
        private Community _communityModerator;
        private Individual _individualFlynn;
        private Individual _individualAlan;
        private Domain _domainModerator;

        [TestInitialize]
        public void Initialize()
        {
            var sharedCommunication = new MemoryCommunicationStrategy();
            _communityFlynn = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualFlynn)
				;
            _communityAlan = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualAlan)
                ;
            _communityModerator = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _domainModerator)
                ;

            _individualFlynn = _communityFlynn.AddFact(new Individual("flynn"));
            _individualAlan = _communityAlan.AddFact(new Individual("alan"));
            _domainModerator = _communityModerator.AddFact(new Domain("Improving Enterprises"));
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

        private void Synchronize()
        {
            while (
                _communityFlynn.Synchronize() ||
                _communityAlan.Synchronize() ||
                _communityModerator.Synchronize()) ;
        }
	}
}
