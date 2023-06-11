using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    [TestClass]
    public class PizzaDatabaseTests
    {
        private SQLiteConnection connection;
        private string dbLocation = "pizza.db";

        [TestInitialize]
        public void SetUp()
        {
            connection = new SQLiteConnection("Data Source=" + dbLocation + ";Version=3;");
            connection.Open();

            // Előkészítő műveletek: Hozzunk létre néhány pizzát az adatbázisban.
            string[] pizzas = new string[] { "Pizza1", "Pizza2", "Pizza3" };
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                foreach (string pizza in pizzas)
                {
                    command.CommandText = "INSERT INTO Pizza (Ingredients) VALUES (@ingredients)";
                    command.Parameters.AddWithValue("@ingredients", pizza);
                    command.ExecuteNonQuery();
                }
            }
        }

        [TestMethod]
        public void TestAddPizza()
        {
            // Ide jön a pizza hozzáadását tesztelő kód
        }

        [TestMethod]
        public void TestListPizzas()
        {
            // Ide jön a pizzák listázását tesztelő kód
        }

        [TestMethod]
        public void TestDeletePizza()
        {
            // Ide jön a pizza törlését tesztelő kód
        }

        [TestCleanup]
        public void TearDown()
        {
            connection.Close();
        }
    }
}
