using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using PizzaBuilderApp;

namespace tesztpizza.Tests
{    
    [TestClass]
    public class PizzaRepositoryTests
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
        public void TestAddPizza()
        {
            PizzaRepository repository = new PizzaRepository(dbStrategy);
            PizzaBuilder pizza = new PizzaBuilder();
            pizza.AddTomatoSauce();
            pizza.AddCheese();
            pizza.AddPepperoni();
            pizza.AddSalami();

            repository.AddPizza(pizza.GetPizza());

            using (IDbConnection connection = dbStrategy.CreateConnection())
            {
                connection.Open();
                string pizzaName = ExecuteScalar<string>(connection, "SELECT name FROM pizzas WHERE id = 1;");
                Assert.AreEqual(pizza.GetPizza().ToString(), pizzaName);

                // Testing crusts
                string crustName = ExecuteScalar<string>(connection, "SELECT name FROM crusts WHERE pizza_id = 1;");
                Assert.AreEqual(pizza.GetPizza().BaseDough, crustName);

                // Testing ingredients
                List<string> testGredients = new List<string> { "Pizzaszósz", "Sajt", "Pepperóni", "Szalámi" };
                List<string> dbGredients = new List<string>();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT name FROM toppings WHERE pizza_id = 1;";
                    using (var pizzaIngredients = command.ExecuteReader())
                    {
                        while (pizzaIngredients.Read())
                        {
                            Assert.IsTrue(testGredients.Contains(pizzaIngredients[0]));
                            dbGredients.Add(pizzaIngredients[0].ToString());
                        }
                    }
                }
                foreach(string gredient in testGredients)
                {
                    Assert.IsTrue(dbGredients.Contains(gredient));
                }

                connection.Close();
            }
        }

        private T ExecuteScalar<T>(IDbConnection connection, string commandText)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                return (T)command.ExecuteScalar();
            }
        }

    }
    
}
