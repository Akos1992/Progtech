using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza.Tests
{
    [TestClass]
    public class SQLiteDatabaseTablesTests
    {
        [TestMethod]
        public void CreateDatabase_CreatesDatabaseFile()
        {
            // Arrange
            string testDbName = "TestDB2";
            string testDbFileName = testDbName + ".sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            // Act
            strategy.CreateDatabase(testDbFileName);

            // Assert
            Assert.IsTrue(File.Exists(testDbFileName));

            // Cleanup
            File.Delete(testDbFileName);
        }

        [TestMethod]
        public void CreateDatabase_CreatesTables()
        {
            // Arrange
            string testDbName = "TestDB3";
            string testDbFileName = testDbName + ".sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            // Act
            strategy.CreateDatabase(testDbFileName);

            // Assert
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={testDbFileName};Version=3;"))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO 'pizzas' (name) VALUES ('testpizza');", conn),
                                      cmd2 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='pizzas';", conn),
                                      cmd3 = new SQLiteCommand("INSERT INTO 'toppings' (pizza_id, name) VALUES (1, 'testtopping');", conn),
                                      cmd4 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='toppings';", conn),
                                      cmd5 = new SQLiteCommand("INSERT INTO 'crusts' (pizza_id, name) VALUES (1, 'testcrust');", conn),
                                      cmd6 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='crusts';", conn))
                {
                    using (SQLiteDataReader reader = cmd2.ExecuteReader())
                    {
                        Assert.IsTrue(reader.HasRows);  // 'pizzas' table exists
                    }

                    using (SQLiteDataReader reader = cmd4.ExecuteReader())
                    {
                        Assert.IsTrue(reader.HasRows);  // 'toppings' table exists
                    }

                    using (SQLiteDataReader reader = cmd6.ExecuteReader())
                    {
                        Assert.IsTrue(reader.HasRows);  // 'crusts' table exists
                    }
                }

                conn.Close();
            }

            // Cleanup
            File.Delete(testDbFileName);
        }
    }
}
