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

        protected override string GetPizzasTableSQL()
        {
            return "CREATE TABLE IF NOT EXISTS pizzas (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT);";
        }

        protected override string GetToppingsTableSQL()
        {
            return "CREATE TABLE IF NOT EXISTS toppings (id INTEGER PRIMARY KEY AUTOINCREMENT, pizza_id INTEGER, name TEXT, FOREIGN KEY (pizza_id) REFERENCES pizzas(id));";
        }

        protected override string GetCrustsTableSQL()
        {
            return "CREATE TABLE IF NOT EXISTS crusts (id INTEGER PRIMARY KEY AUTOINCREMENT, pizza_id INTEGER, name TEXT, FOREIGN KEY (pizza_id) REFERENCES pizzas(id));";
        }

        protected override string GetConnectionString()
        {
            return $"Data Source={dbName};Version=3;";
        }

        public override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
