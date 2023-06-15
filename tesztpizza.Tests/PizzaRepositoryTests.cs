using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using tesztpizza;
    using System.Data;
    using System.IO;
    using global::PizzaBuilderApp;

    namespace PizzaBuilderApp.Tests
    {
        [TestClass]
        public class PizzaRepositoryTests
        {
            private string dbName = "TestDB.sqlite";
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
                // Arrange
                PizzaRepository repository = new PizzaRepository(dbStrategy);
                Pizza pizza = new PizzaBuilder()
                    .AddTomatoSauce()
                    .AddCheese()
                    .GetPizza();

                // Act
                repository.AddPizza(pizza);

                // Assert
                using (IDbConnection connection = dbStrategy.CreateConnection())
                {
                    connection.Open();
                    string pizzaName = ExecuteScalar<string>(connection, "SELECT name FROM pizzas WHERE id = 1");
                    Assert.AreEqual(pizza.ToString(), pizzaName);
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
}
