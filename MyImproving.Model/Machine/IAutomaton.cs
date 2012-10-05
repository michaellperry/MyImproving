using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyImproving.Model.Machine
{
    public interface IAutomaton<T>
    {
        IEnumerable<T> GetItems();
        void Process(T item);
    }
}
