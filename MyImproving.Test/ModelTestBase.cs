using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace MyImproving.Test
{
    public class TestBase
    {
        protected Community _communityAlan;
        protected Community _communityFlynn;
        protected Community _communityModerator;
        protected Domain _domainModerator;
        protected Individual _individualAlan;
        protected Individual _individualFlynn;

        protected void InitializeCommunity()
        {
            var sharedCommunication = new MemoryCommunicationStrategy();
            _communityFlynn = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualFlynn)
                .Subscribe(() => _individualFlynn.Companies)
                .Subscribe(() => _individualFlynn.Games)
                ;
            _communityAlan = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualAlan)
                .Subscribe(() => _individualAlan.Companies)
                .Subscribe(() => _individualAlan.Games)
                ;
            _communityModerator = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _domainModerator)
                .Subscribe(() => _domainModerator.Games)
                ;

            _individualFlynn = _communityFlynn.AddFact(new Individual("flynn"));
            _individualAlan = _communityAlan.AddFact(new Individual("alan"));
            _domainModerator = _communityModerator.AddFact(new Domain("Improving Enterprises"));
        }

        protected void Synchronize()
        {
            while (
                _communityFlynn.Synchronize() ||
                _communityAlan.Synchronize() ||
                _communityModerator.Synchronize());
        }
    }
}
