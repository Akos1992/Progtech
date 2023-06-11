using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public class SQLiteStrategy : DatabaseStrategy
    {
        protected override void CreatePizzasTable(IDbConnection connection)
        {
            string createPizzasTableQuery = "CREATE TABLE IF NOT EXISTS pizzas (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT);";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = createPizzasTableQuery;
                command.ExecuteNonQuery();
            }
        }

        protected override void CreateToppingsTable(IDbConnection connection)
        {
            string createToppingsTableQuery = "CREATE TABLE IF NOT EXISTS toppings (id INTEGER PRIMARY KEY AUTOINCREMENT, pizza_id INTEGER, name TEXT, FOREIGN KEY (pizza_id) REFERENCES pizzas(id));";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = createToppingsTableQuery;
                command.ExecuteNonQuery();
            }
        }

        protected override void CreateCrustsTable(IDbConnection connection)
        {
            string createCrustsTableQuery = "CREATE TABLE IF NOT EXISTS crusts (id INTEGER PRIMARY KEY AUTOINCREMENT, pizza_id INTEGER, name TEXT, FOREIGN KEY (pizza_id) REFERENCES pizzas(id));";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = createCrustsTableQuery;
                command.ExecuteNonQuery();
            }
        }

        protected override string GetConnectionString()
        {
            return $"Data Source={dbName};Version=3;";
        }

        protected override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
