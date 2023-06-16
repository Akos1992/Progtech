using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public interface IObservable
    {
        void AddObserver(IPizzaObserver observer);
        void RemoveObserver(IPizzaObserver observer);
        void NotifyObservers(Pizza entity);
    }
}
