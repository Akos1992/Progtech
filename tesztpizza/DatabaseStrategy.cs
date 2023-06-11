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

            using (IDbConnection connection = CreateConnection())
            {
                connection.Open();

                ExecuteNonQuery(connection, GetPizzasTableSQL());
                ExecuteNonQuery(connection, GetToppingsTableSQL());
                ExecuteNonQuery(connection, GetCrustsTableSQL());

                Console.WriteLine("Database tables created successfully.");

                connection.Close(); 
            }
        }

        // A kód ismétlés elkerülése érdekében (DRY) és a SRP elvének érdekében az SQL parancs futtatását is kiszervezzük.
        // Innentől a korábbi CreatePizzasTable, CreateToppingsTable és CreateCrustsTable metódusoknak más nem is kell létezniük
        // Elegendő ha helyettük az SQL parancsot tartalmazó string -eket adunk vissza. Ez utóbbi lehet adatbázis függő, így
        // ezt már nem érdemes bevonni az absztrakt osztályba.
        protected void ExecuteNonQuery(IDbConnection connection, string commandText)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.ExecuteNonQuery();
            }
        }

        // Az egyes táblák létrehozásának részleteit a leszármazott osztályok implementálják
        protected abstract string GetPizzasTableSQL();
        protected abstract string GetToppingsTableSQL();
        protected abstract string GetCrustsTableSQL();

        // Az adatbázis kapcsolati stringjét és a kapcsolatot a leszármazott osztályok hozzák létre
        protected abstract string GetConnectionString();
        protected abstract IDbConnection CreateConnection();
    }
}
