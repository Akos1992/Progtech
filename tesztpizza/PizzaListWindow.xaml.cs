using PizzaBuilderApp;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace tesztpizza
{
    /// <summary>
    /// Interaction logic for PizzaListWindow.xaml
    /// </summary>
    public partial class PizzaListWindow : Window, IPizzaObserver
    {
        private PizzaRepository pizzaRepository;

        public PizzaListWindow(PizzaRepository pizzaRepository)
        {
            InitializeComponent();
            this.pizzaRepository = pizzaRepository;
            this.pizzaRepository.AddObserver(this);
        }

        public void Update(Pizza newPizza)
        {
            PizzaListBox.Items.Add(newPizza.ToString());
        }
    }
}
