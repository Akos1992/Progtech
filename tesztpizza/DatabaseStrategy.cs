using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public abstract class DatabaseStrategy
    {
        protected string dbName;
        protected string connectionString;
        protected IDbConnection connection;

        // A sablon metódus (a Strtégiában), amely definiálja a táblák létrehozásának sorrendjét.
        public void CreateDatabase(string nameOfDb)
        {
            if (nameOfDb == null)
            {
                Console.WriteLine("Define the database name, please.");
                throw new ArgumentNullException();
            }
            dbName = nameOfDb;
            connectionString = GetConnectionString();

            using (connection = CreateConnection())
            {
                connection.Open();

                ExecuteNonQuery(GetPizzasTableSQL());
                ExecuteNonQuery(GetToppingsTableSQL());
                ExecuteNonQuery(GetCrustsTableSQL());

                Console.WriteLine("Database tables created successfully.");

                connection.Close(); 
            }
        }

        // A kód ismétlés elkerülése érdekében (DRY) és a SRP elvének érdekében az SQL parancs futtatását is kiszervezzük.
        // Innentől a korábbi CreatePizzasTable, CreateToppingsTable és CreateCrustsTable metódusoknak már nem is kell létezniük.
        // Elegendő ha helyettük az SQL parancsot tartalmazó string -eket adunk vissza. Ez utóbbi lehet adatbázis függő, így
        // ennek definiálását már nem érdemes bevonni az absztrakt osztályba.
        protected void ExecuteNonQuery(string commandText)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.ExecuteNonQuery();
            }
        }

        public IDbConnection GetConnection() { return connection; }

        // Az egyes táblák létrehozásának részleteit a leszármazott osztályok implementálják
        protected abstract string GetPizzasTableSQL();
        protected abstract string GetToppingsTableSQL();
        protected abstract string GetCrustsTableSQL();

        // Az adatbázis kapcsolati stringjét és a kapcsolatot a leszármazott osztályok hozzák létre
        protected abstract string GetConnectionString();
        public abstract IDbConnection CreateConnection();
    }
}
