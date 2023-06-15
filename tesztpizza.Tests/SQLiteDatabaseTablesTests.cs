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
            string testDbName = "TestDB2";
            string testDbFileName = testDbName + ".sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            strategy.CreateDatabase(testDbFileName);

            Assert.IsTrue(File.Exists(testDbFileName));

            // Cleanup
            File.Delete(testDbFileName);
        }

        [TestMethod]
        public void CreateDatabase_CreatesTables()
        {
            string testDbName = "TestDB3";
            string testDbFileName = testDbName + ".sqlite";
            SQLiteStrategy strategy = new SQLiteStrategy();

            strategy.CreateDatabase(testDbFileName);

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={testDbFileName};Version=3;"))
            {
                conn.Open();

                using (SQLiteCommand cmd1 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='pizzas';", conn),
                                      cmd2 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='toppings';", conn),
                                      cmd3 = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='crusts';", conn))
                {
                    using (SQLiteDataReader reader = cmd1.ExecuteReader())
                    {
                        Assert.IsTrue(reader.HasRows);  // 'pizzas' table exists
                    }

                    using (SQLiteDataReader reader = cmd2.ExecuteReader())
                    {
                        Assert.IsTrue(reader.HasRows);  // 'toppings' table exists
                    }

                    using (SQLiteDataReader reader = cmd3.ExecuteReader())
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
