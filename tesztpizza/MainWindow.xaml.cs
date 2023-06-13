using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PizzaBuilderApp;

namespace tesztpizza
{
    public partial class MainWindow : Window
    {
        private IDbConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            CreatingDatabase();
        }

        private void CreatingDatabase()
        {
            SQLiteStrategy dbStrategy = new SQLiteStrategy();
            dbStrategy.CreateDatabase("pizza.db");
            connection = dbStrategy.GetConnection();
        }

        private void BuildPizzaButton_Click(object sender, RoutedEventArgs e)
        {
            PizzaBuilder pizzaBuilder = new PizzaBuilder();

            // Készítjük a pizzát a kiválasztott feltételek alapján
            if (TomatoSauceCheckbox.IsChecked == true)
                pizzaBuilder.AddTomatoSauce();
            if (SalamiCheckbox.IsChecked == true)
                pizzaBuilder.AddSalami();
            if (PepperoniCheckbox.IsChecked == true)
                pizzaBuilder.AddPepperoni();
            if (CheeseCheckbox.IsChecked == true)
                pizzaBuilder.AddCheese();

            Pizza pizza = pizzaBuilder.GetPizza();

            // Pizza hozázadása az adatbázishoz
            //AddPizzaToDatabase(connection, pizza);

            // Megjelenítjük a kész pizzát
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("A pizzád összetevői:");
            sb.AppendLine(pizza.ToString());
            MessageBox.Show(sb.ToString(), "Pizza kész!");

            // Visszaállítjuk a kezdőállapotot
            ResetCheckBoxes();
        }

        private void ResetCheckBoxes()
        {
            TomatoSauceCheckbox.IsChecked = false;
            SalamiCheckbox.IsChecked = false;
            PepperoniCheckbox.IsChecked = false;
            CheeseCheckbox.IsChecked = false;
        }
    }
}
