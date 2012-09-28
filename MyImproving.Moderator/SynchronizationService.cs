using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.BinaryHTTPClient;
using MyImproving.Model;

namespace MyImproving.Moderator
{
    public class SynchronizationService
    {
        private Community _community;
        private Domain _domain;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _domain)
                ;

            _domain = _community.AddFact(new Domain("Improving Enterprises"));

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                _community.BeginSending();
            };

            // Periodically resume if there is an error.
            DispatcherTimer synchronizeTimer = new DispatcherTimer();
            synchronizeTimer.Tick += delegate
            {
                _community.BeginSending();
                _community.BeginReceiving();
            };
            synchronizeTimer.Interval = TimeSpan.FromSeconds(60.0);
            synchronizeTimer.Start();

            // And synchronize on startup.
            _community.BeginSending();
            _community.BeginReceiving();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Domain Domain
        {
            get { return _domain; }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }
    }
}
