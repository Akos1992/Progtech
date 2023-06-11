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

                CreatePizzasTable(connection);
                CreateToppingsTable(connection);
                CreateCrustsTable(connection);

                Console.WriteLine("Database tables created successfully.");

                connection.Close();
            }
        }

        // Az egyes táblák létrehozásának részleteit a leszármazott osztályok implementálják
        protected abstract void CreatePizzasTable(IDbConnection connection);
        protected abstract void CreateToppingsTable(IDbConnection connection);
        protected abstract void CreateCrustsTable(IDbConnection connection);

        // Az adatbázis kapcsolati stringjét és a kapcsolatot a leszármazott osztályok hozzák létre
        protected abstract string GetConnectionString();
        protected abstract IDbConnection CreateConnection();
    }
}
