using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public interface IPizzaObserver
    {
        void Update(Pizza pizza);
    }
}
