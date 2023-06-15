using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public class PizzaFactory
    {
        public Pizza CreateCheesePizza()
        {
            PizzaBuilder builder = new PizzaBuilder();
            builder.AddTomatoSauce();
            builder.AddCheese();
            return builder.GetPizza();
        }

        public Pizza CreatePepperoniPizza()
        {
            PizzaBuilder builder = new PizzaBuilder();
            builder.AddTomatoSauce();
            builder.AddCheese();
            builder.AddPepperoni();
            return builder.GetPizza();
        }

        public Pizza CreateSalamiPizza()
        {
            PizzaBuilder builder = new PizzaBuilder();
            builder.AddTomatoSauce();
            builder.AddCheese();
            builder.AddSalami();
            return builder.GetPizza();
        }

    }
}
