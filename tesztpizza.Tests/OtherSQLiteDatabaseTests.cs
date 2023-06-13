using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    [TestClass]
    public class OtherSQLiteDatabaseTests
    {
        [TestMethod]
        public void CreateDatabase_CreatesDatabaseFile()
        {
            // Arrange
            string testDbName = "TestDB";
            string testDbFileName = testDbName + "TestDB.sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            // Act
            strategy.CreateDatabase(testDbName);

            // Assert
            Assert.IsTrue(File.Exists(testDbFileName));

            // Cleanup
            File.Delete(testDbFileName);
        }

        [TestMethod]
        public void CreateDatabase_CreatesTables()
        {
            // Arrange
            string testDbName = "TestDB";
            string testDbFileName = testDbName + "TestDB.sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            // Act
            strategy.CreateDatabase(testDbName);

            // Assert
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={testDbFileName};Version=3;"))
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='pizzas';", conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Assert.IsTrue(reader.HasRows);  // 'pizzas' table exists

                cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='toppings';", conn);
                reader = cmd.ExecuteReader();
                Assert.IsTrue(reader.HasRows);  // 'toppings' table exists

                cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='crusts';", conn);
                reader = cmd.ExecuteReader();
                Assert.IsTrue(reader.HasRows);  // 'crusts' table exists

                conn.Close();
            }

            // Cleanup
            File.Delete(testDbFileName);
        }
    }
}
