using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    [TestClass]
    public class PizzaFactoryTests
    {
        [TestMethod]
        public void TestCreateCheesePizza()
        {
            PizzaFactory factory = new PizzaFactory();

            Pizza pizza = factory.CreateCheesePizza();

            Assert.AreEqual("Alap tészta\nPizzaszósz\nSajt\n", pizza.ToString());
        }

        [TestMethod]
        public void TestCreatePepperoniPizza()
        {
            PizzaFactory factory = new PizzaFactory();

            Pizza pizza = factory.CreatePepperoniPizza();

            Assert.AreEqual("Alap tészta\nPizzaszósz\nSajt\nPepperóni\n", pizza.ToString());
        }

        [TestMethod]
        public void TestCreateSalamiPizza()
        {
            PizzaFactory factory = new PizzaFactory();

            Pizza pizza = factory.CreateSalamiPizza();

            Assert.AreEqual("Alap tészta\nPizzaszósz\nSajt\nSzalámi\n", pizza.ToString());
        }
    }
}
