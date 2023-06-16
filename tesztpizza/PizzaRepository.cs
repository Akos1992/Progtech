using PizzaBuilderApp;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tesztpizza
{
    public class PizzaRepository
    {
        private SQLiteStrategy dbStrategy;
        private List<IPizzaObserver> observers = new List<IPizzaObserver>();

        public PizzaRepository(SQLiteStrategy dbStrategy)
        {
            this.dbStrategy = dbStrategy;
        }

        public void AddObserver(IPizzaObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IPizzaObserver observer)
        {
            observers.Remove(observer);
        }

        public void AddPizza(Pizza pizza)
        {
            using (IDbConnection connection = dbStrategy.CreateConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    try
                    {
                        command.CommandText = "INSERT INTO pizzas (name) VALUES (@name);";
                        IDataParameter paramName = new SQLiteParameter("@name", pizza.ToString());
                        command.Parameters.Add(paramName);
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT last_insert_rowid();";
                        long pizzaId = (long)command.ExecuteScalar();

                        command.CommandText = "INSERT INTO crusts (pizza_id, name) VALUES (@pizzaId, @name);";
                        IDataParameter paramPizzaId = new SQLiteParameter("@pizzaId", pizzaId);
                        IDataParameter paramCrustName = new SQLiteParameter("@name", pizza.BaseDough);
                        command.Parameters.Clear();
                        command.Parameters.Add(paramPizzaId);
                        command.Parameters.Add(paramCrustName);
                        command.ExecuteNonQuery();

                        foreach (var ingredient in pizza.IngredientsList)
                        {
                            command.CommandText = "INSERT INTO toppings (pizza_id, name) VALUES (@pizzaId, @name);";
                            paramPizzaId = new SQLiteParameter("@pizzaId", pizzaId);
                            IDataParameter paramIngredientName = new SQLiteParameter("@name", ingredient);
                            command.Parameters.Clear();
                            command.Parameters.Add(paramPizzaId);
                            command.Parameters.Add(paramIngredientName);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                connection.Close();
            }

            foreach(var observer in observers)
            {
                observer.Update(pizza);
            }
        }
    }
}
