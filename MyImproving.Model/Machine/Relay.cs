using System.Collections.Generic;
using System.Linq;
using UpdateControls;

namespace MyImproving.Model.Machine
{
    public class Relay<T> : IRelay
    {
        private readonly IAutomaton<T> _automaton;

        private List<T> _queue;
        private Dependent _depQueue;

        public Relay(Actuator actuator, IAutomaton<T> automaton)
        {
            _automaton = automaton;
            _depQueue = new Dependent(UpdateQueue);
            _depQueue.Invalidated += delegate
            {
                actuator.RequestTrigger(this);
            };
            _depQueue.OnGet();
        }

        public void Go()
        {
            _depQueue.OnGet();
            foreach (T item in _queue)
                _automaton.Process(item);
        }

        private void UpdateQueue()
        {
            _queue = _automaton.GetItems().ToList();
        }
    }
}
