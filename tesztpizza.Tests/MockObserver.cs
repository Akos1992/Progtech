using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    public class MockObserver : IPizzaObserver
    {
        public Pizza LatestPizza { get; private set; }
        public void Update(Pizza pizza)
        {
            LatestPizza = pizza;
        }

        [TestClass]
        public class ObserverTests
        {
            private string dbName = "TestDB4.sqlite";
            private SQLiteStrategy dbStrategy;

            [TestInitialize]
            public void TestInitialize()
            {
                dbStrategy = new SQLiteStrategy();
                dbStrategy.CreateDatabase(dbName);
            }

            [TestCleanup]
            public void TestCleanup()
            {
                if (File.Exists(dbName))
                {
                    File.Delete(dbName);
                }
            }

            [TestMethod]
            public void TestObserverUpdateOnAddPizza()
            {
                PizzaRepository repository = new PizzaRepository(dbStrategy);
                MockObserver observer = new MockObserver();
                repository.AddObserver(observer); 

                PizzaBuilder pizza = new PizzaBuilder();
                pizza.AddTomatoSauce();
                pizza.AddCheese();
                pizza.AddPepperoni();
                pizza.AddSalami();

                repository.AddPizza(pizza.GetPizza());

                Assert.IsNotNull(observer.LatestPizza);
                Assert.AreEqual(pizza.GetPizza().ToString(), observer.LatestPizza.ToString());
            }

            [TestMethod]
            public void TestObserverRemoveOnAddPizza()
            {
                PizzaRepository repository = new PizzaRepository(dbStrategy);
                MockObserver observer = new MockObserver();
                repository.AddObserver(observer);

                // Make pizza and add to repository
                PizzaBuilder pizza = new PizzaBuilder();
                pizza.AddTomatoSauce();
                pizza.AddCheese();
                pizza.AddPepperoni();
                pizza.AddSalami();
                repository.AddPizza(pizza.GetPizza());

                // Check observer gets updated
                Assert.IsNotNull(observer.LatestPizza);
                Assert.AreEqual(pizza.GetPizza().ToString(), observer.LatestPizza.ToString());

                // Remove observer
                repository.RemoveObserver(observer);
                observer.LatestPizza = null;

                // Add another pizza
                PizzaBuilder pizza2 = new PizzaBuilder();
                pizza2.AddTomatoSauce();
                pizza2.AddCheese();
                repository.AddPizza(pizza2.GetPizza());

                // Check observer does not get updated
                Assert.IsNull(observer.LatestPizza);
            }
        }
    }
}
