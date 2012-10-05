using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyImproving.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;
using MyImproving.Model.Subscriptions;

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
            _communityFlynn = NewCommunity(sharedCommunication);
            _communityAlan = NewCommunity(sharedCommunication);
            _communityModerator = NewCommunity(sharedCommunication);

            _individualFlynn = _communityFlynn.AddFact(new Individual("flynn"));
            _individualAlan = _communityAlan.AddFact(new Individual("alan"));
            _domainModerator = _communityModerator.AddFact(new Domain("Improving Enterprises"));

            _communityFlynn.Subscribe(_individualFlynn);
            _communityAlan.Subscribe(_individualAlan);
            _communityModerator.Subscribe(_domainModerator);
        }

        private static Community NewCommunity(MemoryCommunicationStrategy sharedCommunication)
        {
            return new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>();
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
