using System.Collections.Generic;
using System.Threading;
using UpdateControls.Fields;

namespace MyImproving.Model.Machine
{
    public class Actuator
    {
        private List<IRelay> _relays = new List<IRelay>();
        private Independent<bool> _running = new Independent<bool>(false);
        private List<IRelay> _awaitingTrigger = new List<IRelay>();
        private int _triggerCount = 0;
        private ManualResetEvent _triggerCountIsZero = new ManualResetEvent(true);

        public Actuator Add<T>(IAutomaton<T> automaton)
        {
            _relays.Add(new Relay<T>(this, automaton));
            return this;
        }

        public void Start()
        {
            lock (this)
            {
                _running.Value = true;

                foreach (var relay in _awaitingTrigger)
                    Trigger(relay);
                _awaitingTrigger.Clear();
            }
        }

        public void Stop()
        {
            lock (this)
            {
                _running.Value = false;
            }
        }

        public bool Running
        {
            get { return _running; }
        }

        public void Quiesce()
        {
            _triggerCountIsZero.WaitOne();
        }

        internal void RequestTrigger(IRelay relay)
        {
            lock (this)
            {
                if (_running)
                    Trigger(relay);
                else
                    _awaitingTrigger.Add(relay);
            }
        }

        private void Trigger(IRelay relay)
        {
            if (_triggerCount == 0)
                _triggerCountIsZero.Reset();
            _triggerCount++;
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    relay.Go();
                }
                finally
                {
                    DecrementTriggerCount();
                }
            });
        }

        private void DecrementTriggerCount()
        {
            lock (this)
            {
                _triggerCount--;
                if (_triggerCount == 0)
                    _triggerCountIsZero.Set();
            }
        }
    }
}
