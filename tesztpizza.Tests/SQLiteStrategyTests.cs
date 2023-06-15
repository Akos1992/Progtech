using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    [TestClass]
    public class SQLiteStrategyTests
    {
        private string dbName = "TestDB.sqlite";

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
        }

        [TestMethod]
        public void TestCreateDatabase()
        {
            SQLiteStrategy strategy = new SQLiteStrategy();

            strategy.CreateDatabase(dbName);

            using (IDbConnection connection = strategy.CreateConnection())
            {
                connection.Open();
                Assert.IsTrue(connection.State == ConnectionState.Open);
            }
        }

        [TestMethod]
        public void TestCreateTables()
        {
            SQLiteStrategy strategy = new SQLiteStrategy();
            strategy.CreateDatabase(dbName);

            using (IDbConnection connection = strategy.CreateConnection())
            {
                connection.Open();

                // Check if pizzas table was created
                var pizzasTable = ExecuteScalar<string>(connection, "SELECT name FROM sqlite_master WHERE type='table' AND name='pizzas';");
                Assert.IsNotNull(pizzasTable);

                // Check if toppings table was created
                var toppingsTable = ExecuteScalar<string>(connection, "SELECT name FROM sqlite_master WHERE type='table' AND name='toppings';");
                Assert.IsNotNull(toppingsTable);

                // Check if crusts table was created
                var crustsTable = ExecuteScalar<string>(connection, "SELECT name FROM sqlite_master WHERE type='table' AND name='crusts';");
                Assert.IsNotNull(crustsTable);
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
